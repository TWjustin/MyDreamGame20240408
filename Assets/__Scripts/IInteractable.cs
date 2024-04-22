using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact(Transform interactorTransform);   // NPC交互、使用器材、撿拾
    string GetInteractText();
    Transform GetTransform();
}
