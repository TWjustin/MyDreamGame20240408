using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarSelection : MonoBehaviour
{
    public PlayerInventoryHolder playerInventoryHolder;
    
    private int currentIndex;
    private Slot selectedSlot;
    private List<Slot> hotbarSlotList;
    private SlotUI[] hotbarSlotUIArray;
    private ItemData selectedItem;
    
    public GameObject selectionFrame;
    
    private void Start()
    {
        hotbarSlotList = playerInventoryHolder.primaryInventorySystem.Slots;
        hotbarSlotUIArray = GetComponentsInChildren<SlotUI>();
        
        Select(0);
    }

    private void Update()
    {
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        
        if (scrollDelta > 0f)
        {
            currentIndex = (currentIndex + 1) % hotbarSlotList.Count;
            Select(currentIndex);
        }
        else if (scrollDelta < 0f)
        {
            currentIndex = (currentIndex - 1 + hotbarSlotList.Count) % hotbarSlotList.Count;
            Select(currentIndex);
        }
    }

    private void Select(int id)
    {
        selectedSlot = hotbarSlotList[id];
        selectedItem = selectedSlot.Item;
        selectionFrame.transform.position = hotbarSlotUIArray[id].transform.position;
        selectionFrame.transform.SetParent(hotbarSlotUIArray[id].transform);
        
        // Debug.Log(selectedItem.name);
    }
}
