using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHammer : IState
{
    CharacterAI character;
    GameObject hammer;
    Animator animator;
    float startTime;
    bool activated;

    public GetHammer(CharacterAI _character, GameObject _hammer)
    {
        character = _character;
        animator = _character.GetComponent<Animator>();
        hammer = _hammer;
    }
    
    public void OnEnter()
    {
        startTime = Time.time;
        animator.SetTrigger("get hammer");
    }

    public void OnExit()
    {
        character.hammerAcquired = false;
        activated = false;
    }

    public void Tick()
    {
        if (Time.time - startTime >= 0.5f && !activated)
        {
            hammer.SetActive(true);
            activated = true;
        }

        if (Time.time - startTime >= 1f) character.hammerAcquired = true;
    }
}
