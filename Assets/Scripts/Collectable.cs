using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private Rigidbody rb;
    
    
    public float attractSpeed = 5f;
    public float destroyDistance = 0.3f;
    public float rotationSpeed = 50f;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rb.useGravity = false;
            
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
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
