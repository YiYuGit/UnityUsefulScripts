using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// In the 360 video player scene with gps dots (from corresponding GPX data file --> csv )
/// This script read the video player's current time, and use the time to find matching GPS dot
/// Then, assemnble the latitude and longitude information from current gps dot into a link to Google Map.
/// Open the map in web browser.
/// 
/// In this script, the function is called when "G" is pressed.
/// 
/// This script can be modified to take user input latitude and longitude.
/// Also, modify it to visit any website.
/// 
/// </summary>

public class OpenGoogleMap : MonoBehaviour
{
    // Drop the video player, used to read the current player time and use the time to corresponding gps dot
    public VideoPlayer player;

    // The gps dot found by matching the name from video player time
    private GameObject gpsDot;

    // Read the gps dot information from the "OnMouseDownPlayUnityVideo" script
    private OnMouseDownPlayUnityVideo gpsDotInfo;


    public void OpenGoogleMapLink()
    {
        // Find the gps dot by the name, name is the current video player time.
        gpsDot = GameObject.Find((Mathf.RoundToInt((float)player.time).ToString()));


        // Get the "OnMouseDownPlayUnityVideo" script from the gps dot
        gpsDotInfo = gpsDot.GetComponent<OnMouseDownPlayUnityVideo>();

        // Assemble the latitude and longitude into a google earth link and open it upon saving the txt file
        Application.OpenURL("https://maps.google.com/?q=" + gpsDotInfo.latitude.ToString() + "," + gpsDotInfo.longitude.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            OpenGoogleMapLink();
        }
    }
}
