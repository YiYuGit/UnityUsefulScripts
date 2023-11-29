using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script should be attached to the main camera of the scene.
/// Use AWSD to control the movement, and use mouse to control the rotation.
/// Holding the left shift key will switch the normal moving speed to the fast moving speed.
/// The key P is used to freeze and unfreeze the camera movemenet.
/// </summary>


public class FreeFlyCameraController : MonoBehaviour
{
    public float normalSpeed = 60f;      // Normal movement speed
    public float fastSpeed = 180f;       // Fast movement speed when left shift is held down
    public float sensitivity = 2f;      // Mouse sensitivity for rotation

    private float rotationX = 0f;
    private float rotationY = 0f;

    // A bool to swtich the camera movement on and off
    public bool activeStatus = true;


    void Update()
    {
        if (activeStatus)
        {
            // Rotation based on mouse movement
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            rotationX -= mouseY * sensitivity;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);

            rotationY += mouseX * sensitivity;

            transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);

            // Movement based on keyboard input
            float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? fastSpeed : normalSpeed;

            float horizontalMovement = Input.GetAxis("Horizontal");
            float verticalMovement = Input.GetAxis("Vertical");

            Vector3 moveDirection = new Vector3(horizontalMovement, 0f, verticalMovement).normalized;
            moveDirection = transform.TransformDirection(moveDirection);

            // Apply movement to the rigidbody
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        }

        if (Input.GetKeyDown(KeyCode.P)) 
        {
            activeStatus = !activeStatus; 
        }

    }
}
