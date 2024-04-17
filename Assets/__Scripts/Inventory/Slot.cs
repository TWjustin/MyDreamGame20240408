using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Slot
{
    [SerializeField] private ItemData item;
    [SerializeField] private int stackAmount;
    
    public ItemData Item => item;
    public int StackAmount => stackAmount;
    
    public Slot(ItemData source, int amount)    // constructor to make occupied slot
    {
        item = source;
        stackAmount = amount;
    }

    public Slot()   // constructor to make empty slot
    {
        ClearSlot();
    }
    
    public void ClearSlot()
    {
        item = null;
        stackAmount = -1;
    }

    public void AssignItem(Slot slot)
    {
        if (item == slot.Item) AddToStack(slot.StackAmount);
        else
        {
            item = slot.Item;
            stackAmount = slot.StackAmount;
        }
    }
    
    
    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining)   // enough room left in stack
    {
        amountRemaining = item.maxStackSize - stackAmount;
        
        return RoomLeftInStack(amountToAdd);
    }
    
    public bool RoomLeftInStack(int amountToAdd)
    {
        if (stackAmount + amountToAdd <= item.maxStackSize) return true;
        else return false;
    }
    
    public void SetSlot(ItemData data, int amount)   // update slot data
    {
        item = data;
        stackAmount = amount;
    }
    
    public void AddToStack(int amount)
    {
        stackAmount += amount;
    }
    
    public void RemoveFromStack(int amount)
    {
        stackAmount -= amount;
    }

    public bool SplitStack(out Slot splitStack)
    {
        if (stackAmount <= 1)
        {
            splitStack = null;
            return false;
        }
        
        int halfStack = stackAmount / 2;
        bool isOdd = stackAmount % 2 != 0;
        if (isOdd) halfStack++;
        
        RemoveFromStack(halfStack);
        
        splitStack = new Slot(item, halfStack); // create new slot with half of the stack
        return true;
    }
}
