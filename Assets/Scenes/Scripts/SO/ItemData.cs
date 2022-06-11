using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Orange, Purple, Red, Pink }

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public ItemType type;
    public Color color;
    public string tag;
}
