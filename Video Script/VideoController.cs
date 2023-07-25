using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System;

/// <summary>
/// This is a simple video player controller that is designed for use with unity video player.
/// Attach this player to the same gameobject with the unity video player.
/// This script can set the play time and read the current play time, accessed by other script.
/// The play control is space for pause and go, and left right arrow for jump back and forth.
/// </summary>


public class VideoController : MonoBehaviour
{
    [SerializeField] 
    VideoPlayer videoPlayer;

    public float startTime;

    [Header("This is for setting the time")]
    public int time;

    //Read the current player time to determine the current gps dot number
    [Header("This is the current gps dot number")]
    public int gpsDotNumber;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        
        videoPlayer.time = startTime;

        videoPlayer.Pause();
    }

    public void SetTime(int time)
    {
        videoPlayer.time = time;    
    }



    // Update is called once per frame
    void Update()
    {
        // Keep updating the current gps dot number
        gpsDotNumber = Convert.ToInt32(videoPlayer.time);

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
