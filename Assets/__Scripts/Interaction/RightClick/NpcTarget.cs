using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NpcTarget : Target
{
    private void Awake()
    {
        inputType = InputType.RightClick;
    }

    public abstract override void Interact();
    
}
