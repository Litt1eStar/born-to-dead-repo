using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum InventoryType
{
    Weapon,
    ActiveSkill,
    PassiveSkill,
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
    public InventoryType inventoryType;
    public event Action<Dictionary<Dictionary<int, InventoryItem>, InventoryType>> OnInventoryUpdated;

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
                //if value of inventoryItem[i] is Empty then create new Item
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = newItem;
                    return;
                }
            }
        }

        //Case : Item is Stackable
        else if(item.IsStackable)
        {
            //Loop in Inventory
            for (int i = 0; i < inventoryItems.Count; i++)
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
        }
    }
    private int AddItemToFirstFreeSlot(ItemSO item, int quantity)
    {
        InventoryItem newItem = new InventoryItem
        {
            item = item,
            quantity = quantity
        };

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty)
            {
                inventoryItems[i] = newItem;
                return quantity;
            }
        }
        return 0;
    }

/*    public int AddItem(ItemSO item, int quantity)
    {
        if (item.IsStackable == false)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                while (quantity > 0 && IsInventoryFull() == false)
                {
                    quantity -= AddItemToFirstFreeSlot(item, 1);
                }
                InformAboutChange();
                return quantity;
            }
        }
        quantity = AddStackableItem(item, quantity);
        InformAboutChange();
        return quantity;
    }
*/
    private bool IsInventoryFull()
        => inventoryItems.Where(item => item.IsEmpty).Any();
    private int AddStackableItem(ItemSO item, int quantity)
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty)
                continue;

            if (inventoryItems[i].item.ID == item.ID)
            {
                int amountPossibleToTake = inventoryItems[i].item.MaxStackSize - inventoryItems[i].quantity;

                if (quantity > amountPossibleToTake)
                {
                    inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].item.MaxStackSize);
                    quantity -= amountPossibleToTake;
                }
                else
                {
                    inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].quantity + quantity);
                    InformAboutChange();
                    return 0;
                }
            }
        }

        while (quantity > 0 && IsInventoryFull() == false)
        {
            int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
            quantity -= newQuantity;
            AddItemToFirstFreeSlot(item, newQuantity);
        }
        return quantity;
    }

    public Dictionary<Dictionary<int, InventoryItem>, InventoryType> GetCurrentInventoryState()
    {
        Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();
        List<InventoryType> itemTypes = new List<InventoryType>();

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty)
                continue;

            returnValue[i] = inventoryItems[i];
            itemTypes.Add(inventoryItems[i].ItemType);
        }

        InventoryType inventoryType = DetermineOverallInventoryType(itemTypes);

        Dictionary<Dictionary<int, InventoryItem>, InventoryType> inventoryState = new Dictionary<Dictionary<int, InventoryItem>, InventoryType>
    {
        { returnValue, inventoryType }
    };
        return inventoryState;
    }

    private InventoryType DetermineOverallInventoryType(List<InventoryType> itemTypes)
    {
        if (itemTypes.Count == 1)
        {
            // Only one type found in the inventory
            return itemTypes.First();
        }
        else if (itemTypes.Count == 0)
        {
            // No items in the inventory
            return InventoryType.Undefined;
        }
        else
        {
            // Multiple types found, decide how to handle it
            // For this example, let's prioritize ActiveSkill > PassiveSkill > Weapon
            if (itemTypes.Contains(InventoryType.ActiveSkill))
            {
                Debug.Log("ActiveSKill");
                return InventoryType.ActiveSkill;
            }
            else if (itemTypes.Contains(InventoryType.PassiveSkill))
            {
                Debug.Log("PassiveSKill");
                return InventoryType.PassiveSkill;
            }
            else if (itemTypes.Contains(InventoryType.Weapon))
            {
                return InventoryType.Weapon;
            }
            else
            {
                // No recognized type found among multiple types
                // You might want to log a warning or throw an exception
                Debug.Log("No recognized type");
                return InventoryType.MultipleTypes;
            }
        }
    }
    private InventoryType GetItemType(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.WEAPON:
                return InventoryType.Weapon;
            case ItemType.ACTIVE:
                return InventoryType.ActiveSkill;
            case ItemType.PASSIVE:
                return InventoryType.PassiveSkill;
            default:
                return InventoryType.Undefined;
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
}

[Serializable]
public struct InventoryItem
{
    public int quantity;
    public ItemSO item;
    public InventoryType ItemType => item != null ? item.ItemType : InventoryType.Undefined;
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
