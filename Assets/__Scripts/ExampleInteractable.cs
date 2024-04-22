using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;
    
    public void ExampleInteract()
    {
        Debug.Log("Interacting with " + gameObject.name);
    }
    
    public void Interact(Transform interactorTransform)
    {
        ExampleInteract();
    }
    
    public string GetInteractText()
    {
        return interactText;
    }
    
    public Transform GetTransform()
    {
        return transform;
    }
}

