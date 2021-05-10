using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawn manager for the Jump Fence game
/// It will instantiate obstaclePrefab while game is not over
/// </summary>
public class SpawnManager : MonoBehaviour
{


    // Put obstacle here
    public GameObject obstaclePrefab;

    private Vector3 spawnPos = new Vector3(25, 0, 0);

    private float startDelay = 2f;

    private float repeatRate = 2f;

    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void SpawnObstacle()
    {
        // Stop spawn when game over
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
