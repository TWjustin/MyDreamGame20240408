using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tool", menuName = "Inventory/Items/New Tool")]
public class ToolItem : ItemData
{
    [Header("Tool Properties")]
    public ToolType toolType;
    public int strength;  // 強度，單次使用造成的傷害
    public int durability;  // 耐久度，ex使用一次減一

    private void Awake()
    {
        stackable = false;
        itemType = ItemType.Tool;
    }
}

public enum ToolType
{
    Default,
    Axe,
    Pickaxe,
    Hoe,    // 鋤頭
    Scythe, // 鐮刀
    // Sword, Spear
    Shovel,
    Hammer
}