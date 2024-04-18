using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicInstanceInventory : MonoBehaviour
{
    public InventorySizeData inventorySizeData;
    
    private InventoryType inventoryType;
    private InventorySO inventorySO;
    
    
    private void Awake()
    {
        inventorySO = inventorySizeData.InventoryUnitInit(inventoryType);
    }
}


