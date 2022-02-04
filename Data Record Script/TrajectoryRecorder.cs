using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Globalization;
using System.Threading;

/// <summary>
/// This script can record the position and rotation of a gameobjct in the FixedUpdate.
/// The trajectory is recorded in the CSV file.
/// Remember to change file nanme for different sessions, so the records won't pile up in one file.
/// The trajectory can be replayed by assigning the position and rotation in the FixedUpdate to object.
/// Using the "ReplayTrajectory.cs" and"CSVReader.cs"
/// </summary>

public class TrajectoryRecorder : MonoBehaviour
{
    // The gameobject that need to be recorded
    [Header("Drag and drop the target object here")]
    public Transform target;

    // Name for the csv file
    public string saveFileName = "PlayerTrajectoryData";

    // Start is called before the first frame update
    void Start()
    {
        //Write the head of the csv file, adjust for different purpose accordingly
        WriteToFile("\n" + "position-x" + "," + "position-y" + "," + "position-z" + "," + "rotation-x" + "," + "rotation-y" + "," + "rotation-z" + "," + "rotation-w");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        WriteToFile("\n" + target.position.x + "," + target.position.y + "," + target.position.z + "," + target.rotation.x + "," + target.rotation.y + "," + target.rotation.z + "," + target.rotation.w);
    }

    public void WriteToFile(string message)
    {
        // The path is in assets folder, can be changed to other path
        string path = Application.dataPath + "/CSV/" + saveFileName + ".csv";
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
