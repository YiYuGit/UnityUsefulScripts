using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using TMPro;
using UnityEngine.UIElements;

/// <summary>
/// This script is used to record the location data to a csv file on the phone. 
/// The csv files are stored in the Android / appname / data folder
/// </summary>


public class LocationRecorder : MonoBehaviour
{
    // Assign the status text in the Inspector, used to show the location status
    public TMP_Text statusText;

    // Consider using a UI dropdown menu to select the recording interval for the "InvokeRepeating", maybe every 1s,2s,5s,10s
    // The settings to change recording internal maybe better put in another scene. 
    // for accessing object from another scene, see https://gamedev.stackexchange.com/questions/128129/accessing-an-object-from-another-scene-in-unity
    public TMPro.TMP_Dropdown menu;


    private bool isRecording = false;
    private StreamWriter csvWriter;
    private int sequenceNumber = 1;
    private int recordingInterval = 1;

    // Start or stop recording when the button is clicked
    public void ToggleRecording()
    {
        isRecording = !isRecording;
        if (isRecording)
        {
            StartRecording();
            statusText.text = "Recording Location...";
        }
        else
        {
            StopRecording();
            statusText.text = "Recording Stopped";
        }
    }

    public void SetRecordingInterval()
    {
        recordingInterval = Convert.ToInt16(menu.options[menu.value].text);
        //Debug.Log(recordingInterval);
    }



    // Start recording and create the CSV file
    private void StartRecording()
    {
        string dateTimeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string fileName = "GPS_Record_" + dateTimeStamp + ".csv";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        try
        {
            csvWriter = new StreamWriter(filePath);
            csvWriter.WriteLine("Sequence,DateAndTime,Latitude,Longitude,Altitude,CompassHeading");
        }
        catch (Exception e)
        {
            Debug.LogError("Error creating CSV file: " + e.Message);
            isRecording = false;
        }

        //InvokeRepeating("RecordLocation", 0f, 1f); // Record every 1 second
        InvokeRepeating("RecordLocation", 0f, recordingInterval); // Record every :"recordingInterval" seconds
    }

    // Record location data and compass heading
    private void RecordLocation()
    {
        if (isRecording)
        {
            // Get the current location
            float latitude = Input.location.lastData.latitude;
            float longitude = Input.location.lastData.longitude;
            float altitude = Input.location.lastData.altitude;

            // Get the current date and time
            string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

            // Get the compass heading
            float heading = Input.compass.trueHeading;

            // Write the data to the CSV file
            string csvLine = sequenceNumber + "," + currentDateTime + "," + latitude + "," + longitude + "," + altitude + "," + heading;
            csvWriter.WriteLine(csvLine);
            sequenceNumber++;
        }
    }

    // Stop recording and close the CSV file
    private void StopRecording()
    {
        if (csvWriter != null)
        {
            csvWriter.Close();
            CancelInvoke("RecordLocation"); // Stop recording
            Debug.Log("Recording stopped.");
        }
    }
}
