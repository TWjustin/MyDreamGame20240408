using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "Inventory/Items/New Food")]
public class FoodItem : ItemData
{
    public int restoreEnergyValue;

    private void Awake()
    {
        itemType = ItemType.Consumable;
    }
}
