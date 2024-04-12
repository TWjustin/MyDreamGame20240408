using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    private Canvas canvas;
    private SlotUI slotUI;

    private void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();
        slotUI = GetComponentInChildren<SlotUI>();
    }
    
    public void SetData(Sprite itemSprite, int amount)
    {
        slotUI.SetData(itemSprite, amount);
    }

    private void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition,
            canvas.worldCamera, out localPoint);
        transform.position = canvas.transform.TransformPoint(localPoint);
    }
    
    public void ToggleVisibility(bool visible)
    {
        gameObject.SetActive(visible);
    }
}
