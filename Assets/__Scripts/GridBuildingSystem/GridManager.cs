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
            (GridXZ<GridObject> g, int x, int z, float c) => new GridObject(g, x, z, c));
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position,
            new Vector3(gridWidth, 1, gridHeight) * cellSize);
    }
    
}
