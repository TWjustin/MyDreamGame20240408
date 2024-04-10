using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    private CharacterController controller;
    private Transform cam;
    private Animator animator;
    
    public float speed = 6f;
    
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        
        if (direction.magnitude >= 0.1f)
        {
            animator.SetBool("isWalking", true);
            
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
