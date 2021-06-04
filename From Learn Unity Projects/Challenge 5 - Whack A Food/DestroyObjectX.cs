using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is attached to particle system of explosion.
/// This will destroy the particle after 2 seconds.
/// </summary>

public class DestroyObjectX : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 2); // destroy particle after 2 seconds
    }

}
