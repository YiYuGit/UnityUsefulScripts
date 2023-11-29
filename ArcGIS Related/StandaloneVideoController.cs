using UnityEngine;
using UnityEngine.Video;

/// <summary>
///  This script is for playing single mp4 video file from StreamingAsset folder.
///  Set the videoFileName in the inspector (without ".mp4"), the script will find the video in the streamingAssets folder.
///  Other play/pause/jump forward/move frame functions are the same with MultiVideoContoroller
///  
/// </summary>

public class StandaloneVideoController : MonoBehaviour
{
    // The name of the .mp4 video file in the StreamingAssetsPath
    public string videoFileName; 

    private VideoPlayer videoPlayer;

    void Start()
    {
        // Get the VideoPlayer component attached to the same GameObject
        videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer == null)
        {
            Debug.LogError("No VideoPlayer component found on this GameObject.");
            return;
        }

        // Set the video file to play
        PlayVideo(videoFileName);
    }

    public void PlayVideo(string fileName)
    {

        fileName = fileName + ".mp4";
        
        // Check if the video file exists in the StreamingAssets folder
        string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);

        if (System.IO.File.Exists(videoPath))
        {
            // Set the video player's source to the file path
            videoPlayer.url = videoPath;

            // Prepare the video player
            videoPlayer.Prepare();

            // Add an event handler for when the video is prepared
            videoPlayer.prepareCompleted += VideoPrepared;
        }
        else
        {
            Debug.LogError("Video file not found: " + videoPath);
        }
    }

    private void VideoPrepared(VideoPlayer player)
    {
        // Start playing the video once it's prepared
        player.Play();
    }


    // This is used for jumping to the start time from the GPS point
    public void JumpToVideoTime(int startTime)
    {
        videoPlayer.time = startTime;
        videoPlayer.Pause();
        videoPlayer.time = startTime;

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("space key was pressed");
            PauseAndPlay();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            VideoJumpBackward();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            VideoJumpForward();
        }

        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            MoveFrameBackward();
        }

        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            MoveFrameForward();
        }

        // Esc for quitting the app, make sure there is only Quit() in the scene
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif

            Application.Quit();
        }
    }


    // If playing, pause. If paused, play.
    public void PauseAndPlay()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        }
        else if (videoPlayer.isPaused)
        {
            videoPlayer.Play();
        }
    }

    public void VideoJumpForward()
    {
        videoPlayer.time += 5f;
    }

    public void VideoJumpBackward()
    {
        videoPlayer.time -= 5f;
    }

    public void MoveFrameForward()
    {

        if (videoPlayer.isPaused)
        {
            videoPlayer.frame++;
        }
    }

    public void MoveFrameBackward()
    {

        if (videoPlayer.isPaused)
        {
            videoPlayer.frame--;
        }
    }

}
