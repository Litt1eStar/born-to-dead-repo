using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ItemType
{
    Weapon,
    ActiveSkill,
    PassiveSkill,
    Augment,
    Undefined,
    MultipleTypes
}
[CreateAssetMenu(menuName = "InventorySO")]
public class InventorySO : ScriptableObject
{
    [SerializeField]
    public List<InventoryItem> inventoryItems;

    [field: SerializeField]
    public int Size { get; private set; } = 6;
    public ItemType inventoryType;
    public event Action<Dictionary<Dictionary<int, InventoryItem>, ItemType>> OnInventoryUpdated;

    public void Initialize()
    {
        inventoryItems = new List<InventoryItem>();
        for (int i = 0; i < Size; i++)
        {
            inventoryItems.Add(InventoryItem.GetEmptyItem());
        }
    }

    public void AddItem(ItemSO item, int quantity)
    {
        if (item == null)
        {
            Debug.LogError("Item is null");
            return;
        }

        //Create new Item
        InventoryItem newItem = new InventoryItem
        {
            item = item,
            quantity = quantity
        };

        //Case : Item isn't Stackable
        if (!item.IsStackable)
        {
            //Loop in Inventory
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (!IsInventoryFull())
                {
                    //if value of inventoryItem[i] is Empty then create new Item
                    if (inventoryItems[i].IsEmpty)
                    {
                        inventoryItems[i] = newItem;
                        return;
                    }
                }
                else Debug.Log("Inventory is full");
            }
        }

        //Case : Item is Stackable
        else if(item.IsStackable)
        {
            //Loop in Inventory
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (!IsInventoryFull())
                {
                    //if value of inventoryItem[i] is Empty then create new Item
                    if (inventoryItems[i].IsEmpty)
                    {
                        inventoryItems[i] = newItem;
                        return;
                    }
                    //if ID of inventoryItem[i] is equal tp ID of newItem then increase stack of item
                    if (inventoryItems[i].item.ID == newItem.item.ID)
                    {
                        InventoryItem sameItemInInventory = inventoryItems[i];
                        if (sameItemInInventory.quantity < sameItemInInventory.item.MaxStackSize)
                        {
                            sameItemInInventory.quantity += newItem.quantity;
                            inventoryItems[i] = sameItemInInventory;
                            Debug.Log(inventoryItems[i].quantity);
                            return;
                        }
                    }
                }
                else Debug.Log("Inventory is full");
            }
        }
    }
    private bool IsInventoryFull()
    {
        int inventoryItemCount = 0;
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty)
            {
                return false;
            }
            else if (!inventoryItems[i].IsEmpty)
            {
                if (inventoryItems[i].quantity == inventoryItems[i].item.MaxStackSize)
                    inventoryItemCount++;
            }
        }

        if(inventoryItemCount == inventoryItems.Count)
            return true;    
        else
            return false;
    }
        

    public Dictionary<Dictionary<int, InventoryItem>, ItemType> GetCurrentInventoryState()
    {
        Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();
        List<ItemType> itemTypes = new List<ItemType>();

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty)
                continue;

            returnValue[i] = inventoryItems[i];
            itemTypes.Add(inventoryItems[i].ItemType);
        }

        ItemType inventoryType = DetermineOverallInventoryType(itemTypes);

        Dictionary<Dictionary<int, InventoryItem>, ItemType> inventoryState = new Dictionary<Dictionary<int, InventoryItem>, ItemType>
    {
        { returnValue, inventoryType }
    };
        return inventoryState;
    }

    private ItemType DetermineOverallInventoryType(List<ItemType> itemTypes)
    {
        if (itemTypes.Count == 1)
        {
            // Only one type found in the inventory
            return itemTypes.First();
        }
        else if (itemTypes.Count == 0)
        {
            // No items in the inventory
            return ItemType.Undefined;
        }
        else
        {
            // Multiple types found, decide how to handle it
            // For this example, let's prioritize ActiveSkill > PassiveSkill > Weapon
            if (itemTypes.Contains(ItemType.ActiveSkill))
            {
                Debug.Log("ActiveSKill");
                return ItemType.ActiveSkill;
            }
            else if (itemTypes.Contains(ItemType.PassiveSkill))
            {
                Debug.Log("PassiveSKill");
                return ItemType.PassiveSkill;
            }
            else if (itemTypes.Contains(ItemType.Weapon))
            {
                return ItemType.Weapon;
            }
            else
            {
                // No recognized type found among multiple types
                // You might want to log a warning or throw an exception
                Debug.Log("No recognized type");
                return ItemType.MultipleTypes;
            }
        }
    }
    public InventoryItem GetItemAt(int itemIndex)
    {
        return inventoryItems[itemIndex];
    }

    public void AddItem(InventoryItem item)
    {
        AddItem(item.item, item.quantity);
    }

    public void SwapItems(int itemIndex_1, int itemIndex_2)
    {
        InventoryItem item1 = inventoryItems[itemIndex_1];
        inventoryItems[itemIndex_1] = inventoryItems[itemIndex_2];
        inventoryItems[itemIndex_2] = item1;
        InformAboutChange();
    }

    private void InformAboutChange()
    {
        OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
    }

    public List<ActiveSkillSO> GetActiveSkillInInventory()
    {
        List<ActiveSkillSO> v_list  = new List<ActiveSkillSO>();
        inventoryItems.ForEach(item => {
            if(item.item != null)
                v_list.Add((ActiveSkillSO)item.item);
        });

        return v_list;
    }

    public bool IsStack(InventoryItem _item)
    {
        return _item.quantity > 0 ? true : false;
    }
}


[Serializable]
public struct InventoryItem
{
    public int quantity;
    public ItemSO item;
    public ItemType ItemType => item != null ? item.ItemType : ItemType.Undefined;
    public bool IsEmpty => item == null;

    public InventoryItem ChangeQuantity(int newQuantity)
    {
        return new InventoryItem
        {
            item = this.item,
            quantity = newQuantity
        };
    }

    public static InventoryItem GetEmptyItem() =>
        new InventoryItem
        {
            item = null,
            quantity = 0
        };
}
