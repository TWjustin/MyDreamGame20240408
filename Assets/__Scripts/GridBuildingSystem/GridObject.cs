using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GridObject
{
    private GridXZ<GridObject> grid;
    public int x { get; private set; }
    public int z { get; private set; }
    private float cellSize;
    
    private PlacedObject placedObject;
    
    // public GameObject belongingObject;
    private bool selected;  //

    public GridObject(GridXZ<GridObject> grid, int x, int z, float cellSize)
    {
        this.grid = grid;
        this.x = x;
        this.z = z;
    }

    public void SetPlacedObject(PlacedObject placedObject)
    {
        this.placedObject = placedObject;
        grid.TriggerGridObjectChanged(x, z);
    }
    
    public PlacedObject GetPlacedObject()
    {
        return placedObject;
    }
    
    public void ClearPlacedObject()
    {
        placedObject = null;
        grid.TriggerGridObjectChanged(x, z);
    }
    
    public bool CanBuild()
    {
        return placedObject == null;
    }
    
    public void CreatePropertyDebugText()
    {
        string text = x + ", " + z + ", " + placedObject.name;
        TextMesh textMesh = UtilsClass.CreateWorldText(text, null,
            grid.GetWorldPosition(this) + new Vector3(cellSize, 0, cellSize) * 0.5f, 12, Color.black,
            TextAnchor.MiddleCenter);
        textMesh.transform.localScale = Vector3.one * .13f;
    }
        
    public BoundsInt GetBoundsInt() // 自己加的
    {
        return new BoundsInt(new Vector3Int(x, 0, z), new Vector3Int(1, 20, 1));
    }
}
