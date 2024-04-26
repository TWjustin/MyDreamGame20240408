using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Items/New Default")]
public class ItemData : ScriptableObject    // 保留
{
    public int ID => GetInstanceID();   // 改
    public Sprite icon;
    [SerializeField] private string itemEngName;
    [SerializeField] private string itemZhName;
    public string itemName => itemEngName;
    public ItemType itemType = ItemType.Default;
    [TextArea(4, 4)] public string description = "Item Description";
    public GameObject collectablePrefab;    // 新的
    
    [Header("Item Properties")]
    public bool stackable = true;
    public int maxStackSize => stackable ? 99 : 1;
    
    // public bool consumable;
    // public bool placeable;
    public int goldValue;
    
}

public enum ItemType
{
    Default,
    Tool,
    Consumable,
    Placeable,
    Clothing,
    Armor,
    Weapon,
    Functional,
    Equipment,
}

public enum FilterSort
{
    
}
