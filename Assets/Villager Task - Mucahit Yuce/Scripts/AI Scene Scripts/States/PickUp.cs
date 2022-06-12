using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : IState
{
    CharacterAI character;
    Transform itemRoot, targetItem;
    Animator animator;
    float enterTime;
    bool itemAcquired;

    public PickUp(CharacterAI _character)
    {
        character = _character;
        animator = _character.GetComponent<Animator>();
        itemRoot = _character.itemRoot;
    }

    public void OnEnter()
    {
        animator.SetTrigger("pickup");
        enterTime = Time.time;
        targetItem = character.targetItem;
    }

    public void OnExit()
    {
        character.pickedUp = false;
        itemAcquired = false;
    }

    public void Tick()
    {
        if(Time.time - enterTime >= 1f && !itemAcquired)
        {
            targetItem.parent = itemRoot;
            targetItem.localPosition = Vector3.zero;
            targetItem.rotation = itemRoot.rotation;
            itemAcquired = true;
        }

        if (Time.time - enterTime >= 2f)
            character.pickedUp = true;
    }
}
