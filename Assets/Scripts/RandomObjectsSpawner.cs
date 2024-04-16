using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectsSpawner : MonoBehaviour
{
    public GridManager gridManager;
    
    // public Vector3 generationAreaSize = new Vector3(100f, 1f, 100f);
    
    public Transform containerPrefab;
    public List<PrefabAmountPair> prefabList;
    
    [System.Serializable]
    public struct PrefabAmountPair
    {
        public Transform prefab;
        public int amount;
    }
    
    
    void Start()
    {
        foreach (PrefabAmountPair pair in prefabList)
        {
            Transform container = Instantiate(containerPrefab);
            container.name = pair.prefab.name + " Container";
            Generate(pair.prefab, pair.amount, container.transform);
        }
    }
    
    
    
    void Generate(Transform prefab, int prefabAmount,Transform parentContainer)
    {
        for (int i = 0; i < prefabAmount; i++)
        {
            GridObject randomGridObject = GetRandomGridObject();
            Vector3 randomPosition = GetRandomSpawnPositionInGrid(randomGridObject);
            Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            
            Transform prefabInstance = Instantiate(prefab, randomPosition, randomRotation, parentContainer);
            randomGridObject.SetTransform(prefabInstance);
        }
    }
    
    Vector3 GetRandomSpawnPositionInGrid(GridObject obj)  // 需先獲得GridObject
    {
        
        
        // randomPosition.y = Terrain.activeTerrain.SampleHeight(randomPosition);
        return gridManager.grid.GetCenterPositionInGrid(obj.x, obj.z);
    }
    
    GridObject GetRandomGridObject()
    {
        int x = Random.Range(0, gridManager.gridWidth);
        int z = Random.Range(0, gridManager.gridHeight);
        return gridManager.grid.GetValue(x, z);
    }
    
    
    
}
