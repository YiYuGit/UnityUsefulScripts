using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple collision detection and will destroy both colliding objets.
/// Remember to put trigger collider on both objects and at least one object should have rigidbody, 
/// Uncheck use gravity if necessary
/// </summary>

public class DetectCollisions : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Destroy(other.gameObject);
    }
}
