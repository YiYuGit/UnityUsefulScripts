using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  This script move the obstacles to the left side, the back ground also use the same script
/// </summary>

public class MoveLeft : MonoBehaviour
{
    [SerializeField]
    private float speed = 20f;
    private PlayerController playerControllerScript;

    private float leftBound = -15f;


    // Start is called before the first frame update
    void Start()
    {
        // Get component from player using Find name
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // When game over, stop moving left
        if (playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        
        // Destroy object when out of boundary
        if(transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
