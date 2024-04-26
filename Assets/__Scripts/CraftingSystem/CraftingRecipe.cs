using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemAmount
{
    public ItemData item;
    [Range(1, 99)]
    public int amount;
}

[CreateAssetMenu(fileName = "CraftingRecipe", menuName = "Inventory/Crafting/New Crafting Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public List<ItemAmount> materials;
    public List<ItemAmount> results;
    
    public bool CanCraft(PlayerInventory inventory)
    {
        foreach (ItemAmount itemAmount in materials)
        {
            // if (inventory.ItemCount(itemAmount.item) < itemAmount.amount)
            // {
            //     return false;
            // }
        }

        return true;
    }
    
    public void Craft(PlayerInventory inventory)
    {
        if (!CanCraft(inventory))
        {
            return;
        }

        foreach (ItemAmount itemAmount in materials)
        {
            for (int i = 0; i < itemAmount.amount; i++)
            {
                // inventory.RemoveItem(itemAmount.item);
            }
        }

        foreach (ItemAmount itemAmount in results)
        {
            for (int i = 0; i < itemAmount.amount; i++)
            {
                // inventory.AddItem(itemAmount.item);
            }
        }
    }
}
