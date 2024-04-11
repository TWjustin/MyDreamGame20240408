using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class gridtest : MonoBehaviour
{
    public GridXZ<GridObject> grid;

    public int gridWidth = 50;
    public int gridHeight = 50;
    public float cellSize = 1f;

    private void Awake()
    {
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(-gridWidth / 2, 0, -gridHeight / 2));
    }

    public class GridObject
    {
        private GridXZ<GridObject> grid;
        private int x;
        private int z;

        public GridObject(GridXZ<GridObject> grid, int x, int z)
        {
            this.grid = grid;
            this.x = x;
            this.z = z;
        }
    }
}
