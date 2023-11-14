using System;
using System.Collections.Generic;
using UnityEngine;

public class UIInvetoryPage : MonoBehaviour
{
    [SerializeField] private UIInvetoryItem activeSkillItemPrefab; //Prefab of itemUI
    [SerializeField] private UIInvetoryItem passiveSkillItemPrefab; //Prefab of itemUI

    [SerializeField] private RectTransform activeSkillContentPanel;//Parent of SlotItem
    [SerializeField] private RectTransform passiveSkillContentPanel;//Parent of SlotItem

    [SerializeField] private UIInventoryDescription itemDescription;
    [SerializeField] private MouseFollower mouseFollower;

    private List<UIInvetoryItem> listOfActiveSkill_UIItem = new List<UIInvetoryItem>();
    private List<UIInvetoryItem> listOfPassiveSkill_UIItem = new List<UIInvetoryItem>();

    public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;
    public event Action<int, int> OnSwapItems;

    private int currentlyDraggedItemIndex = -1;
    private void Awake()
    {
        //Hide();
        //itemDescription.ResetDescription();
        mouseFollower.Toggle(false);
    }
    public void InitalizeActiveSkillInventoryUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            UIInvetoryItem uiItem = Instantiate(activeSkillItemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(activeSkillContentPanel);
            listOfActiveSkill_UIItem.Add(uiItem);
/*          uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDropeedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMouseButtonClick += HandleShowItemActions;*/
        }
    }

    public void InitalizePassiveSkillInventoryUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            UIInvetoryItem uiItem = Instantiate(passiveSkillItemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(passiveSkillContentPanel);
            listOfPassiveSkill_UIItem.Add(uiItem);
        }
    }
    //Update UI Data of Item
    public void UpdateActiveSkillInventoryData(int itemIndex, Sprite itemImage, int itemQuantity)
    {
        Debug.Log("List of active skill :: " + listOfActiveSkill_UIItem.Count);
        if (listOfActiveSkill_UIItem.Count > itemIndex)
        {
            listOfActiveSkill_UIItem[itemIndex].SetData(itemImage, itemQuantity);
        }
    }

    public void UpdatePassiveSkillInventoryData(int itemIndex, Sprite itemImage, int itemQuantity)
    {
        Debug.Log("List of passive skill :: " + listOfPassiveSkill_UIItem.Count);
        if (listOfPassiveSkill_UIItem.Count > itemIndex)
        {
            listOfPassiveSkill_UIItem[itemIndex].SetData(itemImage, itemQuantity);
        }
    }
    #region Event
    private void HandleShowItemActions(UIInvetoryItem item)
    {

    }

    private void HandleEndDrag(UIInvetoryItem item)
    {
        ResetDraggedItem();
    }

    private void HandleSwap(UIInvetoryItem item)
    {
        int index = listOfActiveSkill_UIItem.IndexOf(item);
        if (index == -1)
        {
            return;
        }

        OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
        HandleItemSelection(item);
    }

    private void ResetDraggedItem()
    {
        mouseFollower.Toggle(false);
        currentlyDraggedItemIndex = -1;
    }

    private void HandleBeginDrag(UIInvetoryItem item)
    {
        int index = listOfActiveSkill_UIItem.IndexOf(item);
        if (index == -1)
            return;

        currentlyDraggedItemIndex = index;
        HandleItemSelection(item);
        OnStartDragging?.Invoke(index);
    }

    public void CreateDraggedItem(Sprite sprite, int quantity)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(sprite, quantity);
    }

    private void HandleItemSelection(UIInvetoryItem item)
    {
        int index = listOfActiveSkill_UIItem.IndexOf(item);
        if (index == -1)
            return;
        OnDescriptionRequested?.Invoke(index);
    }

    #endregion
    public void Show()
    {
        gameObject.SetActive(true);
        ResetSelection();
    }

    public void ResetSelection()
    {
        //itemDescription.ResetDescription();
        DeselectAllItems();
    }

    private void DeselectAllItems()
    {
        foreach (UIInvetoryItem item in listOfActiveSkill_UIItem)
        {
            item.Deselect();
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        ResetDraggedItem();
    }

    public void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
    {
        //itemDescription.SetDescription(itemImage, name, description);
        DeselectAllItems();
        listOfActiveSkill_UIItem[itemIndex].Select();
    }

    public void ResetAllItems()
    {
        foreach (var item in listOfActiveSkill_UIItem)
        {
            item.ResetData();
            item.Deselect();
        }
    }
}
