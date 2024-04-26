using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

// [CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory SO")]
public class InventorySO : ScriptableObject
{
    public int inventorySize;
    public List<Slot> slotList;
    
    public UnityAction<Slot> OnSlotChanged;
    
    public void InventoryUnitInit(int size)
    {
        inventorySize = size;
        
        slotList = new List<Slot>(size);
        for (int i = 0; i < size; i++)
        {
            slotList.Add(new Slot());
        }
    }
    
    public bool CheckAvailable(ItemData itemToAdd, int amountToAdd)
    {
        if (ContainsItem(itemToAdd, amountToAdd, out Slot stackSlot))
        {
            return true;
        }
        else if (HasFreeSlot(out Slot freeSlot))
        {
            return true;
        }
        else
        {
            Debug.Log(this.name + " is full");
            return false;
        }
    }

    public void AddItem(ItemData itemToAdd, int amountToAdd)
    {
        if (ContainsItem(itemToAdd, amountToAdd, out Slot stackSlot))  // item exists in inventory
        {
            stackSlot.AddToStack(amountToAdd);
            OnSlotChanged?.Invoke(stackSlot);
            return;
        }
        if (HasFreeSlot(out Slot freeSlot))    // get first free slot
        {
            freeSlot.SetSlot(itemToAdd, amountToAdd);
            OnSlotChanged?.Invoke(freeSlot);
            return;
        }
        
        Debug.Log("CheckAvailable not working?");
    }

    public bool ContainsItem(ItemData itemToAdd, int amountToAdd, out Slot stackSlot) // 取得包含物品的slot
    {
        stackSlot = slotList.FirstOrDefault(s => s.Item == itemToAdd && s.RoomLeftInStack(amountToAdd));
        return stackSlot != null;
    }
    
    public bool HasFreeSlot(out Slot freeSlot)  // 取得第一個空slot
    {
        freeSlot = slotList.FirstOrDefault(s => s.Item == null);
        return freeSlot != null;
    }

}
