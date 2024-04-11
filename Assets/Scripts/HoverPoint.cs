using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverPoint : MonoBehaviour // 名字待改
{
    public GridManager gridManager;
    
    public Transform hoverCanvas;
    public Transform player;
    
    private int handlingX, handlingZ;
    private Vector3 hoverPosition;
    public float forNegXZ = 2f;
    
    private GridManager.GridObject handlingGridObject;   // 待使用 // 待改

    private void Update()
    {
        Vector3 spotPos = player.position;
        
        if (player.forward.x >= 0f)
        {
            spotPos += player.forward;
            handlingX = Mathf.FloorToInt(spotPos.x);
        }
        else
        {
            spotPos += player.forward * forNegXZ;
            handlingX = Mathf.CeilToInt(spotPos.x);
        }
        if (player.forward.z >= 0f)
        {
            spotPos += player.forward;
            handlingZ = Mathf.FloorToInt(spotPos.z);
        }
        else
        {
            spotPos += player.forward * forNegXZ;
            handlingZ = Mathf.CeilToInt(spotPos.z);
        }
        
        
        handlingGridObject = gridManager.grid.GetValue(new Vector3(handlingX, 0, handlingZ));
        hoverCanvas.position = new Vector3(handlingX + 0.5f, 0.01f, handlingZ + 0.5f);
        
        
        // Debug.Log(handlingGridObject.ToString());
    }
}
