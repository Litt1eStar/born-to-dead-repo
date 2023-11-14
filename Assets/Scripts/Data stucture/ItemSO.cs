using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    WEAPON,
    ACTIVE,
    PASSIVE,
    Undefined
}
[CreateAssetMenu(menuName = "ItemSO")]
public class ItemSO : ScriptableObject
{
    public InventoryType ItemType;
    public bool IsStackable;
    public int ID => GetInstanceID();
    public int MaxStackSize = 1;
    public string Name;
    public string Description;
    public Sprite ItemImage; 
}
