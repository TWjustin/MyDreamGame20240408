using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GridManager : MonoBehaviour    // singleton?
{
    public GridXZ<GridObject> grid;

    public int gridWidth = 50;
    public int gridHeight = 50;
    public float cellSize = 1f;

    private void Awake()
    {
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(-gridWidth / 2, 0, -gridHeight / 2),
            (GridXZ<GridObject> g, int x, int z, float c) => new GridObject(g, x, z, c));
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position,
            new Vector3(gridWidth, 1, gridHeight) * cellSize);
    }
    
}

public class GridObject
{
    private GridXZ<GridObject> grid;
    public int x { get; private set; }
    public int z { get; private set; }
    private float cellSize;
    
    private Transform transform;
    
    // public GameObject belongingObject;
    private bool selected;  //

    public GridObject(GridXZ<GridObject> grid, int x, int z, float cellSize)
    {
        this.grid = grid;
        this.x = x;
        this.z = z;
    }

    public void SetTransform(Transform transform)
    {
        this.transform = transform;
        grid.TriggerGridObjectChanged(x, z);
    }
    
    public void ClearTransform()
    {
        transform = null;
        grid.TriggerGridObjectChanged(x, z);
    }
    
    public bool CanBuild()
    {
        return transform == null;
    }
    
    public void CreatePropertyDebugText()
    {
        string text = x + ", " + z + ", " + transform;
        TextMesh textMesh = UtilsClass.CreateWorldText(text, null,
            grid.GetWorldPosition(this) + new Vector3(cellSize, 0, cellSize) * 0.5f, 12, Color.black,
            TextAnchor.MiddleCenter);
        textMesh.transform.localScale = Vector3.one * .13f;
    }
        
    public BoundsInt GetBoundsInt()
    {
        return new BoundsInt(new Vector3Int(x, 0, z), new Vector3Int(1, 20, 1));
    }
}