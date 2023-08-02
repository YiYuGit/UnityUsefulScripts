using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// 
/// Experimental Script, for testing the unity video player
/// 
/// (Currently not in use for 360 video player, but functions can be used elsewhere)
/// 
/// Attach to icon object in Unity, (need collider) 
/// Drag and drop the video player in the scene.
/// On start, the video start from begining,
/// On mouse down, video will jump to the startTime set by user
/// 
/// Use "space", "left arrow" and "right arrow" to pause/play, and jump -+ 5 seconds
/// 
/// Pay attention to the 
/// Input.GetKeyDown(KeyCode.***)
/// it may conflict with UI inputfield, make a separte key for switching the inputmode or some other "if" condition is recommended.
/// 
/// </summary>

public class ClickPlayUnityVideo : MonoBehaviour
{
    // Drag and drop video player object here
    public VideoPlayer videoPlayer;

    public float startTime;

    private void OnMouseDown()
    {
        // This line set the video player current play time
        videoPlayer.time = startTime;


        //This line can set the playback speed to 1/10 or 10X
        //videoPlayer.playbackSpeed = videoPlayer.playbackSpeed / 10.0f;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && videoPlayer.isPlaying)
        {
            //Debug.Log("space key was pressed");
            videoPlayer.Pause();
        }
        else if (Input.GetKeyDown("space") && videoPlayer.isPaused)
        {
            videoPlayer.Play();
        }    

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            videoPlayer.time -= 5f;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            videoPlayer.time += 5f;
        }
    }
}
