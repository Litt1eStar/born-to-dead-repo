using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class LootItem : Item, ICollectible
{
    [Header("Item Setting")]
    public ItemSO inventoryItem;
    protected virtual void OnValidate()
    {
        gameObject.name = "[" + inventoryItem.Name + "]"+ " : Item Drop";
        GetComponent<SpriteRenderer>().sprite = inventoryItem.sprite;
    }

    public void Collect()
    {
        Destroy(gameObject);
    }
}
