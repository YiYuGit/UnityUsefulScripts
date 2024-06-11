using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;

/// <summary>
/// Work with CameraLocationBookmark
/// Read the camera position, rotation and fov from bookmark and replay them in the scene
/// In this version, the play is controlled by space key, also can be linked to UI button.
/// 
/// </summary>

public class ReplayCameraLocation : MonoBehaviour
{
    // Drop the main camera here
    public Camera mCam;

    public string fileName;

    // Define a structure to hold bookmark data
    private struct BookmarkData
    {
        public int serialNumber;
        public Vector3 position;
        public Quaternion rotation;
        public float fieldOfView;
    }

    // List to store all bookmark data
    private List<BookmarkData> bookmarks;

    // Index to keep track of the current bookmark
    private int currentBookmarkIndex = 0;

    void Start()
    {
        // Load CSV file from StreamingAssets folder
        LoadCSV(fileName);

        // Ensure there is at least one bookmark before calling PlayBookmark
        if (bookmarks.Count > 0)
        {
            // Set the camera to the initial bookmark
            ApplyBookmark(bookmarks[currentBookmarkIndex]);
        }
    }

    void Update()
    {
        // Example: Call PlayBookmark when a key is pressed, you can change this based on your input needs
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayBookmark();
        }
    }

    // Function to play the next bookmark
    void PlayBookmark()
    {
        // Increment the bookmark index
        currentBookmarkIndex++;

        // If we've reached the end, loop back to the beginning
        if (currentBookmarkIndex >= bookmarks.Count)
        {
            currentBookmarkIndex = 0;
        }

        // Apply the data from the current bookmark
        ApplyBookmark(bookmarks[currentBookmarkIndex]);
    }

    // Function to load CSV file from StreamingAssets folder
    void LoadCSV(string fileName)
    {
        bookmarks = new List<BookmarkData>();

        string bookmarkPath = Application.streamingAssetsPath + "/CSV_log/" + fileName;

        try
        {
            // Read all lines from the CSV file
            string[] lines = File.ReadAllLines(bookmarkPath);

            // Parse each line and add data to the bookmarks list
            foreach (string line in lines)
            {
                string[] values = line.Split(',');

                BookmarkData bookmark = new BookmarkData();
                bookmark.serialNumber = int.Parse(values[0]);
                bookmark.position = new Vector3(float.Parse(values[1]), float.Parse(values[2]), float.Parse(values[3]));
                bookmark.rotation = new Quaternion(float.Parse(values[4]), float.Parse(values[5]), float.Parse(values[6]), float.Parse(values[7]));
                bookmark.fieldOfView = float.Parse(values[8]);

                bookmarks.Add(bookmark);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error loading CSV file: " + e.Message);
        }
    }

    // Function to apply bookmark data to the camera
    void ApplyBookmark(BookmarkData bookmark)
    {
        
        // Assign position, rotation, and field of view
        mCam.transform.SetPositionAndRotation(bookmark.position, bookmark.rotation);
        mCam.fieldOfView = bookmark.fieldOfView;
    }
}
