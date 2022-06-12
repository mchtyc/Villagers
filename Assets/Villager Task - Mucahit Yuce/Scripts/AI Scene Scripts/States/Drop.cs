using System;
using System.Collections.Generic;
using UnityEngine;

public class Drop : IState
{
    public static event Action<int> OnItemCollected;

    CharacterAI character;
    Animator animator;
    Transform item;
    float enterTime;
    bool dropped;
    int score = 0;

    public Drop(CharacterAI _character)
    {
        character = _character;
        animator = character.GetComponent<Animator>();
    }

    public void OnEnter()
    {
        animator.SetTrigger("drop");
        item = character.targetItem;
        enterTime = Time.time;
    }

    public void OnExit()
    {
        dropped = false;
    }

    public void Tick()
    {
        if(Time.time - enterTime >= 1f && !dropped)
        {
            Item _item = item.GetComponent<Item>();
            item.position = _item.CalculateDestination();
            item.rotation = Quaternion.identity;
            item.parent = _item.GetCollectionArea();
            dropped = true;
        }
        if(Time.time - enterTime >= 2f)
        {
            character.targetItem = null;
            OnItemCollected?.Invoke(++score);
        }
    }
}
