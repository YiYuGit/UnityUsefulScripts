using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// New experimental script.In use.
/// 
/// This script is providing the top map cam (a map that facing down)'s ability to zoom in/out and move around.
/// Right click and drag to move around.
/// Mouse scroll to zoom in and out.
/// 
/// (Since the other camera - main camera's controller is also using mouse scroll to zoom,
/// it is linked with top map camera, when top map camera is enabled. the main camera's rotation and zoom will be disabled.)
/// 
/// This script also can be used for a camera facing other direction and you can edit it to have moving limit.
/// Just change and test the directions.
/// 
/// </summary>

public class TopMapZoomAndMoveController : MonoBehaviour
{
    // Mouse drag origin
    private Vector3 dragOrigin;
    
    // Reference to the top map camera
    private Camera mCam;

    // The values below need to be changed to fit different project.
    private float zoom = 363f;

    // The boundary of the top map camera, so user can not drag to far.
    public float minX = 200f;
    public float maxX = 1200f;
    public float minZ = 0f;
    public float maxZ = 800f;

    // The max camera size should fit the whole base map
    // The min camera size should show reasonable map details
    public float minOrthographicSize = 100f;
    public float maxOrthographicSize = 363f;
    public float zoomSpeed = 10f;

    void Start()
    {
        // Get the top map camera
        mCam = GetComponent<Camera>();
    }

    void Update()
    {
        HandleMouseInput();

        // Adjust orthographic size based on mouse scroll
        //float scroll = Input.mouseScrollDelta.y;
        //float newSize = mCam.orthographicSize - scroll * zoomSpeed;
        zoom -=  zoomSpeed * Input.GetAxis("Mouse ScrollWheel");
        zoom = Mathf.Clamp(zoom, minOrthographicSize, maxOrthographicSize);
        mCam.orthographicSize = zoom;
    }

    void HandleMouseInput()
    {
        // Check if right mouse button is pressed
        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        // Check if right mouse button is released
        if (!Input.GetMouseButton(1)) return;

        // Calculate the difference in mouse position
        Vector3 dragDelta = Input.mousePosition - dragOrigin;

        // Invert the movement for X and Z axes to move the camera in the opposite direction
        float moveX = -dragDelta.x * 0.1f;
        float moveZ = -dragDelta.y * 0.1f;

        // Adjust the camera position
        Vector3 newPosition = transform.position + new Vector3(moveX, 0, moveZ);

        // Clamp the position within the specified bounds
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        transform.position = newPosition;

        // Update the dragOrigin for the next frame
        dragOrigin = Input.mousePosition;


    }
}
