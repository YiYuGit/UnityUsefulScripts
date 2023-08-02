using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// 
/// Experimental Script
/// 
/// This script is used to jump the video to the bookmarked play time.
/// And the assign the maincamera rotation and field of view
/// So the image showing on the screen is the same with the bookmarked image.
/// 
/// This is a manual input demo version. 
/// The full verison should be using CSV for bookmark info recording and replaying.
/// Consider add input field for taking notes on each screen shot.
/// Use key or UI button to navigate bookmarks.
/// 
/// Try make a new scene just for showing bookmarks.
/// A small,always-on mini map, use video play time to find the GPS dot, use coroutine to toggle the dot's renderer to highlight current dot
/// </summary>



public class JumpToVideoBookmark : MonoBehaviour
{
    
    // Objects here need manual drag and drop. The play time ,rotation ,fov data need to be copied and pasted from txt bookmark file
    public VideoPlayer player;

    public Camera mainCamera;

    public float playTime;

    public Quaternion camRotation;

    public float camFOV;



    // Start is called before the first frame update
    void Start()
    {
        player.time = playTime;

        mainCamera.transform.rotation = camRotation;

        mainCamera.fieldOfView = camFOV;

    }

    // Update is called once per frame
    void Update()
    {
        player.time = playTime;

        mainCamera.transform.rotation = camRotation;

        mainCamera.fieldOfView = camFOV;

    }
}
