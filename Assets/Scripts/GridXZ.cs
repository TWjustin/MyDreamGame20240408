using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GridXZ<TGridObject>
{
    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs {
        public int x;
        public int z;
    }
    
    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private TGridObject[,] gridArray;
    // private TextMesh[,] debugTextArray;
    
    public GridXZ(int width, int height, float cellSize, Vector3 originPosition, Func<GridXZ<TGridObject>, int, int, float, TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height];
        // debugTextArray = new TextMesh[width, height];
        
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int z = 0; z < gridArray.GetLength(1); z++)
            {
                gridArray[x, z] = createGridObject(this, x, z, cellSize);
            }
        }

        // debug
        
        // for (int x = 0; x < gridArray.GetLength(0); x++)
        // {
        //     for (int z = 0; z < gridArray.GetLength(1); z++)
        //     {
        //         debugTextArray[x, z] = UtilsClass.CreateWorldText(gridArray[x, z]?.ToString(), null,
        //             GetWorldPosition(x, z) + new Vector3(cellSize, 0, cellSize) * 0.5f, 20, Color.black,
        //             TextAnchor.MiddleCenter);
        //         debugTextArray[x, z].transform.localScale = Vector3.one * .13f;
        //         debugTextArray[x, z].transform.eulerAngles = new Vector3(90, 0, 0);
        //         Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), Color.black, 100f);
        //         Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), Color.black, 100f);
        //         
        //         // Debug.Log(gridArray[x, z]);
        //     }
        // }
        // Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.black, 100f);
        // Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.black, 100f);
        
        Debug.DrawLine(GetWorldPosition(0, 0), GetWorldPosition(width, 0), Color.black, 100f);
        Debug.DrawLine(GetWorldPosition(0, 0), GetWorldPosition(0, height), Color.black, 100f);
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.black, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.black, 100f);

        // OnGridObjectChanged += (object sender, OnGridObjectChangedEventArgs eventArgs) =>
        // {
        //     debugTextArray[eventArgs.x, eventArgs.z].text = gridArray[eventArgs.x, eventArgs.z]?.ToString();
        // };
        
    }
    

    public void TriggerGridObjectChanged(int x, int z)
    {
        // debugTextArray[x, z].text = gridArray[x, z].ToString();
        OnGridObjectChanged?.Invoke(this, new OnGridObjectChangedEventArgs { x = x, z = z });
    }
    
    public int GetWidth() {
        return width;
    }

    public int GetHeight() {
        return height;
    }

    public float GetCellSize() {
        return cellSize;
    }
    
    public Vector3 GetWorldPosition(int x, int z)   // 重新命名
    {
        return new Vector3(x, 0, z) * cellSize + originPosition;
    }
    
    public Vector3 GetWorldPosition(GridObject gridObject)
    {
        return GetWorldPosition(gridObject.x, gridObject.z);
    }
    
    public Vector3 GetCenterPositionInGrid(int x, int z)
    {
        return GetWorldPosition(x, z) + new Vector3(cellSize, 0, cellSize) * 0.5f;
    }

    public Vector3 GetCenterPositionInGrid(Vector3 worldPosition)
    {
        GetXZ(worldPosition, out int x, out int z);
        return GetCenterPositionInGrid(x, z);
    }
    
    public void GetXZ(Vector3 worldPosition, out int x, out int z)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        z = Mathf.FloorToInt((worldPosition - originPosition).z / cellSize);
    }
    
    public void SetValue(int x, int z, TGridObject value)   // 重新命名
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
        {
            gridArray[x, z] = value;
            // debugTextArray[x, z].text = gridArray[x, z].ToString();
            TriggerGridObjectChanged(x, z);
        }
    }
    
    public void SetValue(Vector3 worldPosition, TGridObject value)
    {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        SetValue(x, z, value);
    }
    
    public TGridObject GetValue(int x, int z)
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
        {
            return gridArray[x, z];
        }
        else
        {
            return default(TGridObject);
        }
    }
    
    public TGridObject GetValue(Vector3 worldPosition)
    {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        return GetValue(x, z);
    }
}
