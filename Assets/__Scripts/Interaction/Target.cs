using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour  // 右鍵interact 左鍵hit
{
    public string interactText;
    public InputType inputType;
    
    public virtual void Interact() { }   // Transform interactorTransform Codemonkey
    public virtual void UseItem(ToolItem toolItem) { }
    
}

public enum InputType
{
    LeftClick,
    RightClick,
}