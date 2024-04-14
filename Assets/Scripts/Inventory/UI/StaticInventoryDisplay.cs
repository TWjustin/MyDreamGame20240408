using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private SlotUI[] slotUIs;
    
    protected override void Start()
    {
        base.Start();

        if (inventoryHolder != null)
        {
            inventorySystem = inventoryHolder.PrimaryInventorySystem;
            inventorySystem.OnSlotChanged += UpdateSlotUI;
        }
        else
        {
            Debug.LogWarning("No inventory holder assigned to " + gameObject.name);
        }
        
        AssignSlot(inventorySystem);
    }

    public override void AssignSlot(InventorySystem invToDisplay)
    {
        slotDictionary = new Dictionary<SlotUI, Slot>();

        if (slotUIs.Length != inventorySystem.InventorySize)
        {
            Debug.LogWarning("inventory slots out of sync on " + gameObject.name);
            return;
        }

        for (int i = 0; i < inventorySystem.InventorySize; i++)
        {
            slotDictionary.Add(slotUIs[i], inventorySystem.Slots[i]);
            slotUIs[i].Init(inventorySystem.Slots[i]);
        }
    }
}
