using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The enemy object will follow the player by calculating the look direction
/// And the enemy will be destroyed when fall off ground.
/// </summary>

public class Enemy : MonoBehaviour
{
    public float speed;

    private Rigidbody enemyRb;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // Get rigidbody component
        enemyRb = GetComponent<Rigidbody>();

        // Find Player
        player = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the look direction to player
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;

        // Add force on enemy rigidbody toward the player
        enemyRb.AddForce( lookDirection * speed);

        // Destroy the enemy object when fall off the ground
        if ( transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
