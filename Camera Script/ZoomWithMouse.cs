﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZoomWithMouse : MonoBehaviour
{

    /// <summary>
    ///  Attched to the camera to Zoom in and out with mouse scroll wheel
    /// </summary>

    // Zoom speed
    public float zoomSpeed = 20f;

    // Min and Max field of view
    public float fovMin = 3f, fovMax = 150f;
    private float zoom = 10;
    private float camFov = 120f;


    void Update()
    {

        zoom = zoom - (zoomSpeed * Input.GetAxis("Mouse ScrollWheel"));
        camFov = Mathf.Clamp(zoom, fovMin, fovMax);
        //Debug.Log(camFov);

        Camera.main.fieldOfView = camFov;
    }
}