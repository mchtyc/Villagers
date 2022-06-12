using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Types {Unbreakable, Breakable}

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public Color color;
    public string tag;
    public Types type;
}
