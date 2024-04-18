using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicInstanceInventory : MonoBehaviour
{
    public InventorySizeData inventorySizeData;
    
    public InventoryType inventoryType;
    private InventorySO inventorySO;
    
    // create
    private void Awake()
    {
        inventorySO = inventorySizeData.InventoryUnitInit(inventoryType);
    }
}


