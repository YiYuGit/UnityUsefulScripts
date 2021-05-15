using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Put camera as the child of an empty object, apply this script to the object
/// This will rotate the camera view around the object by horizontal input and rotation speed.
/// </summary>
public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed;


    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
    }
}
