using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour // 移到player
{
    public GridManager gridManager;
    
    public Transform frameCanvas;
    public Transform interactPoint;

    [HideInInspector] public int gridX;
    [HideInInspector] public int gridZ;
    [HideInInspector] public Vector3 gridPosition;  //
    
    public GridObject interactingGridObject;    //

    private void Update()
    {
        gridManager.grid.GetXZ(interactPoint.position, out gridX, out gridZ);
        interactingGridObject = gridManager.grid.GetValue(gridX, gridZ);
        
        int x = Mathf.FloorToInt(interactPoint.position.x);
        int z = Mathf.FloorToInt(interactPoint.position.z);
        Vector3 framePosition = new Vector3(x + 0.5f, 0.01f, z + 0.5f);
        frameCanvas.position = framePosition;

        gridPosition = new Vector3(x, 0, z);

        // Debug.Log(interactingGridObject.belongingObject.name);
    }
}
