using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlacedObjectType", menuName = "Inventory/Items/Placed Object Type")]
public class PlacedObjectTypeSO : ItemData
{
    public enum Dir {
        Down,
        Left,
        Up,
        Right,
    }
    
    // public static Vector2Int GetDirForwardVector(Dir dir)
    // public static Dir GetDir(Vector2Int from, Vector2Int to)
    
    [Header("Placed Object Properties")]
    public Transform prefab;
    // public bool isGridObject;
    // public Transform visual;
    public int width = 1;
    public int height = 1;

    public static Dir GetDir(Vector3 vec)
    {
        float angle = Mathf.Atan2(vec.z, vec.x) * Mathf.Rad2Deg;
        angle = (angle + 360) % 360;
        
        if (angle >= 45 && angle < 135) {
            return Dir.Down;
        } else if (angle >= 135 && angle < 225) {
            return Dir.Right;
        } else if (angle >= 225 && angle < 315) {
            return Dir.Up;
        } else {
            return Dir.Left;
        }
    }
    
    public int GetRotationAngle(Dir dir) {
        switch (dir) {
            default:
            case Dir.Down:  return 0;
            case Dir.Left:  return 90;
            case Dir.Up:    return 180;
            case Dir.Right: return 270;
        }
    }
    
    public Vector2Int GetRotationOffset(Dir dir) {
        switch (dir) {
            default:
            case Dir.Down:  return new Vector2Int(0, 0);
            case Dir.Left:  return new Vector2Int(0, width);
            case Dir.Up:    return new Vector2Int(width, height);
            case Dir.Right: return new Vector2Int(height, 0);
        }
    }
    
    public List<Vector2Int> GetGridPositionList(Vector2Int offset, Dir dir) {
        List<Vector2Int> gridPositionList = new List<Vector2Int>();
        switch (dir) {
            default:
            case Dir.Down:
            case Dir.Up:
                for (int x = 0; x < width; x++) {
                    for (int y = 0; y < height; y++) {
                        gridPositionList.Add(offset + new Vector2Int(x, y));
                    }
                }
                break;
            case Dir.Left:
            case Dir.Right:
                for (int x = 0; x < height; x++) {
                    for (int y = 0; y < width; y++) {
                        gridPositionList.Add(offset + new Vector2Int(x, y));
                    }
                }
                break;
        }
        return gridPositionList;
    }

}
