using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDropHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Text itemAmountText;
    [SerializeField] private Image borderImage;
    
    public event Action<SlotUI> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnItemRightClicked;
    
    private bool empty = true;

    private void Awake()
    {
        ResetData();
        Deselect();
    }

    public void ResetData()
    {
        itemImage.gameObject.SetActive(false);
        empty = true;
    }

    public void Deselect()
    {
        borderImage.enabled = false;
    }
    
    public void SetData(Sprite itemSprite, int amount)
    {
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = itemSprite;
        itemAmountText.text = amount.ToString();
        empty = false;
    }
    
    public void Select()
    {
        borderImage.enabled = true;
    }
    

    public void OnPointerClick(PointerEventData pointerData)
    {
        if (pointerData.button == PointerEventData.InputButton.Left)
        {
            OnItemClicked?.Invoke(this);
        }
        else if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnItemRightClicked?.Invoke(this);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (empty)
        {
            return;
        }
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
