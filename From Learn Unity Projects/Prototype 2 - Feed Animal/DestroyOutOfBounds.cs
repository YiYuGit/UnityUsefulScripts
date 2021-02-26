using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script will detect if game object if out of boundary,and destroy gameobject, it can be changed to x y z 
/// Either detect + or - using Mathf.Abs  or use if/else if
/// 
/// </summary>
public class DestroyOutOfBounds : MonoBehaviour
{
    private float topBound = 30;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Mathf.Abs(transform.position.z) > topBound)
        {
            Destroy(gameObject);
        }
        */

        // If object go over topbound it get destroyed
        if (transform.position.z > topBound)
        {
            Destroy(gameObject);
        }
        else if (transform.position.z < -topBound)
        {
            // If animal go over -topbound, game is over and destroy object

            Debug.Log("Game Over!");
            Destroy(gameObject);

        }
    }
}
