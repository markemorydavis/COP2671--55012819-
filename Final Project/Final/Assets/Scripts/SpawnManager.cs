using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public GameObject obstaclePrefab;
    public GameObject starPrefab;

    public int powerUpCount = 5;
    public int obstacleCount = 10;

    private float spawnRangeX = 7;
    private List<GameObject> spawnedObjects = new List<GameObject>();


    // Update is called once per frame
    void Update()
    {
        
    }
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        float spawnPosZ = Random.Range(15, 300);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;

    }
    public void SpawnObjects(int rate)
    {
        for (int i = 0; i < powerUpCount ; i++)
        {
            GameObject spawnedPowerUp = Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
            spawnedObjects.Add(spawnedPowerUp);
        }
        if (rate > 0)
        {
            for (int i = 0; i < obstacleCount * rate; i++)
            {
                 GameObject spawnedObstacle = Instantiate(obstaclePrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
                 spawnedObjects.Add(spawnedObstacle);
            }
        }
        else
        {
            for (int i = 0; i < obstacleCount; i++)
            {
                GameObject spawnedObstacle = Instantiate(obstaclePrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
                spawnedObjects.Add(spawnedObstacle);
            }
        }
        GameObject spawnedStar = Instantiate(starPrefab, new Vector3(0,2,50), starPrefab.transform.rotation);
        spawnedObjects.Add(spawnedStar);
    }

    public void ClearAllPrefabs()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
            {
                DestroyImmediate(obj);
            }
        }

        // Clear list
        spawnedObjects.Clear();
    }


}
