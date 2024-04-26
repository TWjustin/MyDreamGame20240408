using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ToolResourcesTarget : Target
{
    
    public ToolType properToolType;
    
    public int maxHealth;
    [HideInInspector] public int currentHealth;

    private void Awake()
    {
        inputType = InputType.LeftClick;
    }

    protected void Start()
    {
        currentHealth = maxHealth;
    }

    public abstract override void UseItem(ToolItem toolItem);
    
}
