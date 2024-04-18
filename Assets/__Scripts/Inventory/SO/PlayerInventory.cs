using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventorySizeData inventorySizeData;
    
    public InventorySO hotbarSO;
    private InventorySO backpackSO;

    private void Awake()
    {
        hotbarSO = inventorySizeData.InventoryUnitInit(InventoryType.Hotbar);
        backpackSO = inventorySizeData.InventoryUnitInit(InventoryType.Backpack);
        
        Debug.Log(hotbarSO.slotList.Count);
    }

    public bool CheckAvailable(ItemData itemToAdd, int amountToAdd)
    {
        return hotbarSO.CheckAvailable(itemToAdd, amountToAdd) ||
               backpackSO.CheckAvailable(itemToAdd, amountToAdd);
    }

    public void AddToInventory(ItemData itemToAdd, int amountToAdd)
    {
        if (hotbarSO.CheckAvailable(itemToAdd, amountToAdd))
        {
            hotbarSO.AddToInventory(itemToAdd, amountToAdd);
        }
        else if (backpackSO.CheckAvailable(itemToAdd, amountToAdd))
        {
            backpackSO.AddToInventory(itemToAdd, amountToAdd);
        }
    }
}
