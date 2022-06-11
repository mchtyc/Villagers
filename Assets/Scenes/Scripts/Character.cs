using System.Collections;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Character : MonoBehaviour
{
    public static event Action<int> OnAddScoreEvent;
    int score;

    [SerializeField] Transform itemContainer, itemRoot;
    [SerializeField] float runSpeed = 5f, walkWithItemSpeed = 3f, walkWithHammerSpeed = 4f;
    [SerializeField] GameObject hammer, brokenBoxPrefab;
    [SerializeField] ButtonInteractions btnInteractions;

    Animator animator;
    
    bool running, walkingWithItem, walkWithHammer;
    Transform currentTarget;
    Vector3 collectionPos;
    GameObject brokenBoxInstance;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        score = 0;
    }

    void Update()
    {
        if (running) Run();
        if (walkingWithItem) WalkWithItem();
        if (walkWithHammer) WalkWithHammer();
    }

    #region Pickup Clicked

    public void OnPickupClicked()
    {
        if (itemContainer.childCount > 0)
        {
            hammer.SetActive(false);
            btnInteractions.DisableAllBtns();

            currentTarget = itemContainer.GetChild(0);
            StartCoroutine(FacingTarget(currentTarget.position));

            running = true;
            animator.SetTrigger("run");
        }
    }

    void Run()
    {
        transform.position += transform.forward * runSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, currentTarget.position) <= 2f)
        {
            PickUp();
            running = false;
        }
    }

    void PickUp()
    {
        animator.SetTrigger("pickup");

        StartCoroutine(PickingUp());
    }

    IEnumerator PickingUp()
    {
        yield return new WaitForSeconds(1f);

        btnInteractions.EnableGatherBtn();

        currentTarget.parent = itemRoot;
        currentTarget.localPosition = Vector3.zero;
        currentTarget.rotation = itemRoot.rotation;
    }

    #endregion

    #region Gather Clicked

    public void OnGatherClicked()
    {
        btnInteractions.DisableAllBtns();
        collectionPos = currentTarget.GetComponent<Item>().CalculateDestination();

        StartCoroutine(FacingTarget(collectionPos));
        walkingWithItem = true;
        animator.SetTrigger("walk with item");
    }

    void WalkWithItem()
    {
        transform.position += transform.forward * walkWithItemSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, collectionPos) <= 2f)
        {
            DropItem();
            walkingWithItem = false;
        }
    }

    void DropItem()
    {
        animator.SetTrigger("drop");

        StartCoroutine(Dropping());
    }

    IEnumerator Dropping()
    {
        yield return new WaitForSeconds(1f);

        btnInteractions.EnableSpawnBtn();

        OnAddScoreEvent?.Invoke(++score);
        currentTarget.position = collectionPos;
        currentTarget.rotation = Quaternion.identity;
        currentTarget.parent = currentTarget.GetComponent<Item>().GetCollectionArea();
    }

    #endregion

    #region Get Hammer Clicked

    public void OnGetHammerCliked()
    {
        btnInteractions.DisableAllBtns();
        animator.SetTrigger("get hammer");

        StartCoroutine(GettingHammer());
    }

    IEnumerator GettingHammer()
    {
        yield return new WaitForSeconds(0.5f);

        hammer.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        btnInteractions.EnableHitBtn();
        btnInteractions.EnablePickBtn();
    }

    #endregion

    #region Hit with Hammer Clicked

    public void OnHitWithHammerClicked()
    {
        if (itemContainer.childCount > 0)
        {
            btnInteractions.DisableAllBtns();

            currentTarget = itemContainer.GetChild(0);
            GenerateBrokenBox(currentTarget);
            StartCoroutine(FacingTarget(currentTarget.position));

            walkWithHammer = true;
            animator.SetTrigger("walk with hammer");
        }
    }

    void GenerateBrokenBox(Transform _transform)
    {
        brokenBoxInstance = Instantiate(brokenBoxPrefab, _transform.position, _transform.rotation);
        brokenBoxInstance.SetActive(false);

        // Changing materials of fratures as they look like the original item
        GameObject fractures_root = brokenBoxInstance.transform.GetChild(1).gameObject;
        Material new_material = currentTarget.GetComponent<MeshRenderer>().material;

        for (int i = 0; i < fractures_root.transform.childCount; i++)
        {
            fractures_root.transform.GetChild(i).GetComponent<SkinnedMeshRenderer>().material = new_material;
        }
    }

    void WalkWithHammer()
    {
        transform.position += transform.forward * walkWithHammerSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, currentTarget.position) <= 2f)
        {
            BreakDownItem();
            walkWithHammer = false;
        }
    }

    void BreakDownItem()
    {
        animator.SetTrigger("hit with hammer");

        StartCoroutine(BrakingDownItem());
    }

    IEnumerator BrakingDownItem()
    {
        yield return new WaitForSeconds(0.5f);

        brokenBoxInstance.SetActive(true);
        Transform fracturesRoot = brokenBoxInstance.transform.GetChild(0);

        float max_force = 3f;

        // Fracturing item
        for (int i = 0; i < fracturesRoot.childCount; i++)
        {
            fracturesRoot.GetChild(i).GetComponent<Rigidbody>().AddForce(Random.Range(-max_force, max_force),
                                                                         Random.Range(-max_force * 3f, max_force * 3f),
                                                                         Random.Range(-max_force, max_force),
                                                                         ForceMode.Impulse);
        }

        Destroy(currentTarget.gameObject);
        yield return new WaitForSeconds(1f);
        Destroy(brokenBoxInstance);
        btnInteractions.EnableSpawnBtn();
    }

    #endregion

    // Turning character towards target
    IEnumerator FacingTarget(Vector3 _target)
    {
        Quaternion new_rot = Quaternion.LookRotation(_target - transform.position, transform.up);

        for (int i = 1; i < 21; i++)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, new_rot, i / 20f);

            yield return new WaitForSeconds(0.03f);
        }
    }
}
