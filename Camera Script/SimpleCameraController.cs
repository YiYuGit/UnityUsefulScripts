using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System;

/// <summary>
/// A simple camera script that rotate the camera view,
/// Have a public function that use a bool to pause the rotation.
/// The rotation speed can be adjusted by the speed parameter.
/// </summary>


public class SimpleCameraController : MonoBehaviour
{
    // Control the direction of the camera
    Vector2 rotation = new Vector2(0, 0);

    // Adjust the speed the direction rotation
    public float speed = 8f;

    // rotateStatus is used to control the rotation of the camera
    // when camera is not rotation, user can click UI buttons
    [Header("Press 'q' to turn mouse rotation on/off")]
    public bool rotateStatus = true;

    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            SwitchRotateStatus();
        }

        if (rotateStatus)
        { 
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        transform.eulerAngles = (Vector2)rotation * speed;
        }
    }

    public void SwitchRotateStatus()
    {
        rotateStatus = !rotateStatus;
    }

}
