using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Video;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using TMPro;


/// <summary>
/// This script read the recorded screenshot bookmark csv data (from "WriteCSVBookmark") and display the same screen scene by
/// changing the video player's time for each screenshot and change the camera's roation and FOV to match the screenshot
/// 
/// In this script, the replay function is an automated process, all bookmarks played in loop with a wait time for each bookmark.
/// </summary>

public class ReplayScreenshotBookmark : MonoBehaviour
{
    // Drag and drop the main camera here
    public Camera mainCamera;

    // Drag and drop the video player here
    public VideoPlayer videoPlayer;

    // The camera heading TMP text
    public TMP_Text camHeadingText;

    // Name of the bookmark csv file, the file should be 
    //public string bookMarkFile;

    //[Header("Location of data file")]
    //public string path = @"C:\Users\yyu\Desktop\Unity360VideoPlayer\Assets\StreamingAssets\CSV_log\BookmarkData.csv";

    // List for holding data from CSV reader
    private List<Dictionary<string, object>> bookMarks;

    private int photoFileName = 0;
    private int timeStamp = 1;
    private int videoTime = 2;
    private int camHeading = 3;
    private int camRotation_x = 4;
    private int camRotation_y = 5;
    private int camRotation_z = 6;
    private int camRotation_w = 7;
    private int camFov = 8;

    // Full column names;
    private string name0, name1, name2, name3, name4, name5, name6, name7, name8;

    //  Quaterion to hold the rotation of the camera
    private Quaternion[] rotation;

    // Bookmark count
    private int bookMarkCount;

    // Bookmark replay counter
    private int counter = 0;

    // Wait time between book marks
    [Header("Wait time between two bookmarks")]
    public float waitTime = 5f;

    // Replay status
    public bool isReplaying = true;


    //private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        
        LoadingData();

        //coroutine = WaitAndPlayBookMarks();

        StartCoroutine("WaitAndPlayBookMarks");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseAndResumeRepaly();

        }
    }


    private void LoadingData()
    {
        // read data into bookmarks
        // There are several ways to do this, use absolute path or Application.streamingAssetsPath
        // Application.streamingAssetsPath is good for storing and reading data from Builds
        //bookMarks = CSVReader.Read(bookMarkFile);
        //string path = @"C:\Users\yyu\Desktop\Unity360VideoPlayer\Assets\StreamingAssets\CSV_log\BookmarkData.csv";

        //bookMarks = NewCSVReader.Read(path);

        bookMarks = NewCSVReader.Read(Application.streamingAssetsPath + "/CSV_log/" + "BookmarkData.csv");

        // Remove the possible empty lines in the bookMarks
        bookMarks = bookMarks.Where(item => item != null).ToList();

        // Declare list of strings, fill with keys (column names)
        List<string> columnList = new List<string>(bookMarks[1].Keys);



        // the amount of columns is columnList.Count
        name0 = columnList[photoFileName];
        name1 = columnList[timeStamp];
        name2 = columnList[videoTime];
        name3 = columnList[camHeading];
        name4 = columnList[camRotation_x];
        name5 = columnList[camRotation_y];
        name6 = columnList[camRotation_z];
        name7 = columnList[camRotation_w];
        name8 = columnList[camFov];

        // Array for the camera rotation

        rotation = new Quaternion[bookMarks.Count];


        // loop through the bookMarks
        for (int i = 1; i < bookMarks.Count; i++)
        {
            // Get the rotation value in the bookMarks at its "row" in "cloumn" name

            float r_x = Convert.ToSingle(bookMarks[i][name4]);
            float r_y = Convert.ToSingle(bookMarks[i][name5]);
            float r_z = Convert.ToSingle(bookMarks[i][name6]);
            float r_w = Convert.ToSingle(bookMarks[i][name7]);

            // Assemble the rotation of camera roation ionto roatation[]
            rotation[i] = new Quaternion(r_x, r_y, r_z, r_w);

        }

        bookMarkCount = rotation.Length;
        //Debug.Log(vidTimes);

    }

    public void AutoPlay()
    {
        // Auto play read the counter and find the corresponding player time of the video player, also assign the main camera's rotation and FOV
        videoPlayer.time = (float)bookMarks[counter][name2];

        camHeadingText.text = bookMarks[counter][name3].ToString();
        //Debug.Log(bookMarks[counter][name3]);

        mainCamera.transform.rotation = rotation[counter];

        mainCamera.fieldOfView = Convert.ToSingle(bookMarks[counter][name8]);
    }

    public IEnumerator WaitAndPlayBookMarks()
    {
        while (true)
        {
            // Restart the play if counter is >= to bookmark count
            if (counter >= bookMarkCount)
            {
                counter = 0;
            }


            AutoPlay();


            yield return new WaitForSeconds(waitTime);

            if (isReplaying)
            {
                counter++;
            }

        }
    }

    // The function can be called by another script, so no keycode is used here.
    public void PauseAndResumeRepaly()
    {
        isReplaying = !isReplaying;
    }

}




