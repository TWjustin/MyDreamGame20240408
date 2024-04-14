using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public int ID => GetInstanceID();
    public Sprite icon;
    public string itemName;
    [TextArea(4, 4)] public string description;
    public bool stackable = true;
    public int maxStackSize;
    public int goldValue;
}
