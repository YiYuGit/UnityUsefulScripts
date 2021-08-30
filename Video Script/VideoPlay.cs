using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to object with video player. Depeneding on the situation, choose to get video player on start or in update(if)
/// use Jump button to play or pause the video.
/// </summary>


public class VideoPlay : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //var vp = GetComponent<UnityEngine.Video.VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Jump"))
        {
            var vp = GetComponent<UnityEngine.Video.VideoPlayer>();

            if (vp.isPlaying)
            {
                vp.Pause();
            }
            else
            {
                vp.Play();
            }
        }
    }
}
