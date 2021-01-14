using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Globalization;
using System.Threading;

public class DataRecording : MonoBehaviour
{
    // This script record the time and location of gameobject

    // The gameobject that need to be recorded
    [Header("Drag and drop the target object here")]
    public GameObject target;
    
    [Header("The recorded file is in Assets folder")]
    [Header("Press key 'r' to start the recording")]

    // The recording status, defalut is false, so no recording unitil press "r", and stop recording when press "r" again
    public bool recordingStatus = false;

    void Start()
    {
        //Write the head of the csv file, adjust for different purpose accordingly
        WriteToFile("\n" + "Sytem Time" + "," + "object-x" + "," + "object-y" + "," + "object-z" + "," + "ObjectTag");
    }

    // Update is called once per frame
    void Update()
    {
        // Switching the recording status
        if (Input.GetKeyDown("r"))
        {
             recordingStatus = !recordingStatus;
        }

        if ( recordingStatus == true)
        {
            // Covnert target location to string, with selected digits
            string targetLocationX = target.transform.position.x.ToString("f5");
            string targetLocationY = target.transform.position.y.ToString("f5");
            string targetLocationZ = target.transform.position.z.ToString("f5");

            // Get system time
            System.DateTime time = System.DateTime.Now;

            // Get the millisecond component
            int millisecond = time.Millisecond;

            // Write to file
            WriteToFile("\n" + time.ToLongTimeString() + " " + millisecond.ToString() + "ms" + "," + targetLocationX + "," + targetLocationY + "," + targetLocationZ + "," + target.tag);


        }

    }


    public void WriteToFile(string message)
    {
        // The path is in assets folder, can be changed to other path
        string path = Application.dataPath + "/RecordedData.csv";
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

}
