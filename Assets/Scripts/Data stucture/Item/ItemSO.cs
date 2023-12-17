    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemSO")]
public class ItemSO : ScriptableObject
{
    public ItemType ItemType;
    public bool IsStackable;
    public int ID => GetInstanceID();
    public int MaxStackSize = 1;
    public string Name;
    public string Description;
    public Sprite sprite; 

}
