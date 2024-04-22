using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "Inventory/Items/New Food")]
public class FoodItem : ItemData
{
    public bool quickConsume;
    
    public int restoreEnergyValue;

    private void Awake()
    {
        itemType = ItemType.Consumable;
    }
}
