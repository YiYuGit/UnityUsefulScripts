using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple collision detection and will destroy both colliding objets.
/// Remember to put trigger collider on both objects and at least one have rigidbody, 
/// Uncheck use gravity if necessary
/// </summary>

public class DetectCollisions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Destroy(other.gameObject);
    }
}
