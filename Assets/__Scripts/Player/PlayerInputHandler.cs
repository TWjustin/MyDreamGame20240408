using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public PlayerInteractUI playerInteractUI;   // todo: 顛倒引用
    
    public float interactRange = 2f;
    
    // 長按
    private float pressTime;
    public float longPressThreshold = 1f;
    

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactRange))  // todo: grid
        {
            // Debug.Log(hit.collider.name);
            var hitTransform = hit.transform;
            hitTransform.TryGetComponent(out Target target);
            if (target != null)
            {
                playerInteractUI.Show(target, (int)target.inputType);
                
                // todo: 白色shader
                
                if (Input.GetMouseButtonDown(1))    // todo 取消UI
                {
                    target.Interact();
                }
                else if (Input.GetMouseButtonDown(0) && HotbarSelection.selectedItem is ToolItem)   // todo 待改
                {
                    target.UseItem(HotbarSelection.selectedItem as ToolItem);
                }
            }
            else
            {
                playerInteractUI.Hide();
            }
        }
        else
        {
            playerInteractUI.Hide();
        }
        
        
        // 長按吃東西
        if (Input.GetMouseButton(0))
        {
            pressTime += Time.deltaTime;
            if (pressTime > longPressThreshold)
            {
                // todo 吃?
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            pressTime = 0;
        }
        
    }

    
}
