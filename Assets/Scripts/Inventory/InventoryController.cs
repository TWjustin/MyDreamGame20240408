using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private InventorySO inventoryData;
    
    public List<InventorySlot> initialSlots = new List<InventorySlot>();
    

    private void Start()
    {
        PrepareUI();
        PrepareInventoryData();
    }

    private void PrepareUI()
    {
        inventoryUI.InitializeInventory(inventoryData.size);
        inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
        inventoryUI.OnSwapItems += HandleSwapItems;
        inventoryUI.OnStartDragging += HandleDragging;
        inventoryUI.OnItemActionRequested += HandleItemActionRequest;
    }
    
    private void PrepareInventoryData()
    {
        inventoryData.Initialize();
        inventoryData.OnInventoryChanged += UpdateInventoryUI;
        foreach (var slot in initialSlots)
        {
            if (slot.isEmpty)
            {
                continue;
            }
            inventoryData.AddItem(slot);
        }
    }

    private void UpdateInventoryUI(Dictionary<int, InventorySlot> inventoryState)
    {
        inventoryUI.ResetAllSlots();
        foreach (var item in inventoryState)
        {
            inventoryUI.UpdateData(item.Key, item.Value.item.itemIcon, item.Value.amount);
        }
    }
    
    private void HandleDescriptionRequest(int itemID)
    {
        InventorySlot slot = inventoryData.GetItemAt(itemID);
        if (slot.isEmpty)
        {
            inventoryUI.ResetSelection();
            return;
        }
        ItemSO item = slot.item;
        inventoryUI.UpdateDescription(itemID, item.itemIcon, item.itemName, item.itemDescription);
    }
    
    private void HandleSwapItems(int itemID_1, int itemID_2)
    {
        inventoryData.SwapItems(itemID_1, itemID_2);
    }
    
    private void HandleDragging(int itemID)
    {
        InventorySlot slot = inventoryData.GetItemAt(itemID);
        if (slot.isEmpty)
        {
            return;
        }
        inventoryUI.CreateDraggedItem(slot.item.itemIcon, slot.amount);
    }
    
    private void HandleItemActionRequest(int itemID)
    {
        // inventoryData.UseItem(itemID);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();
                foreach (var item in inventoryData.GetCurrentlyHeldItems())
                {
                    inventoryUI.UpdateData(item.Key, item.Value.item.itemIcon, item.Value.amount);
                }
            }
            else
            {
                inventoryUI.Hide();
            }
            
        }
    }
}
