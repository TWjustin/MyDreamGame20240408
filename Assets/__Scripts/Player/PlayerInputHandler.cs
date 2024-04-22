using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour   // todo: 1.多人不要用Singleton 2.記得掛到物件上
{
    private Camera mainCamera;
    public Transform player;
    
    public float interactRange = 2f;
    
    // 長按
    private float pressTime;
    public float longPressThreshold = 1f;

    #region Singleton

    public static PlayerInputHandler Instance { get; set; }
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

    #endregion

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        // RaycastHit hit;
        if (Physics.Raycast(player.position, player.forward, 
                out RaycastHit hit, interactRange))
        {
            // Debug.Log(hit.collider.name);
            var hitTransform = hit.transform;
            // todo: 白色shader
            IInteractable interactable = GetInteractableObject(hitTransform);
            if (Input.GetMouseButtonDown(1) && interactable != null)
            {
                // interactable.Interact(transform);
            }
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            // 瞄準先?
            switch (HotbarSelection.selectedItem.itemType)
            {
                case ItemType.Tool:
                    // 使用工具
                    break;
                case ItemType.Placeable:
                    // 放置物品
                    break;
                case ItemType.Weapon:
                    // 攻擊
                    break;
                // 打草、樹枝...
            }
        }
        else if (Input.GetMouseButton(0))
        {
            pressTime += Time.deltaTime;
            if (pressTime > longPressThreshold)
            {
                // 吃?
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            pressTime = 0;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            // 交互
            // 使用器材
            // 撿拾
        }
    }

    public IInteractable GetInteractableObject(Transform objTransform)
    {
        objTransform.TryGetComponent(out IInteractable interactable);
        return interactable;
    }
}
