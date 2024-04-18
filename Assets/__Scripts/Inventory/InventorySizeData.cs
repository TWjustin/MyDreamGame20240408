using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventorySizeData", menuName = "Inventory/Inventory Size Data")]
public class InventorySizeData : ScriptableObject
{
    public List<InventorySizePair> invenSizePairs = new List<InventorySizePair>();
    
    public int GetInventorySize(InventoryType inventoryType)
    {
        foreach (var pair in invenSizePairs)
        {
            if (pair.inventoryType == inventoryType)
            {
                return pair.inventorySize;
            }
        }

        return -1;
    }

    public InventorySO InventoryUnitInit(InventoryType inventoryType)
    {
        InventorySO inventorySO = ScriptableObject.CreateInstance<InventorySO>();
        inventorySO.InventoryUnitInit(GetInventorySize(inventoryType));
        return inventorySO;
    }
}

[System.Serializable]
public struct InventorySizePair
{
    public InventoryType inventoryType;
    public int inventorySize;
}

public enum InventoryType
{
    Hotbar,
    Backpack,
    Chest,
    BigChest,
    // Shop,
    // Crafting,
    NPC,
    // Enemy,
    // Boss
}
