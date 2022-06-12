using System.Collections;
using System;
using UnityEngine;

public class CharacterAI : MonoBehaviour
{
    [HideInInspector] public Transform targetItem;
    [SerializeField] GameObject hammer, brokenBoxPrefab;
    public Transform itemRoot;

    [HideInInspector] public bool hammerAcquired, isItemClose, pickedUp, readyToDrop;

    StateMachine stateMachine;
    GameObject brokenBoxInstance;

    void Start()
    {
        stateMachine = new StateMachine();

        // Inýtilizing states
        var search = new SearchForItem(this);
        var moveToItem = new WalkToItem(this);
        var getHammer = new GetHammer(this, hammer);
        var pickup = new PickUp(this);
        var breakItem = new BreakItem(this, hammer);
        var moveToCollectionArea = new WalkToCollectionArea(this);
        var drop = new Drop(this);

        // Generating all transitions
        stateMachine.AddTransition(search, moveToItem, HasUnbreakableTarget());
        stateMachine.AddTransition(search, getHammer, HasBreakableTarget());
        stateMachine.AddTransition(getHammer, moveToItem, HammerAcquired());
        stateMachine.AddTransition(moveToItem, pickup, ReadyToPickup());
        stateMachine.AddTransition(moveToItem, breakItem, Breakable());
        stateMachine.AddTransition(pickup, moveToCollectionArea, PickedUp());
        stateMachine.AddTransition(moveToCollectionArea, drop, ReadyToDrop());
        stateMachine.AddTransition(drop, search, Searchable());
        stateMachine.AddTransition(breakItem, search, Searchable());

        // Setting default state to start with
        stateMachine.SetState(search);

        // Defining conditions of all transitions
        Func<bool> HasUnbreakableTarget() => () => targetItem != null && targetItem.GetComponent<Item>().GetType() == Types.Unbreakable;
        Func<bool> HasBreakableTarget() => () => targetItem != null && targetItem.GetComponent<Item>().GetType() == Types.Breakable;
        Func<bool> HammerAcquired() => () => hammerAcquired;
        Func<bool> ReadyToPickup() => () => isItemClose && !hammer.activeSelf;
        Func<bool> PickedUp() => () => pickedUp;
        Func<bool> ReadyToDrop() => () => readyToDrop;
        Func<bool> Searchable() => () => targetItem == null && brokenBoxInstance == null;
        Func<bool> Breakable() => () => isItemClose && hammer.activeSelf;
    }

    void Update() => stateMachine.Tick();

    // Turning character towards target
    public IEnumerator FacingTarget(Vector3 _target)
    {
        Quaternion new_rot = Quaternion.LookRotation(_target - transform.position, transform.up);

        for (int i = 1; i < 21; i++)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, new_rot, i / 20f);

            yield return new WaitForSeconds(0.03f);
        }
    }

    public GameObject GenerateBrokenBox()
    {
        brokenBoxInstance = Instantiate(brokenBoxPrefab, targetItem.transform.position,
                                                         targetItem.transform.rotation);
        brokenBoxInstance.SetActive(false);

        // Changing materials of fratures as they look like the original item
        GameObject fractures_root = brokenBoxInstance.transform.GetChild(1).gameObject;
        Material new_material = targetItem.GetComponent<MeshRenderer>().material;

        for (int i = 0; i < fractures_root.transform.childCount; i++)
        {
            fractures_root.transform.GetChild(i).GetComponent<SkinnedMeshRenderer>().material = new_material;
        }

        return brokenBoxInstance;
    }

    public void DestroyTargetItem()
    {
        Destroy(targetItem.gameObject);
    }

    public void DestroyBrokenBox()
    {
        Destroy(brokenBoxInstance);
    }
}
