using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Transform cam;
    
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    [HideInInspector] public float currentSpeed;
    
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    [HideInInspector] public Vector3 moveDir;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;
        // Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        
        if (direction.magnitude >= 0.1f)
        {
            
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            currentSpeed = (Input.GetKey(KeyCode.R)) ? runSpeed : walkSpeed;
            
            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * currentSpeed * Time.deltaTime);
        }
        else
        {
            currentSpeed = 0;
        }
    }
}
