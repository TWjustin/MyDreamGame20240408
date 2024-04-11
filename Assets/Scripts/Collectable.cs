using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private Rigidbody rb;
    public SphereCollider sphereCollider;
    
    private bool grounded;
    
    public float attractSpeed = 5f;
    public float destroyDistance = 0.3f;
    public float rotationSpeed = 50f;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            grounded = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && grounded)
        {
            rb.useGravity = false;
            sphereCollider.enabled = false;
            
            transform.position = Vector3.MoveTowards(transform.position, other.transform.GetChild(1).position,
                attractSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, other.transform.GetChild(1).position) < destroyDistance)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        if (grounded)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }
}
