using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The enemy object will follow the player by calculating the look direction
/// And the enemy will destroy when fall of ground.
/// </summary>

public class Enemy : MonoBehaviour
{
    public float speed;

    private Rigidbody enemyRb;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();

        player = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;

        enemyRb.AddForce( lookDirection * speed);

        if ( transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
