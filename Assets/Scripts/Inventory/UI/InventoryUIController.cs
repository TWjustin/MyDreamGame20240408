using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay chestPanel;
    public DynamicInventoryDisplay playerBackpackPanel;

    private void Awake()
    {
        chestPanel.gameObject.SetActive(false);
        playerBackpackPanel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayFunctionalInventory;
        PlayerInventoryHolder.OnPlayerBackpackDisplayRequested += DisplayPlayerBackpack;
    }
    
    private void OnDisable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayFunctionalInventory;
        PlayerInventoryHolder.OnPlayerBackpackDisplayRequested -= DisplayPlayerBackpack;
    }

    void Update()
    {

        if (chestPanel.gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
            chestPanel.gameObject.SetActive(false);
        if (playerBackpackPanel.gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
            playerBackpackPanel.gameObject.SetActive(false);
    }
    
    private void DisplayFunctionalInventory(InventorySystem invToDisplay)
    {
        chestPanel.gameObject.SetActive(true);
        chestPanel.RefreshDynamicInventory(invToDisplay);
    }
    
    private void DisplayPlayerBackpack(InventorySystem invToDisplay)
    {
        playerBackpackPanel.gameObject.SetActive(true);
        playerBackpackPanel.RefreshDynamicInventory(invToDisplay);
    }
}
