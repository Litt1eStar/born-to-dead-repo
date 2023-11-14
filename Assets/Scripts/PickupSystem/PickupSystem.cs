using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    [SerializeField] private InventorySO inventoryData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
/*        Item item = collision.GetComponent<Item>();
        if(item != null)
        {
            Debug.Log("item is not null");
            int reminder = inventoryData.AddItem(item.inventoryItem, item.Quantity);
            if(reminder == 0)
                item.DestroyItem();
            else
                item.Quantity = reminder;
        }*/
    }
}
