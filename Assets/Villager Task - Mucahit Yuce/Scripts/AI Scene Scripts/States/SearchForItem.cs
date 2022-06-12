using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForItem : IState
{
    CharacterAI character;
    Transform itemContainer;

    public SearchForItem(CharacterAI _character) 
    {
        character = _character;
        itemContainer = GameObject.Find("Item Container").transform;
    }

    public void OnEnter(){ }

    public void OnExit() { }

    public void Tick()
    {
        if (itemContainer.childCount > 0)
            character.targetItem = itemContainer.GetChild(0);
    }
}
