using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory")]
public class InventorySO : ScriptableObject
{
    [SerializeField] private List<InventorySlot> slots;
    [field: SerializeField] public int size { get; private set; } = 10;
    
    public event Action<Dictionary<int, InventorySlot>> OnInventoryChanged; 

    public void Initialize()
    {
        slots = new List<InventorySlot>();
        for (int i = 0; i < size; i++)
        {
            slots.Add(InventorySlot.GetEmptySlot());
        }
    }

    public void AddItem(ItemSO item, int amount)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].isEmpty)
            {
                slots[i] = new InventorySlot()
                {
                    item = item,
                    amount = amount,
                };
                return;
            }
        }
    }
    
    public void AddItem(InventorySlot slot)
    {
        AddItem(slot.item, slot.amount);
    }
    
    public Dictionary<int, InventorySlot> GetCurrentlyHeldItems()
    {
        Dictionary<int, InventorySlot> heldItems = new Dictionary<int, InventorySlot>();
        for (int i = 0; i < slots.Count; i++)
        {
            if (!slots[i].isEmpty)
            {
                heldItems[i] = slots[i];
            }
        }
        return heldItems;
    }
    
    public InventorySlot GetItemAt(int index)
    {
        return slots[index];
    }
    
    public void SwapItems(int index1, int index2)
    {
        // if (index1 < 0 || index1 >= slots.Count || index2 < 0 || index2 >= slots.Count)
        // {
        //     return;
        // }
        (slots[index1], slots[index2]) = (slots[index2], slots[index1]);    // swap via deconstruction
        InformAboutChange();
    }
    
    private void InformAboutChange()
    {
        OnInventoryChanged?.Invoke(GetCurrentlyHeldItems());
    }
    
}

[Serializable]
public struct InventorySlot
{
    public ItemSO item;
    public int amount;
    public bool isEmpty => item == null;
    
    public InventorySlot ChangeQuantity(int newAmount)
    {
        return new InventorySlot()
        {
            item = this.item,
            amount = newAmount,
        };
    }
    
    public static InventorySlot GetEmptySlot()
        => new InventorySlot()
        {
            item = null,
            amount = 0,
        };
}