using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject slotUIPrefab;
    
    [SerializeField] private RectTransform contentPanel;
    
    [SerializeField] private InventoryDescriptionUI itemDescription;
    [SerializeField] private MouseFollower mouseFollower;
    
    private List<SlotUI> slotList = new List<SlotUI>();
    
    private int currentDraggedItemIndex = -1;

    public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;
    public event Action<int, int> OnSwapItems; 

    private void Awake()
    {
        Hide();
        mouseFollower.ToggleVisibility(false);
        itemDescription.RestDescription();
    }

    public void InitializeInventory(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            GameObject slotGO = Instantiate(slotUIPrefab, Vector3.zero, Quaternion.identity);
            SlotUI slotScript = slotGO.GetComponent<SlotUI>();
            
            slotGO.transform.SetParent(contentPanel);
            slotList.Add(slotScript);
            slotScript.OnItemClicked += HandleItemSelection;
            slotScript.OnItemBeginDrag += HandleBeginDrag;
            slotScript.OnItemDroppedOn += HandleSwap;
            slotScript.OnItemEndDrag += HandleEndDrag;
            slotScript.OnItemRightClicked += HandleShowItemActions;
        }
    }

    public void UpdateData(int itemIndex, Sprite itemSprite, int amount)
    {
        if (slotList.Count > itemIndex)
        {
            slotList[itemIndex].SetData(itemSprite, amount);
        }
    }

    private void HandleItemSelection(SlotUI obj)
    {
        int index = slotList.IndexOf(obj);
        if (index == -1)
        {
            return;
        }
        
        OnDescriptionRequested?.Invoke(index);
    }
    
    private void HandleBeginDrag(SlotUI obj)
    {
        int index = slotList.IndexOf(obj);
        if (index == -1)
        {
            return;
        }
        currentDraggedItemIndex = index;
        
        HandleItemSelection(obj);
        OnStartDragging?.Invoke(index);
    }
    
    private void HandleSwap(SlotUI obj)
    {
        int index = slotList.IndexOf(obj);
        if (index == -1)
        {
            return;
        }
        
        OnSwapItems?.Invoke(currentDraggedItemIndex, index);
    }
    
    private void HandleEndDrag(SlotUI obj)
    {
        ResetDraggedItem();
    }
    
    private void HandleShowItemActions(SlotUI obj)
    {
        
    }
    
    public void CreateDraggedItem(Sprite itemSprite, int amount)
    {
        mouseFollower.SetData(itemSprite, amount);
        mouseFollower.ToggleVisibility(true);
    }
    
    public void ResetDraggedItem()
    {
        mouseFollower.ToggleVisibility(false);
        currentDraggedItemIndex = -1;
    }

    public void ResetSelection()
    {
        itemDescription.RestDescription();
        DeselectAllItems();
    }
    
    public void ResetAllSlots()
    {
        foreach (var slot in slotList)
        {
            slot.ResetData();
            slot.Deselect();
        }
    }
    
    private void DeselectAllItems()
    {
        foreach (var slot in slotList)
        {
            slot.Deselect();
        }
    }
    
    public void UpdateDescription(int itemID, Sprite itemSprite, string itemName, string description)
    {
        itemDescription.SetDescription(itemSprite, itemName, description);
        DeselectAllItems();
        slotList[itemID].Select();
    }
    

    public void Show()
    {
        gameObject.SetActive(true);
        ResetSelection();
        
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
        ResetDraggedItem();
        
        Cursor.lockState = CursorLockMode.Locked;
    }
}
