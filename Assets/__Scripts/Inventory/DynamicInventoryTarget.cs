using UnityEngine.Events;

public class DynamicInventoryTarget : ApplianceTarget
{
    public InventoryType inventoryType;
    private InventorySO inventorySO;
    
    public static UnityAction<InventorySO> OnDynamicInventoryDisplayRequested;  // todo 合併
    
    private void Awake()
    {
        inventorySO = DataSingleton.Instance.inventorySizeData.InventoryUnitInit(inventoryType);
    }
    
    public override void Interact()
    {
        OpenChest();
    }
    
    private void OpenChest()
    {
        OnDynamicInventoryDisplayRequested?.Invoke(inventorySO);
    }
}


