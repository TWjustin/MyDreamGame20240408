using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectsSpawner : MonoBehaviour
{
    public Vector3 generationAreaSize = new Vector3(100f, 1f, 100f);
    
    public GameObject containerPrefab;
    public List<PrefabAmountPair> prefabList;
    
    [System.Serializable]
    public class PrefabAmountPair
    {
        public GameObject prefab;
        public int amount;
    }
    
    
    void Start()
    {
        foreach (PrefabAmountPair pair in prefabList)
        {
            GameObject container = Instantiate(containerPrefab);
            container.name = pair.prefab.name + " Container";
            Generate(pair.prefab, pair.amount, container.transform);
        }
    }
    
    
    
    void Generate(GameObject prefab, int numberOfPrefabInstances,Transform parentContainer)
    {
        for (int i = 0; i < numberOfPrefabInstances; i++)
        {
            Vector3 randomPosition = GetRandomPositionInGenerationArea();
            Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            Instantiate(prefab, randomPosition, randomRotation, parentContainer);
        }
    }
    
    Vector3 GetRandomPositionInGenerationArea()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(-generationAreaSize.x / 2, generationAreaSize.x / 2),
            0f,
            Random.Range(-generationAreaSize.z / 2, generationAreaSize.z / 2)
        );
        
        // randomPosition.y = Terrain.activeTerrain.SampleHeight(randomPosition);

        return transform.position + randomPosition;
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, generationAreaSize);
    }
}
