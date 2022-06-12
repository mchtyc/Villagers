using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakItem : IState
{
    CharacterAI character;
    Animator animator;
    float enterTime;
    GameObject brokenBox, hammer;
    bool itemDestroyed;

    public BreakItem(CharacterAI _character, GameObject _hammer)
    {
        character = _character;
        animator = character.GetComponent<Animator>();
        hammer = _hammer;
    }

    public void OnEnter()
    {
        brokenBox = character.GenerateBrokenBox();
        animator.SetTrigger("hit with hammer");
        enterTime = Time.time;
    }

    public void OnExit()
    {
        hammer.SetActive(false);
        itemDestroyed = false;
    }

    public void Tick()
    {
        if(Time.time - enterTime >= 0.5f && !itemDestroyed)
        {
            brokenBox.SetActive(true);
            character.DestroyTargetItem();
            Transform fracturesRoot = brokenBox.transform.GetChild(0);

            float max_force = 3f;

            // Fracturing item
            for (int i = 0; i < fracturesRoot.childCount; i++)
            {
                fracturesRoot.GetChild(i).GetComponent<Rigidbody>().AddForce(Random.Range(-max_force, max_force),
                                                                             Random.Range(-max_force * 3f, max_force * 3f),
                                                                             Random.Range(-max_force, max_force),
                                                                             ForceMode.Impulse);
            }
            itemDestroyed = true;
        }

        if(Time.time - enterTime >= 1f)
            character.DestroyBrokenBox();
    }
}
