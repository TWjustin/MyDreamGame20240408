using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DynamicInventoryDisplay : InventoryDisplay
{
    [SerializeField] protected SlotUI slotUIPrefab;
    
    protected override void Start()
    {
        base.Start();
    }
    

    public void RefreshDynamicInventory(InventorySystem invToDisplay)
    {
        ClearSlots();
        inventorySystem = invToDisplay;
        if (inventorySystem != null) inventorySystem.OnSlotChanged += UpdateSlotUI;
        AssignSlot(invToDisplay);
    }

    public override void AssignSlot(InventorySystem invToDisplay)
    {
        ClearSlots();
        
        slotDictionary = new Dictionary<SlotUI, Slot>();

        if (invToDisplay == null)
        {
            Debug.LogWarning("No inventory system assigned to " + gameObject.name);
            return;
        }
        
        for (int i = 0; i < invToDisplay.InventorySize; i++)
        {
            SlotUI newSlotUI = Instantiate(slotUIPrefab, transform);
            newSlotUI.Init(invToDisplay.Slots[i]);
            newSlotUI.UpdateSlotUI();
            slotDictionary.Add(newSlotUI, invToDisplay.Slots[i]);
        }
    }

    private void ClearSlots()
    {
        foreach (var item in transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }

        if (slotDictionary != null) slotDictionary.Clear();
    }

    private void OnDisable()
    {
        if (inventorySystem != null) inventorySystem.OnSlotChanged -= UpdateSlotUI;
    }
}
