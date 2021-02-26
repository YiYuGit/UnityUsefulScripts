using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Spawn manager use InvokeRepeating to spawn prefab at prefab location, and can use Random.Range to change location or time
/// </summary>

public class SpawnManager : MonoBehaviour
{

    public GameObject[] animalPrefabs;

    private float spawnRangeX = 18f;

    private float spawnPosZ = 18f;

    private float startDelay = 2f;

    private float spawnInterval = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal",startDelay , spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
       /*
        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnRandomAnimal();

        }
       */
        
    }

    void SpawnRandomAnimal()
    {
        int animalIndex = Random.Range(0, animalPrefabs.Length);

        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);

        Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);
    }
}
