using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverPoint : MonoBehaviour // 名字待改
{
    public GridManager gridManager;
    
    public Transform selectionCanvas;
    public Transform interactPoint;
    
    
    private GridObject interactingGridObject;   // 待使用

    private void Update()
    {
        
        interactingGridObject = gridManager.grid.GetValue(interactPoint.position);
        Vector3 canvasPosition = gridManager.grid.GetWorldPosition(interactingGridObject.x, interactingGridObject.z);
        canvasPosition += new Vector3(0.5f, 0.01f, 0.5f);
        selectionCanvas.position = canvasPosition;


        Debug.Log(interactingGridObject.ToString());
    }
}
