using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuildingSystem : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GridManager gridManager;
    public GridSelection gridSelection;
    
    [SerializeField] private List<PlacedObjectTypeSO> placedObjectTypeSOList;
    private PlacedObjectTypeSO placedObjectTypeSO;
    private PlacedObjectTypeSO.Dir dir;

    private void Awake()
    {
        placedObjectTypeSO = placedObjectTypeSOList[0];
    }

    void Update()   // todo: gridX, gridZ
    {
        if (Input.GetKeyDown(KeyCode.P) && gridSelection.interactingGridObject != null) 
        {
            // GridObject offsetGrid = selectionManager.interactingGridObject;
            dir = PlacedObjectTypeSO.GetDir(playerMovement.moveDir);

            List<Vector2Int> gridPositionList =
                placedObjectTypeSO.GetGridPositionList(new Vector2Int(gridSelection.gridX, gridSelection.gridZ), dir);
            
            bool canBuild = true;
            foreach (var gridPosition in gridPositionList)
            {
                GridObject g = gridManager.grid.GetValue(gridPosition.x, gridPosition.y);
                if (!g.CanBuild())
                {
                    // cannot build here
                    canBuild = false;
                    break;
                }
            }
            
            if (canBuild)
            {
                Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
                Vector3 placedObjectWorldPosition =
                    gridManager.grid.GetWorldPosition(gridSelection.gridX, gridSelection.gridZ) + 
                    new Vector3(rotationOffset.x, 0, rotationOffset.y) * gridManager.cellSize;

                PlacedObject placedObject = PlacedObject.Create(placedObjectWorldPosition,
                    new Vector2Int(gridSelection.gridX, gridSelection.gridZ), placedObjectTypeSO, dir);
                
                foreach (var gridPosition in gridPositionList)
                {
                    GridObject g = gridManager.grid.GetValue(gridPosition.x, gridPosition.y);
                    g.SetPlacedObject(placedObject);
                    g.CreatePropertyDebugText();
                }
                
            }
            else
            {
                Debug.Log("Cannot build here");
            }
        }

        if (Input.GetKeyDown(KeyCode.O))    // 暫時
        {
            GridObject gridObject = gridManager.grid.GetValue(gridSelection.gridX, gridSelection.gridZ);
            PlacedObject placedObject = gridObject.GetPlacedObject();
            if (placedObject != null)
            {
                placedObject.DestroySelf();

                List<Vector2Int> gridPositionList = placedObject.GetGridPositionList();
                foreach (var gridPosition in gridPositionList)
                {
                    gridManager.grid.GetValue(gridPosition.x, gridPosition.y).ClearPlacedObject();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            // todo
            // dir = PlacedObjectTypeSO.GetNextDir(dir);
            // Debug.Log(dir);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) placedObjectTypeSO = placedObjectTypeSOList[0];
        if (Input.GetKeyDown(KeyCode.Alpha2)) placedObjectTypeSO = placedObjectTypeSOList[1];
        if (Input.GetKeyDown(KeyCode.Alpha3)) placedObjectTypeSO = placedObjectTypeSOList[2];
        if (Input.GetKeyDown(KeyCode.Alpha4)) placedObjectTypeSO = placedObjectTypeSOList[3];
        if (Input.GetKeyDown(KeyCode.Alpha5)) placedObjectTypeSO = placedObjectTypeSOList[4];
        
    }
}
