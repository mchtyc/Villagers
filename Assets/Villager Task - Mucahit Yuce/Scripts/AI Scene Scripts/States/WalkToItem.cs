using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkToItem : IState
{
    CharacterAI character;
    Animator animator;
    Transform target, characterTransform;
    float runSpeed = 5f;

    public WalkToItem(CharacterAI _character)
    {
        character = _character;
        animator = character.GetComponent<Animator>();
        characterTransform = character.transform;
    }

    public void OnEnter()
    {
        target = character.targetItem;
        character.StartCoroutine(character.FacingTarget(target.position));
        animator.SetTrigger("run");
    }

    public void OnExit()
    {
        character.isItemClose = false;
    }

    public void Tick()
    {
        characterTransform.position += (target.position - characterTransform.position).normalized * runSpeed * Time.deltaTime;

        if (Vector3.Distance(characterTransform.position, target.position) <= 1.2f)
            character.isItemClose = true;
    }
}
