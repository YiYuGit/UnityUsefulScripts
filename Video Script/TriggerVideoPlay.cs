using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// Attach script to the video player plane obejct,
/// Set a collider, maybe a box collider out side plane or using the plane itself
/// 
/// The script will find the video player on start and disable the loop. 
/// When trigger or collision happen, it will play the video.
/// Can be used for play instruction to player
/// 
/// Make sure the plane use a single color material, and the material's shader should be Unlit -> Texture
/// </summary>


public class TriggerVideoPlay : MonoBehaviour
{

    public bool isPlaying = false;
    private VideoPlayer videoPlayer;

    //
    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        // below to auto-start playback since we're in Start().
        videoPlayer.playOnAwake = false;

        // Restart from beginning when done.
        videoPlayer.isLooping = true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isPlaying)
        {
            videoPlayer.Play();
        }
        else
        {
            videoPlayer.Pause();
        }
    }

    //When collide with any object, the play status changes
    void OnCollisionEnter(Collision collision)
    {
        isPlaying = !isPlaying;
    }
}
