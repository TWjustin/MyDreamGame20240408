using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreaturesTarget : Target
{
    
    public void Interact(Transform interactorTransform) { } // todo 改

    public abstract override void UseItem(ToolItem toolItem);

    private void Awake()
    {
        inputType = InputType.LeftClick;
    }
}
