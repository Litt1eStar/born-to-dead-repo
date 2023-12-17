using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public UIInvetoryPage inventoryUI;
    public InventorySO activeSkillInventory;
    public InventorySO passiveSkillInventory;

    public ItemSO activeItem;
    public ItemSO passiveItem;
    public List<InventoryItem> initializeItems = new();
    private void Start()
    {
        //Prepare UI at start of game
        PrepareUI();
        
        //Prepare InventoryData at start of game
        PrepareInventoryData();
    }

    public void Update()
    {

        //Update InventoryUI Based on InventoryData
        UpdateInventoryUI(activeSkillInventory.GetCurrentInventoryState());
        UpdateInventoryUI(passiveSkillInventory.GetCurrentInventoryState());
    }
    private void PrepareInventoryData()
    {
        //Initialize InventorySlot based on Size of Inventory
        activeSkillInventory.Initialize();
        passiveSkillInventory.Initialize();
        //Updated InventoryUI
        activeSkillInventory.OnInventoryUpdated += UpdateInventoryUI;
        passiveSkillInventory.OnInventoryUpdated += UpdateInventoryUI;
        foreach (InventoryItem item in initializeItems)
        {
            if (!item.IsEmpty)
            {
                Debug.Log($"Add {item.item.Name} to InventoryData");
                activeSkillInventory.AddItem(item);
                passiveSkillInventory.AddItem(item);
                Debug.Log($"Amount of Initialize Item = {activeSkillInventory.inventoryItems.Count}");
            }

        }
    }

    private void UpdateInventoryUI(Dictionary<Dictionary<int, InventoryItem>, ItemType> inventoryState)
    {
        foreach (var inventoryType in inventoryState.Values)
        {
            if (inventoryType == ItemType.ActiveSkill)
            {
                foreach (var returnValue in inventoryState.Keys)
                {
                    foreach (var item in returnValue)
                    {
                        inventoryUI.UpdateActiveSkillInventoryData(item.Key, item.Value.item.sprite, item.Value.quantity);
                    }
                }
            }

            if (inventoryType == ItemType.PassiveSkill)
            {
                foreach (var returnValue in inventoryState.Keys)
                {
                    foreach (var item in returnValue)
                    {
                        inventoryUI.UpdatePassiveSkillInventoryData(item.Key, item.Value.item.sprite, item.Value.quantity);
                    }
                }
            }
            return;
        }
    }

    private void PrepareUI()
    {
        inventoryUI.InitalizeActiveSkillInventoryUI(activeSkillInventory.Size);
        inventoryUI.InitalizePassiveSkillInventoryUI(passiveSkillInventory.Size);
/*      inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
        inventoryUI.OnSwapItems += HandleSwapItems;
        inventoryUI.OnStartDragging += HandleDragging;
        inventoryUI.OnItemActionRequested += HandleItemActionRequest;*/
    }
}
