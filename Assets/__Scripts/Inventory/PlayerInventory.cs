using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    [HideInInspector] public InventorySO hotbarSO;
    [HideInInspector] public InventorySO backpackSO;
    public InventorySO debugHotbarSO;
    public InventorySO debugBackpackSO;
    
    public static UnityAction<InventorySO> OnDynamicInventoryDisplayRequested;

    private void Awake()
    {
        hotbarSO = DataSingleton.Instance.inventorySizeData.InventoryUnitInit(InventoryType.Hotbar);
        backpackSO = DataSingleton.Instance.inventorySizeData.InventoryUnitInit(InventoryType.Backpack);
        
        // Debug.Log(backpackSO.slotList.Count);
        
        // just for debugging
        debugHotbarSO = hotbarSO;
        debugBackpackSO = backpackSO;
    }

    public bool CheckAvailable(ItemData itemToAdd, int amountToAdd)
    {
        return hotbarSO.CheckAvailable(itemToAdd, amountToAdd) ||
               backpackSO.CheckAvailable(itemToAdd, amountToAdd);
    }

    public void AddItem(ItemData itemToAdd, int amountToAdd)
    {
        if (hotbarSO.CheckAvailable(itemToAdd, amountToAdd))
        {
            hotbarSO.AddItem(itemToAdd, amountToAdd);
        }
        else if (backpackSO.CheckAvailable(itemToAdd, amountToAdd))
        {
            backpackSO.AddItem(itemToAdd, amountToAdd);
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) OnDynamicInventoryDisplayRequested?.Invoke(backpackSO);

    }
}
