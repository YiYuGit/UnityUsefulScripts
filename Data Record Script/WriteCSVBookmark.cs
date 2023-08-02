using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.Video;
using UnityEngine.Networking;


/// <summary>
/// This script is used for saving screenshot bookmark information to csv file.
/// The "WriteBookmark()" can be called by other scripts.
/// The examples here can save the camera facing direction, video player time, camera rotation, camera FOV information of the screenshot and other geographic information into a csv file.
/// Each screen shot correspond to one data entry in the csv file.
/// The output file serve as "Bookmark" for the 360 video player controller
/// This script is also linked to the ScreenShotWithUI script, each time that script take a screen shot, a data entry is written to the csv file in this script.
/// A data replaying script ("RepalyBookmark.cs") can be used to read and display the data saved in this file.
/// </summary>

public class WriteCSVBookmark : MonoBehaviour
{
    // Drop the north to camera heading text, the heading text provide the camera heading number
    public TMP_Text inputText;

    // Drop the video player, used to read the current player time and use this time value to find the corresponding gps dot
    public VideoPlayer player;

    // Drop the main camera
    public Camera mainCamera;

    [Header("Set bookmark data file name")]
    public string bookMarkDataFileName = "BookmarkData.csv";

    // The gps dot found by matching the name from video player time
    private GameObject gpsDot;

    // Read the gps dot information from the "OnMouseDownPlayUnityVideo" script
    private OnMouseDownPlayUnityVideo gpsDotInfo;


    // Start is called before the first frame update. In case the csv file folder is not created, create the folder
    void Start()
    {
        // Create the txt folder. The "Application.streamingAssetsPath" is accessbile from both Editor mode and Builds. Try to use this for more input/output files.
        Directory.CreateDirectory(Application.streamingAssetsPath + "/CSV_log/");
    }


    // The function that write the one line data to the csv file
    public void WriteToFile(string message)
    {
        // The path is in assets folder, can be changed to other path
        // For the file name, it is coded
        string path = Application.streamingAssetsPath + "/CSV_log/" + bookMarkDataFileName;
        try
        {
            StreamWriter filewriter = new StreamWriter(path, true);
            filewriter.Write(message);
            filewriter.Close();
        }
        catch
        {
            Debug.LogError("cannot write to the file");
        }

    }

    // The bookmark function that called by other script to record data into csv file
    public void WriteBookmark()
    {
        // The file name of the bookmark data csv file
        //string csvFileName = Application.streamingAssetsPath + "/CSV_log/" + "BookmarkData.csv";

        // Find the gps dot by the name, name is the current video player time.
        gpsDot = GameObject.Find((Mathf.RoundToInt((float)player.time).ToString()));

        // Get the "OnMouseDownPlayUnityVideo" script from the gps dot
        gpsDotInfo = gpsDot.GetComponent<OnMouseDownPlayUnityVideo>();

        Transform camTrans = mainCamera.transform;


        // Write the line of data into the CSV file
        WriteToFile("\n" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + "," + gpsDotInfo.timeStamp.ToString() + "," + player.time.ToString() + "," + inputText.text + "," + camTrans.rotation.x + "," + camTrans.rotation.y + "," + camTrans.rotation.z + "," + camTrans.rotation.w + "," + mainCamera.fieldOfView.ToString());



        // These lines were originally created in the WriteTxt.cs  Keep them here for explanantion txt. Also, if needed, the write txt can also be used.
        // Pay attention to WriteAllText and AppendAllText

        /*
        File.WriteAllText(txtFileName, inputText.text);
        File.AppendAllText(txtFileName, "\n");
        File.AppendAllText(txtFileName, "Video Player Time: " + player.time.ToString() + " s");
        File.AppendAllText(txtFileName, "\n");
        File.AppendAllText(txtFileName, "Main Camera Rotation: " + mainCamera.transform.rotation.ToString());
        File.AppendAllText(txtFileName, "\n");
        File.AppendAllText(txtFileName, "Main Camera FOV: " + mainCamera.fieldOfView.ToString());
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


         */
        // Additional function, assemble the latitude and longitude into a google earth link and open it upon saving the txt file
        //Application.OpenURL("https://maps.google.com/?q=" + gpsDotInfo.latitude.ToString() + "," + gpsDotInfo.longitude.ToString());

    }

}
