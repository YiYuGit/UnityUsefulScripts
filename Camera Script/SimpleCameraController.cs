using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System;

/// <summary>
/// Simple camera controller that use the Mouse X and Y to rotate the camera
/// </summary>

public class SimpleCameraController : MonoBehaviour
{
    Vector2 rotation = new Vector2(0, 0);
    public float speed = 3;

    void Update()
    {
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        transform.eulerAngles = (Vector2)rotation * speed;
    }
}
