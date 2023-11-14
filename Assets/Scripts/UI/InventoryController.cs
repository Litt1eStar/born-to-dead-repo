using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private UIInvetoryPage inventoryUI;
    [SerializeField] private InventorySO activeSkillData;
    [SerializeField] private InventorySO passiveSkillData;

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
        UpdateInventoryUI(activeSkillData.GetCurrentInventoryState());
        UpdateInventoryUI(passiveSkillData.GetCurrentInventoryState());

        //Test Add Item
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            passiveSkillData.AddItem(passiveItem, 4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            activeSkillData.AddItem(activeItem, 4);
        }

    }
    private void PrepareInventoryData()
    {
        //Initialize InventorySlot based on Size of Inventory
        activeSkillData.Initialize();
        passiveSkillData.Initialize();
        //Updated InventoryUI
        activeSkillData.OnInventoryUpdated += UpdateInventoryUI;
        passiveSkillData.OnInventoryUpdated += UpdateInventoryUI;
        foreach (InventoryItem item in initializeItems)
        {
            if (!item.IsEmpty)
            {
                Debug.Log($"Add {item.item.Name} to InventoryData");
                activeSkillData.AddItem(item);
                passiveSkillData.AddItem(item);
                Debug.Log($"Amount of Initialize Item = {activeSkillData.inventoryItems.Count}");
            }

        }
    }

    private void UpdateInventoryUI(Dictionary<Dictionary<int, InventoryItem>, InventoryType> inventoryState)
    {
        foreach (var inventoryType in inventoryState.Values)
        {
            if (inventoryType == InventoryType.ActiveSkill)
            {
                foreach (var returnValue in inventoryState.Keys)
                {
                    foreach (var item in returnValue)
                    {
                        inventoryUI.UpdateActiveSkillInventoryData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                    }
                }
            }

            if (inventoryType == InventoryType.PassiveSkill)
            {
                foreach (var returnValue in inventoryState.Keys)
                {
                    foreach (var item in returnValue)
                    {
                        inventoryUI.UpdatePassiveSkillInventoryData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                    }
                }
            }
            return;
        }
    }

    private void PrepareUI()
    {
        inventoryUI.InitalizeActiveSkillInventoryUI(activeSkillData.Size);
        inventoryUI.InitalizePassiveSkillInventoryUI(passiveSkillData.Size);
/*      inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
        inventoryUI.OnSwapItems += HandleSwapItems;
        inventoryUI.OnStartDragging += HandleDragging;
        inventoryUI.OnItemActionRequested += HandleItemActionRequest;*/
    }

    private void HandleItemActionRequest(int itemIndex)
    {

    }

    private void HandleDragging(int itemIndex)
    {
        InventoryItem inventoryItem = activeSkillData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;

        inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
    }

    private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
    {
        activeSkillData.SwapItems(itemIndex_1, itemIndex_2);
    }

    private void HandleDescriptionRequest(int itemIndex)
    {
        InventoryItem inventoryItem = activeSkillData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
        {
            inventoryUI.ResetSelection();
            return;
        }

        ItemSO item = inventoryItem.item;
        inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, item.Description);
    }

}
