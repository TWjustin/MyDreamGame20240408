using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject slotUIPrefab;
    
    [SerializeField] private RectTransform contentPanel;
    
    [SerializeField] private InventoryDescriptionUI itemDescription;
    
    private List<SlotUI> slotList = new List<SlotUI>();

    private void Awake()
    {
        Hide();
    }

    public void InitializeInventory(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            GameObject slotGO = Instantiate(slotUIPrefab, Vector3.zero, Quaternion.identity);
            SlotUI slotScript = slotGO.GetComponent<SlotUI>();
            
            slotGO.transform.SetParent(contentPanel);
            slotList.Add(slotScript);
            slotScript.OnItemClicked += HandleItemSelection;
            slotScript.OnItemBeginDrag += HandleBeginDrag;
            slotScript.OnItemDroppedOn += HandleSwap;
            slotScript.OnItemEndDrag += HandleEndDrag;
            slotScript.OnItemRightClicked += HandleShowItemActions;
        }
    }

    private void HandleItemSelection(SlotUI obj)
    {
        
    }
    
    private void HandleBeginDrag(SlotUI obj)
    {
        
    }
    
    private void HandleSwap(SlotUI obj)
    {
        
    }
    
    private void HandleEndDrag(SlotUI obj)
    {
        
    }
    
    private void HandleShowItemActions(SlotUI obj)
    {
        Debug.Log(obj.name);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
