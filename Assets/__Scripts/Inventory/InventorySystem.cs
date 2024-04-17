using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<Slot> slots;
    public List<Slot> Slots => slots;
    public int InventorySize => slots.Count;
    
    public UnityAction<Slot> OnSlotChanged;
    
    public InventorySystem(int size)    // constructor sets inventory with empty slots
    {
        slots = new List<Slot>(size);
        
        for (int i = 0; i < size; i++)
        {
            slots.Add(new Slot());
        }
    }

    public bool CheckAvailable(ItemData itemToAdd, int amountToAdd)
    {
        if (ContainsItem(itemToAdd, amountToAdd, out Slot invSlot))
        {
            return true;
        }
        else if (HasFreeSlot(out Slot freeSlot))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddToInventory(ItemData itemToAdd, int amountToAdd)
    {
        if (ContainsItem(itemToAdd, amountToAdd, out Slot invSlot))  // item exists in inventory
        {
            
            invSlot.AddToStack(amountToAdd);
            OnSlotChanged?.Invoke(invSlot);
            return;
        }
        
        if (HasFreeSlot(out Slot freeSlot))    // get first free slot
        {
            freeSlot.SetSlot(itemToAdd, amountToAdd);
            OnSlotChanged?.Invoke(freeSlot);
            return;
        }
        
    }
    
    
    public bool ContainsItem(ItemData itemToAdd, int amountToAdd, out Slot invSlot) // 取得包含物品的slot
    {
        invSlot = slots.FirstOrDefault(s => s.Item == itemToAdd && s.RoomLeftInStack(amountToAdd));
        
        return invSlot != null;
    }
    
    public bool HasFreeSlot(out Slot freeSlot)  // 取得第一個空slot
    {
        freeSlot = slots.FirstOrDefault(s => s.Item == null);
        return freeSlot != null;
    }
}
