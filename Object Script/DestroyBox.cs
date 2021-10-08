using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A more appropriate name should be "shatter box"script
/// This script content partially come from https://www.youtube.com/watch?v=EgNV0PWVaS8
/// Attach this to original prefab gameobject, and put the shattered version prefab of the same gameobject into the slot
/// On collision or collide with object with certain tag
/// The old object will be destroyed and instantiate a new shattered object at the same transform
/// The effect is object get destroyed and break into pieces.
/// All objects should have rigidbody and colliders

/// </summary>
public class DestroyBox : MonoBehaviour
{
    // The shattered verstion of the same object, ususally should be several pieces under one empty gameobjects
    public GameObject ShatteredObject;

    private void OnCollisionEnter(Collision other)
    {
        // This one is checking the magnitude of collision
        if(other.relativeVelocity.magnitude > 5)
        {
            Instantiate(ShatteredObject, transform.position, transform.rotation);
            Destroy(gameObject);

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // This one is checking the tag of other gameobject
        if (other.gameObject.CompareTag("Hammer"))
        {
            Instantiate(ShatteredObject, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }

    // On mouse down is another option
    /*
    private void OnMouseDown()
    {
        Instantiate(ShatteredObject, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    */
}