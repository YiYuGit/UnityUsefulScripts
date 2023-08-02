using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script move top view camera with UI button -- Event Trigger -- Pointer Enter -- Pointer Exit
/// 
/// This script is designed to move top view map camera with canvas UI button.
/// Set four long strip shaped button at the four edge of the screen space overlay canvas.
/// The buttons' image -- source image can be change to any transparent source image to hide the button shape
/// On each button, add event trigger and set the corresponding function for pointer enter and pointer exit.
/// When pointer enter the button area, the move camera for the direction start,
/// when pointer exit the button area, stop moving camera
/// Both are controlled by checking the bool value
/// 
/// The script can be modified for moving camera in other direction, for map on a horizontal plane here use x and z. for vertical map, use x and y.
/// 
/// To limit the movement range of the camera, check this video for example for clamping the movement range.  https://www.youtube.com/watch?v=R6scxu1BHhs
/// 
/// 
/// </summary>
public class MoveTopMapCam : MonoBehaviour
{
    // Drag and drop the top view camera here
    [Header("Drop the top view camera here")]
    public Camera topViewCam;
    [Header("The moving speed of camera")]
    public float moveSpeed = 50f;

    // bool for move in each direction
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool moveUp = false;
    private bool moveDown = false;



    // Functions for moving and stopping the camera
    public void MoveCamLeft()
    {
        moveLeft = true;
    }

    public void MoveCamRight()
    {
        moveRight = true;
    }

    public void MoveCamUp()
    {
        moveUp = true;
    }
    public void MoveCamDown()
    {
        moveDown = true;
    }

    public void StopMoveCamLeft()
    {
        moveLeft = false;
    }

    public void StopMoveCamRight()
    {
        moveRight = false;
    }

    public void StopMoveCamUp()
    {
        moveUp = false;
    }

    public void StopMoveCamDown()
    {
        moveDown = false;
    }


    // OnDisable and OnEnable stop all moving. Only allow start moving when pointer move onto button area again.
    // Used for when moving button may need to be hidden.
    void OnDisable()
    {
        moveLeft = false;
        moveRight = false;
        moveUp = false;
        moveDown = false;
    }

    void OnEnable()
    {
        moveLeft = false;
        moveRight = false;
        moveUp = false;
        moveDown = false;
    }


    // Update is called once per frame
    void Update()
    {
        // to move camera, the camera's position value add the new vector3 containing the increment of that direction
        if (moveLeft)
        {
            topViewCam.transform.position = topViewCam.transform.position + (new Vector3(-1f * moveSpeed, 0f, 0f) * Time.deltaTime);         
        }

        if (moveRight)
        {
            topViewCam.transform.position = topViewCam.transform.position + (new Vector3(1f * moveSpeed, 0f, 0f) * Time.deltaTime);
        }

        if(moveUp)
        {
            topViewCam.transform.position = topViewCam.transform.position + (new Vector3(0f, 0f, 1f * moveSpeed) * Time.deltaTime);
        }

        if(moveDown)
        {
            topViewCam.transform.position = topViewCam.transform.position + (new Vector3(0f, 0f, -1f * moveSpeed) * Time.deltaTime);
        }
    }
}
