using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private SlotUI[] slotUIs;
    
    protected override void Start()
    {
        base.Start();

        if (playerInventory != null)
        {
            inventorySO = playerInventory.hotbarSO;
            inventorySO.OnSlotChanged += UpdateSlotUIList;
        }
        else
        {
            Debug.LogWarning("No inventory holder assigned to " + gameObject.name);
        }
        
        AssignSlotList(inventorySO);
    }

    public override void AssignSlotList(InventorySO invToDisplay)
    {
        slotDictionary = new Dictionary<SlotUI, Slot>();

        if (slotUIs.Length != inventorySO.inventorySize)
        {
            Debug.LogWarning("inventory slots out of sync on " + gameObject.name);
            return;
        }

        for (int i = 0; i < inventorySO.inventorySize; i++)
        {
            slotDictionary.Add(slotUIs[i], inventorySO.slotList[i]);
            slotUIs[i].AssignSlot(inventorySO.slotList[i]);
        }
    }
}
