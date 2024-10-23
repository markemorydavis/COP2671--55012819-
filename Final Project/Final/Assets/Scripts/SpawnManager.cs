using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public GameObject obstaclePrefab;

    public int powerUpCount = 3;
    public int obstacleCount = 10;

    private float spawnRangeX = 7;


    // Start is called before the first frame update
    void Start()
    {
        SpawnObjects(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        float spawnPosZ = Random.Range(20, 300);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;

    }
    public void SpawnObjects(int rate)
    {
        for (int i = 0; i < powerUpCount ; i++)
        {
            Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
        }
        for (int i = 0; i < obstacleCount + rate; i++)
        {
            Instantiate(obstaclePrefab, GenerateSpawnPosition(), obstaclePrefab.transform.rotation);
        }
    }
}
