using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverPoint : MonoBehaviour
{
    private GridXZ<gridtest.GridObject> grid;

    public Transform hoverCanvas;
    private Vector3 hoverPosition;
    private gridtest.GridObject handlingGrid;   // 待使用

    private void Update()
    {
        int hoverX = Mathf.FloorToInt(transform.position.x);
        int hoverZ = Mathf.FloorToInt(transform.position.z);
        hoverPosition = new Vector3(hoverX + 0.5f, 0.01f, hoverZ + 0.5f);
        
        hoverCanvas.position = hoverPosition;
        handlingGrid = grid.GetValue(hoverPosition);
    }
}
