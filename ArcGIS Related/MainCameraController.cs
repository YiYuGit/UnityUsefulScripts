using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

/// <summary>
/// This script integrated the functions from ZoomWithMouse and SimpleCameraController. 
/// Attach to the main camera of 360 Video/Photo Scene to enable the zoom and rotate of the main camera.
/// 
/// </summary>
public class MainCameraController : MonoBehaviour
{
    // Control the direction of the camera
    private Vector2 rotation = new Vector2(0, 0);

    // cameraStatus is used to control the movement of the camera
    // when camera is frozen, user can click UI buttons
    [Header("Press 'q' to free/unfreeze Camera")]
    public bool cameraStatus = true;

    // Adjust the speed the direction rotation
    public float rotateSpeed = 6f;

    // Zoom speed
    public float zoomSpeed = 50f;

    // Min and Max field of view
    public float fovMin = 10f, fovMax = 150f;

    // The initial number setting for camera fov
    private float zoom = 90f;
    private float camFov = 120f;

    // Start is called before the first frame update
    void Start()
    {
        // Put the things that you don't want to show in Main camera into this cullingMask layer in the inspector,
        // if that layer does not exist, use " Add layer" to create a layer of your choice
        Camera.main.cullingMask = ~(1 << LayerMask.NameToLayer("MainCamIgnore"));
    }

    // Update is called once per frame
    void Update()
    {
        // Switch the rotate and zoom function on and off
        // When using All input handler to call functions.
        // Disable the GetKeyDown in this script
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchRotateZoomStatus();
        }
        

        // If camera rtate and zoom function is on.
        if (cameraStatus)
        {
            // Rotate
            rotation.y += Input.GetAxis("Mouse X");
            rotation.x += -Input.GetAxis("Mouse Y");
            transform.eulerAngles = (Vector2)rotation * rotateSpeed;

            // Zoom
            zoom = zoom - (zoomSpeed * Input.GetAxis("Mouse ScrollWheel"));
            camFov = Mathf.Clamp(zoom, fovMin, fovMax);
            // This line direction the che fov of the main camera in the scene. If used on other camera, change the code here, and reference that public camera
            Camera.main.fieldOfView = camFov;
        }

    }

    public void SwitchRotateZoomStatus()
    {
        cameraStatus = !cameraStatus;
    }

}
