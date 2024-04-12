using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    [field: SerializeField] public string itemName { get; set; }
    [field: SerializeField] [field: TextArea] public string itemDescription { get; set; }
    [field: SerializeField] public Sprite itemIcon { get; set; }
    public int ID => GetInstanceID();
    
    [field: SerializeField] public bool isStackable { get; set; }
    [field: SerializeField] public int maxStack { get; set; } = 1;
}
