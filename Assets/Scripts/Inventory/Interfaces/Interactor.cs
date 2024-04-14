using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Transform interactionPoint;
    public float interactionRange = 2f;
    public LayerMask interactionLayer;
    public bool isInteracting { get; private set; }

    private void Update()
    {
        var colliders = Physics.OverlapSphere(interactionPoint.position, interactionRange, interactionLayer);

        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                var interactable = colliders[i].GetComponent<IInteractable>();

                if (interactable != null) StartInteraction(interactable);
            }
        }
    }

    void StartInteraction(IInteractable interactable)
    {
        interactable.Interact(this, out bool interactSuccessful);
        isInteracting = true;
    }
    
    void EndInteraction(IInteractable interactable)
    {
        interactable.EndInteract();
        isInteracting = false;
    }
}
