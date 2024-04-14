using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;
    
    protected InventorySystem inventorySystem;
    protected Dictionary<SlotUI, Slot> slotDictionary;
    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<SlotUI, Slot> SlotDictionary => slotDictionary;

    protected virtual void Start()
    {
        
    }

    public abstract void AssignSlot(InventorySystem invToDisplay);  // implement in child classes
    
    protected virtual void UpdateSlotUI(Slot updatedSlot)
    {
        foreach (var slot in slotDictionary)
        {
            if (slot.Value == updatedSlot)
            {
                slot.Key.UpdateSlotUI(updatedSlot);
            }
        }
    }

    public void SlotClicked(SlotUI clickedSlotUI)
    {
        bool isSlotEmpty = clickedSlotUI.AssignedSlot.Item == null;
        bool isMouseEmpty = mouseInventoryItem.assignedSlot.Item == null;
        bool isLShiftPressed = Input.GetKey(KeyCode.LeftShift);
        
        // mouse slot is empty and clicked slot is not empty 取走
        if (!isSlotEmpty && isMouseEmpty)
        {
            // split stack
            if (isLShiftPressed && clickedSlotUI.AssignedSlot.SplitStack(out Slot halfStackSlot))    
            {
                mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
                clickedSlotUI.UpdateSlotUI();
                return;
            }
            // take all
            else
            {
                mouseInventoryItem.UpdateMouseSlot(clickedSlotUI.AssignedSlot);
                clickedSlotUI.ClearSlot();
                return;
            }
            
            
        }

        // mouse slot is not empty and clicked slot is empty 放回去
        if (isSlotEmpty && !isMouseEmpty)
        {
            clickedSlotUI.AssignedSlot.AssignItem(mouseInventoryItem.assignedSlot);
            clickedSlotUI.UpdateSlotUI();
            
            mouseInventoryItem.ClearSlot();
            return;
        }

        if (!isSlotEmpty && !isMouseEmpty)
        {
            bool isSameItem = clickedSlotUI.AssignedSlot.Item == mouseInventoryItem.assignedSlot.Item;
            
            // 互換
            if (!isSameItem)
            {
                SwapSlots(clickedSlotUI);
                return;
            }
            // 合併
            else if (isSameItem && clickedSlotUI.AssignedSlot.RoomLeftInStack(mouseInventoryItem.assignedSlot.StackAmount))
            {
                clickedSlotUI.AssignedSlot.AssignItem(mouseInventoryItem.assignedSlot);
                clickedSlotUI.UpdateSlotUI();
                
                mouseInventoryItem.ClearSlot();
                return;
            }
            else if (isSameItem &&
                     !clickedSlotUI.AssignedSlot.RoomLeftInStack(mouseInventoryItem.assignedSlot.StackAmount,
                         out int leftInStack))
            {
                // stack is full
                if (leftInStack < 1) SwapSlots(clickedSlotUI);  
                // 相加，mouse留剩下的
                else    
                {
                    int remainingOnMouse = mouseInventoryItem.assignedSlot.StackAmount - leftInStack;
                    
                    clickedSlotUI.AssignedSlot.AddToStack(leftInStack);
                    clickedSlotUI.UpdateSlotUI();
                    
                    var newItem = new Slot(mouseInventoryItem.assignedSlot.Item, remainingOnMouse);
                    mouseInventoryItem.ClearSlot();
                    mouseInventoryItem.UpdateMouseSlot(newItem);
                }
                return;
                
            }
        }
        
    }

    private void SwapSlots(SlotUI clickedSlot)
    {
        var clonedSlot = new Slot(mouseInventoryItem.assignedSlot.Item, mouseInventoryItem.assignedSlot.StackAmount);
        mouseInventoryItem.ClearSlot();
        
        mouseInventoryItem.UpdateMouseSlot(clickedSlot.AssignedSlot);
        
        clickedSlot.ClearSlot();
        clickedSlot.AssignedSlot.AssignItem(clonedSlot);
        clickedSlot.UpdateSlotUI();
    }
}
