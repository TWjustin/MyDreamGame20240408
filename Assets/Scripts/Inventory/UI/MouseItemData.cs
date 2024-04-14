using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MouseItemData : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI itemAmount;
    public Slot assignedSlot;

    private void Awake()
    {
        itemIcon.color = Color.clear;
        itemAmount.text = "";
    }

    public void UpdateMouseSlot(Slot slot)
    {
        assignedSlot.AssignItem(slot);
        itemIcon.sprite = slot.Item.icon;
        itemIcon.color = Color.white;
        itemAmount.text = slot.StackAmount.ToString();
    }

    private void Update()
    {
        if (assignedSlot.Item != null)  // if mouse has item, follow mouse
        {
            transform.position = Input.mousePosition;

            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
            {
                ClearSlot();
            }
        }
    }
    
    public void ClearSlot()
    {
        assignedSlot.ClearSlot();
        itemIcon.sprite = null;
        itemIcon.color = Color.clear;
        itemAmount.text = "";
    }
    
    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
