using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkToCollectionArea : IState
{
    CharacterAI character;
    Animator animator;
    Transform characterTransform;
    float speedWithItem = 3f;
    Vector3 target;

    public WalkToCollectionArea(CharacterAI _character)
    {
        character = _character;
        animator = character.GetComponent<Animator>();
        characterTransform = character.transform;
    }

    public void OnEnter()
    {
        target = character.targetItem.GetComponent<Item>().CalculateDestination();
        character.StartCoroutine(character.FacingTarget(target));
        animator.SetTrigger("walk with item");
    }

    public void OnExit()
    {
        character.readyToDrop = false;
    }

    public void Tick()
    {
        characterTransform.position += (target - characterTransform.position).normalized * speedWithItem * Time.deltaTime;

        if (Vector3.Distance(characterTransform.position, target) <= 1f)
        {
            character.readyToDrop = true;
        }
    }
}
