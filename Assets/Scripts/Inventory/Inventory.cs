using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // TKey for Weapon Value, TValue for Stack Value
    public Dictionary<Weapon, int> weaponInventory = new();

    public int weaponInventorySize = 1;
    public int activeSkillInventorySize = 6;
    public int passiveSkillInventorySize = 20;

    public void AddItem<T>(T item, Dictionary<T, int> inventory)
    {
        if (item == null) { Debug.LogError($"{item} is null"); return; }

        if(item is Weapon)
        {
            if (inventory.Count == weaponInventorySize)
            {
                Debug.Log("Weapon Inventory is Full");
                return;
            }
           
            inventory.Add(item, 1);
            LogItemAdded(item, inventory[item]);
        }
    }

    public void RemoveItem<T>(T item, Dictionary<T, int> inventory)
    {
        if (inventory.Count <= 0) return;

        //if there is item in inventory then player can remove it
        if (inventory.ContainsKey(item))
        {
            //if there is more than 1 stack of item then decrease stack size
            if (inventory[item] > 1)
            {
                inventory[item]--;
                LogItemRemoved(item, inventory[item]);
            }
            //if there is only 1 item then remove that item from inventory
            else if (inventory[item] == 1)
            {
                inventory.Remove(item);
                LogItemRemoved(item);
            }
        }
        else
        {
            Debug.LogWarning("There is no item to Remove");
        }
    }

    #region Inventory Setup
    
    public void ClearInventory<T>(Dictionary<T, int> inventory)
    {
        inventory.Clear();
        Debug.Log($"{inventory} was Clear || Member Left = {inventory.Count}");
    }

    #endregion
    #region Logging
    public void InventoryInformation()
    {
        Debug.Log("---------------------Inventory Information---------------------");
        Debug.Log($"Weapon Inventory Item Count = {weaponInventory.Count}");

        Debug.Log("---------------------Weapon Inventory---------------------");
        weaponInventory.Keys.ToList().ForEach(item =>
        {
            Debug.Log($"{item.weaponName} | {weaponInventory[item]} stack");
        });
        Debug.Log("---------------------------------------------------------------");
    }

    private void LogItemAdded<T>(T item, int stack)
    {
        Debug.Log($"Added {item.ToString()} | Stack: {stack}");
    }

    private void LogItemRemoved<T>(T item, int stack)
    {
        Debug.Log($"Removed {item.ToString()} | Stack: {stack}");
    }

    private void LogItemRemoved<T>(T item)
    {
        Debug.Log($"Removed {item.ToString()} | No more items in the inventory");
    }

    #endregion

}