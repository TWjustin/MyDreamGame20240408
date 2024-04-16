using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GridXZ<GridObject> grid;

    public int gridWidth = 50;
    public int gridHeight = 50;
    public float cellSize = 1f;

    private void Awake()
    {
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(-gridWidth / 2, 0, -gridHeight / 2),
            (GridXZ<GridObject> g, int x, int z) => new GridObject(g, x, z));
    }
    
}

public class GridObject
{
    private GridXZ<GridObject> grid;
    public int x { get; private set; }
    public int z { get; private set; }
    // private PlacedObject placedObject;
    private GameObject belongingObject; //
    private bool selected;  //

    public GridObject(GridXZ<GridObject> grid, int x, int z)
    {
        this.grid = grid;
        this.x = x;
        this.z = z;
    }
        
    public override string ToString()
    {
        return x + ", " + z;
    }
        
    public BoundsInt GetBoundsInt()    // 加入引數
    {
        return new BoundsInt(new Vector3Int(x, 0, z), new Vector3Int(1, 20, 1));
    }
}