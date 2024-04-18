using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MouseObject : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI itemAmount;
    public Slot assignedSlot;

    private void Awake()
    {
        itemIcon.color = Color.clear;
        itemAmount.text = "";
    }

    public void SetMouseSlot(Slot slot)
    {
        assignedSlot.AssignItem(slot);  // ?
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
                ClearMouseItem();
                // TODO: drop item on ground
            }
        }
    }
    
    public void ClearMouseItem()
    {
        assignedSlot.ClearSlot();
        itemIcon.sprite = null;
        itemIcon.color = Color.clear;
        itemAmount.text = "";
    }
    
    public static bool IsPointerOverUIObject()  // utils
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }   
}
