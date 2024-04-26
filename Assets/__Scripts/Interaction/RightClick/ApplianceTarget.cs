using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ApplianceTarget : Target
{
    public abstract override void Interact();
    
    private void Awake()
    {
        inputType = InputType.RightClick;
    }
    
}
