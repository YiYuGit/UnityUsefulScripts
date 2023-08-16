using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// This script is used to control the play of multiple video clips on the videoplayer.
/// Attach this script to the same object with the VideoPlayer
/// Set the video clips list by enter the number and drag the video clips to the list, sequence does not matter.
/// 
/// The ChangeVideoClip function take the input of clip name string and start time of the video clip, use the name to match the video and set the start time
/// This public function can be called from other script or linked to UI.
/// 
/// Other functions like pause, forward, backward can be add, see " VideoController" script
/// </summary>

public class MultiVideoController: MonoBehaviour
{
    // Put the video clips here
    [SerializeField] private List<VideoClip> videoClips = new List<VideoClip>();
    [SerializeField] private VideoPlayer videoPlayer;

    private void Start()
    {
        // Get the Video Player from the same object
        videoPlayer = GetComponent<VideoPlayer>();
    }

    public void ChangeVideoClip(string clipName, int startTime)
    {
        // Find the clip by name
        VideoClip clipToPlay = videoClips.Find(clip => clip.name == clipName);

        // Assign the video clip and start time
        if (clipToPlay != null)
        {
            videoPlayer.clip = clipToPlay;
            videoPlayer.time = startTime;
            videoPlayer.Play();
        }
        else
        {
            Debug.LogError("Video clip not found: " + clipName);
        }
    }
}
