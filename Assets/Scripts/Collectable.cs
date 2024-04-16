using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private Rigidbody rb;
    private SphereCollider sphereCollider;
    
    private bool grounded;
    
    public float attractSpeed = 5f;
    public float attractSpeedWhileRunning = 6.5f;
    public float pickUpRadius = 0.3f;
    public float rotationSpeed = 50f;
    
    public ItemData itemData;
    private PlayerInventoryHolder inventory;  // 待改
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        sphereCollider = transform.GetChild(0).GetComponent<SphereCollider>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            grounded = true;
        }
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            grounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && grounded) inventory = other.GetComponent<PlayerInventoryHolder>();
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player") && inventory.CheckAvailable(itemData, 1))
        {
            rb.useGravity = false;
            sphereCollider.enabled = false;
                
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            attractSpeed = (playerMovement.currentSpeed == playerMovement.runSpeed) ? attractSpeedWhileRunning : attractSpeed;
            
            transform.position = Vector3.MoveTowards(transform.position, other.transform.GetChild(1).position,
                attractSpeed * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, other.transform.GetChild(1).position) < pickUpRadius)//
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rb.useGravity = true;
            sphereCollider.enabled = true;
        }
    }

    private void Update()
    {
        if (grounded)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }

    private void OnDestroy()
    {
        if (inventory)
        {
            inventory.AddToInventory(itemData, 1);
        }
    }
    
    
}
