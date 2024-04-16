using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Items/New Default")]
public class ItemData : ScriptableObject
{
    public int ID => GetInstanceID();
    public Sprite icon;
    public string itemName;
    [TextArea(4, 4)] public string description;
    
    [Header("Item Properties")]
    public bool stackable = true;
    public int maxStackSize => stackable ? 99 : 1;
    
    // public bool consumable;
    public bool placeable;
    public int goldValue;
}
