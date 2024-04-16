using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventoryHolder : InventoryHolder
{
    [SerializeField] protected int backpackSize;
    [SerializeField] protected InventorySystem secondaryInventorySystem;
    
    public InventorySystem SecondaryInventorySystem => secondaryInventorySystem;
    
    public static UnityAction<InventorySystem> OnPlayerBackpackDisplayRequested;
    
    protected override void Awake()
    {
        base.Awake();
        secondaryInventorySystem = new InventorySystem(backpackSize);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) OnPlayerBackpackDisplayRequested?.Invoke(secondaryInventorySystem);
        
    }

    public bool CheckAvailable(ItemData data, int amount)
    {
        return primaryInventorySystem.CheckAvailable(data, amount) ||
               secondaryInventorySystem.CheckAvailable(data, amount);
    }
    
    public void AddToInventory(ItemData data, int amount)
    {
        if (primaryInventorySystem.CheckAvailable(data, amount))
        {
            primaryInventorySystem.AddToInventory(data, amount);
        }
        else if (secondaryInventorySystem.CheckAvailable(data, amount))
        {
            secondaryInventorySystem.AddToInventory(data, amount);
        }
        
    }
}
