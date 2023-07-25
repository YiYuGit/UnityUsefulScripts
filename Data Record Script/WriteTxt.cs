using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.Video;
using UnityEngine.Networking;


/// <summary>
/// This script is used for saving information to txt file
/// It can be controlled by other scripts
/// The examples here can save the camera facing direction, video player time, camera rotation, camera FOV information and other geographic information into one file. 
/// Serve as "Bookmark" for the 360 video player controller
/// 
/// This for testing, for actual bookmark.
/// It is better to record in a csv file for replay the bookmarks.
/// On saving the txt file, also open a google map link.
/// 
/// </summary>


public class WriteTxt : MonoBehaviour
{
    // Drop the north heading text
    public TMP_Text inputText;

    // Drop the video player, used to read the current player time and use the time to corresponding gps dot
    public VideoPlayer player;

    // Drop the main camera
    public Camera mainCamera;

    // The gps dot found by matching the name from video player time
    private GameObject gpsDot;

    // Read the gps dot information from the "OnMouseDownPlayUnityVideo" script
    private OnMouseDownPlayUnityVideo gpsDotInfo;


    // Start is called before the first frame update
    void Start()
    {
        // Create the txt folder
        Directory.CreateDirectory(Application.dataPath + "/Txt_log/");
    }

    public void CreateTxtFile()
    {
        // Assemble the txt file name, the name is based on the date and time, also consider using the video player time
        string txtFileName = Application.dataPath + "/Txt_log/" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".txt";

        // Check if the file exists
        if (!File.Exists(txtFileName))
        {
            // Find the gps dot by the name, name is the current video player time.
            gpsDot = GameObject.Find((Mathf.RoundToInt((float)player.time).ToString()));

            // Get the "OnMouseDownPlayUnityVideo" script from the gps dot
            gpsDotInfo = gpsDot.GetComponent<OnMouseDownPlayUnityVideo>();

            // Pay attention to WriteAllText and AppendAllText
            File.WriteAllText(txtFileName, inputText.text);
            File.AppendAllText(txtFileName, "\n");
            File.AppendAllText(txtFileName, "Video Player Time: " + player.time.ToString() + " s");
            File.AppendAllText(txtFileName, "\n");
            File.AppendAllText(txtFileName, "Main Camera Rotation: " + mainCamera.transform.rotation.ToString());
            File.AppendAllText(txtFileName, "\n");
            File.AppendAllText(txtFileName, "Main Camera FOV: "+ mainCamera.fieldOfView.ToString());
            File.AppendAllText(txtFileName, "\n");
            File.AppendAllText(txtFileName, "This point's latitude: " + gpsDotInfo.latitude.ToString());
            File.AppendAllText(txtFileName, "\n");
            File.AppendAllText(txtFileName, "This point's longitude: " + gpsDotInfo.longitude.ToString());
            File.AppendAllText(txtFileName, "\n");
            File.AppendAllText(txtFileName, "This point's elevation in Meter: " + gpsDotInfo.elevationM.ToString());
            File.AppendAllText(txtFileName, "\n");
            File.AppendAllText(txtFileName, "This point's northing: " + gpsDotInfo.northing.ToString());
            File.AppendAllText(txtFileName, "\n");
            File.AppendAllText(txtFileName, "This point's easting: " + gpsDotInfo.easting.ToString());
            File.AppendAllText(txtFileName, "\n");
            File.AppendAllText(txtFileName, "This point's elevation in Ft: " + gpsDotInfo.elevationFt.ToString());
            File.AppendAllText(txtFileName, "\n");
            File.AppendAllText(txtFileName, "This point's time stamp: " + gpsDotInfo.timeStamp.ToString());
            File.AppendAllText(txtFileName, "\n");
            File.AppendAllText(txtFileName, "Google Map link for this point: " + "https://maps.google.com/?q=" + gpsDotInfo.latitude.ToString() + "," + gpsDotInfo.longitude.ToString());

            // Additional function, assemble the latitude and longitude into a google earth link and open it upon saving the txt file
            Application.OpenURL("https://maps.google.com/?q=" + gpsDotInfo.latitude.ToString() + "," + gpsDotInfo.longitude.ToString());
        }
    }
}
