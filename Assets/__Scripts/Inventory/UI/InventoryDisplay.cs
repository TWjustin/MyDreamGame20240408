using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;
    
    protected InventorySystem inventorySystem;  // 繼承來指定
    protected Dictionary<SlotUI, Slot> slotDictionary;
    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<SlotUI, Slot> SlotDictionary => slotDictionary;

    protected virtual void Start()
    {
        
    }

    public abstract void AssignSlotList(InventorySystem invToDisplay);  // implement in child classes
    
    protected virtual void UpdateSlotUIList(Slot updatedSlot)   // 維持update字樣
    {
        foreach (var slot in slotDictionary)
        {
            if (slot.Value == updatedSlot)
            {
                slot.Key.SetSlotUI(updatedSlot);
            }
        }
    }

    public void SlotClicked(SlotUI clickedSlotUI)
    {
        bool isSlotEmpty = clickedSlotUI.AssignedSlot.Item == null;
        bool isMouseEmpty = mouseInventoryItem.assignedSlot.Item == null;
        bool isLShiftPressed = Input.GetKey(KeyCode.LeftShift); // 改
        
        // mouse slot is empty and clicked slot is not empty 取走
        if (!isSlotEmpty && isMouseEmpty)
        {
            // split stack
            if (isLShiftPressed && clickedSlotUI.AssignedSlot.SplitStack(out Slot halfStackSlot))    
            {
                mouseInventoryItem.SetMouseSlot(halfStackSlot);
                clickedSlotUI.SetSlotUI();
                return;
            }
            // take all
            else
            {
                mouseInventoryItem.SetMouseSlot(clickedSlotUI.AssignedSlot);
                clickedSlotUI.ClearSlotUI();
                return;
            }
            
            
        }

        // mouse slot is not empty and clicked slot is empty 放回去
        if (isSlotEmpty && !isMouseEmpty)
        {
            clickedSlotUI.AssignedSlot.AssignItem(mouseInventoryItem.assignedSlot);
            clickedSlotUI.SetSlotUI();
            
            mouseInventoryItem.ClearMouseItem();
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
                clickedSlotUI.SetSlotUI();
                
                mouseInventoryItem.ClearMouseItem();
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
                    clickedSlotUI.SetSlotUI();
                    
                    var newItem = new Slot(mouseInventoryItem.assignedSlot.Item, remainingOnMouse);
                    mouseInventoryItem.ClearMouseItem();
                    mouseInventoryItem.SetMouseSlot(newItem);
                }
                return;
                
            }
        }
        
    }

    private void SwapSlots(SlotUI clickedSlotUI)
    {
        var clonedSlot = new Slot(mouseInventoryItem.assignedSlot.Item, mouseInventoryItem.assignedSlot.StackAmount);
        mouseInventoryItem.ClearMouseItem();
        
        mouseInventoryItem.SetMouseSlot(clickedSlotUI.AssignedSlot);
        
        clickedSlotUI.ClearSlotUI();
        clickedSlotUI.AssignedSlot.AssignItem(clonedSlot);
        clickedSlotUI.SetSlotUI();
    }
}
