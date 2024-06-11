using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// This script take the position and rotation, write into a csv file, as bookmark.
/// Can be triggered by a key or link it to a UI button.
/// </summary>


public class CameraLocationBookmark : MonoBehaviour
{
    // Drop the main camera
    [Header("Drop main camera here")]
    public Camera mainCamera;

    [Header("Set bookmark data file name here, with .csv")]
    public string bookMarkDataFileName = "BookmarkData.csv";

    // To keep track of the comment number
    private int cameraViewCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Create the csv folder. The "Application.streamingAssetsPath" is accessbile from both Editor mode and Builds. Try to use this for more input/output files.
        Directory.CreateDirectory(Application.streamingAssetsPath + "/CSV_log/");

        // Full path for the csv bookmark file 
        string bookmarkPath = Application.streamingAssetsPath + "/CSV_log/" + bookMarkDataFileName;

        // Check if the CSV file exists
        if (File.Exists(bookmarkPath))
        {
            // Read the last record to determine the current comment count
            using (StreamReader reader = new StreamReader(bookmarkPath))
            {
                // Set an empty line
                string lastLine = "";
                while (!reader.EndOfStream)
                {
                    // Read to the last line of bookmark csv file
                    lastLine = reader.ReadLine();
                }

                // If the last line of the file is not null or empty
                if (!string.IsNullOrEmpty(lastLine))
                {
                    // Split the last line to get the comment count
                    string[] parts = lastLine.Split(',');
                    if (parts.Length > 0)
                    {
                        // Since the comment count is the in the first cell of each line
                        // Try parse out the first string into int, as the commnet count
                        if (int.TryParse(parts[0], out int lastCameraViewCount))
                        {
                            cameraViewCount = lastCameraViewCount;
                        }
                    }
                }
            }
        }
    }

    public void WriteBookmark()
    {
        

        // Full path for the csv bookmark file 
        string bookmarkPath = Application.streamingAssetsPath + "/CSV_log/" + bookMarkDataFileName;

        // Increment the comment count for each entry
        cameraViewCount++;

        // Create a formatted string with comment number, screenshot name, video clip name, video exact frame, camera rotation x,y,z,w, camera fov, and user input
        string newBookmark = cameraViewCount + ","  + mainCamera.transform.position.x + "," + mainCamera.transform.position.y + "," + mainCamera.transform.position.z + "," + mainCamera.transform.rotation.x + "," + mainCamera.transform.rotation.y + "," + mainCamera.transform.rotation.z + "," + mainCamera.transform.rotation.w + "," + mainCamera.fieldOfView + ",";

        // Write the formatted line to the CSV file
        using (StreamWriter writer = new StreamWriter(bookmarkPath, true))
        {
            writer.WriteLine(newBookmark);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
