using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DynamicInstanceInventory : MonoBehaviour
{
    public InventoryType inventoryType;
    public InventorySO inventorySO;
    
    public static UnityAction<InventorySO> OnDynamicInventoryDisplayRequested;
    
    private void Awake()
    {
        inventorySO = DataSingleton.Instance.inventorySizeData.InventoryUnitInit(inventoryType);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            OnDynamicInventoryDisplayRequested?.Invoke(inventorySO);
        }
    }
}


