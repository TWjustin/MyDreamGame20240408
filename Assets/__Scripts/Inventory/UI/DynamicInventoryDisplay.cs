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
    

    public void RefreshDynamicInventory(InventorySO invToDisplay)
    {
        ClearSlots();
        inventorySO = invToDisplay;
        if (inventorySO != null) inventorySO.OnSlotChanged += UpdateSlotUIList;
        AssignSlotList(invToDisplay);
    }

    public override void AssignSlotList(InventorySO invToDisplay)
    {
        ClearSlots();
        
        slotDictionary = new Dictionary<SlotUI, Slot>();

        if (invToDisplay == null)
        {
            Debug.LogWarning("No inventory system assigned to " + gameObject.name);
            return;
        }
        
        for (int i = 0; i < invToDisplay.inventorySize; i++)
        {
            SlotUI newSlotUI = Instantiate(slotUIPrefab, transform);
            newSlotUI.AssignSlot(invToDisplay.slotList[i]);
            newSlotUI.SetSlotUI();
            slotDictionary.Add(newSlotUI, invToDisplay.slotList[i]);
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
        if (inventorySO != null) inventorySO.OnSlotChanged -= UpdateSlotUIList;
    }
}
