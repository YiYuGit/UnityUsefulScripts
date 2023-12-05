using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Experimental script. 
/// This script change the time scale of the game. 
/// Can be used for changing video play speed. But since the video player already have have a play speed.
/// There is no need for this for the video player.
/// </summary>

public class TimeaScaleChange : MonoBehaviour
{
    public float scale = 4f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
             if (Time.timeScale == 1.0f)
            {
                Time.timeScale = scale;

            }
             else
            {
                Time.timeScale = 1.0f;
            }
        }
    }
}
