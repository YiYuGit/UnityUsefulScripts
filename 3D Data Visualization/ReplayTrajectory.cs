using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// This script can replay the recorded trajectory. (requires the CSVReader.cs)
/// Press the replayKey to replay
/// The format of the recorded data should be pos-x, pos-y, pos-z, rot-x, rot-y, rot-z, rot-w.
/// Since the trajectory recording and replay are both in FixedUpdate, the speed kept the same.
/// </summary>
public class ReplayTrajectory : MonoBehaviour
{
    // Key code for start/pause the replay
    public KeyCode replayKey = KeyCode.R;

    // The gameobject that need to be replayed
    [Header("Drag and drop the target object here")]
    public Transform target;

    // Name for the csv file
    public string PlayerTrajectoryData;

    // List for holding data from CSV reader
    private List<Dictionary<string, object>> pathList;

    // Indices for columns to be assigned
    public int position_x = 0;
    public int position_y = 1;
    public int position_z = 2;
    public int rotation_x = 3;
    public int rotation_y = 4;
    public int rotation_z = 5;
    public int rotation_w = 6;

    // Full column names
    public string name0;
    public string name1;
    public string name2;
    public string name3;
    public string name4;
    public string name5;
    public string name6;

    // two array to hold the pos and rot of each frame
    private Vector3[] pos;

    private Quaternion[] rot;


    // Frame count
    private int frameCount;

    // Replay frame counter
    private int counter = 0;

    // Replay status bool
    public bool replaying = false;

    // Start is called before the first frame update
    void Start()
    {

        LoadingData();

        //The following part is moved to LoadingData();
        //pathList = CSVReader.Read(PlayerTrajectoryData);

        //// Declare list of strings, fill with keys (column names)
        //List<string> columnList = new List<string>(pathList[1].Keys);

        //// Print number of keys (using .count)
        //Debug.Log("There are " + columnList.Count + " columns in the CSV file");

        //foreach (string key in columnList)
        //    Debug.Log("Column name is " + key);

        //// Assign column name from columnList to Name variables
        //name0 = columnList[position_x];
        //name1 = columnList[position_y];
        //name2 = columnList[position_z];
        //name3 = columnList[rotation_x];
        //name4 = columnList[rotation_y];
        //name5 = columnList[rotation_z];
        //name6 = columnList[rotation_w];

        //pos = new Vector3[pathList.Count];
        //rot = new Quaternion[pathList.Count];

        ////Loop through Pointlist
        //for (var i = 0; i < pathList.Count; i++)
        //{
        //    // Get position and rotation value in markList at its "row", in "column" Name
        //    float p_x = Convert.ToSingle(pathList[i][name0]);
        //    float p_y = Convert.ToSingle(pathList[i][name1]);
        //    float p_z = Convert.ToSingle(pathList[i][name2]);

        //    float r_x = Convert.ToSingle(pathList[i][name3]);
        //    float r_y = Convert.ToSingle(pathList[i][name4]);
        //    float r_z = Convert.ToSingle(pathList[i][name5]);
        //    float r_w = Convert.ToSingle(pathList[i][name6]);

        //    //float p_x = System.Convert.ToSingle(pathList[i][name0]);
        //    //float p_y = System.Convert.ToSingle(pathList[i][name1]);
        //    //float p_z = System.Convert.ToSingle(pathList[i][name2]);

        //    //float r_x = System.Convert.ToSingle(pathList[i][name3]);
        //    //float r_y = System.Convert.ToSingle(pathList[i][name4]);
        //    //float r_z = System.Convert.ToSingle(pathList[i][name5]);
        //    //float r_w = System.Convert.ToSingle(pathList[i][name6]);


        //    // Assemble the position and rotation of mark object and save into pos[] and rot[]
        //    pos[i] = new Vector3(p_x, p_y, p_z);

        //    rot[i] = new Quaternion(r_x, r_y, r_z, r_w);

        //}

        //// get the frameCount
        //frameCount = pos.Length;

    }


    private void LoadingData()
    {

        pathList = CSVReader.Read(PlayerTrajectoryData);

        // Declare list of strings, fill with keys (column names)
        List<string> columnList = new List<string>(pathList[1].Keys);

        // Print number of keys (using .count)
        Debug.Log("There are " + columnList.Count + " columns in the CSV file");

        foreach (string key in columnList)
            Debug.Log("Column name is " + key);

        // Assign column name from columnList to Name variables
        name0 = columnList[position_x];
        name1 = columnList[position_y];
        name2 = columnList[position_z];
        name3 = columnList[rotation_x];
        name4 = columnList[rotation_y];
        name5 = columnList[rotation_z];
        name6 = columnList[rotation_w];

        pos = new Vector3[pathList.Count];
        rot = new Quaternion[pathList.Count];

        //Loop through Pointlist
        for (var i = 0; i < pathList.Count; i++)
        {
            // Get position and rotation value in markList at its "row", in "column" Name
            float p_x = Convert.ToSingle(pathList[i][name0]);
            float p_y = Convert.ToSingle(pathList[i][name1]);
            float p_z = Convert.ToSingle(pathList[i][name2]);

            float r_x = Convert.ToSingle(pathList[i][name3]);
            float r_y = Convert.ToSingle(pathList[i][name4]);
            float r_z = Convert.ToSingle(pathList[i][name5]);
            float r_w = Convert.ToSingle(pathList[i][name6]);

            // Assemble the position and rotation of mark object and save into pos[] and rot[]
            pos[i] = new Vector3(p_x, p_y, p_z);

            rot[i] = new Quaternion(r_x, r_y, r_z, r_w);

        }

        // get the frameCount
        frameCount = pos.Length;
    }


    // The Replay read the counter and find the corresponding pos and rot, assign them to the target's transform
    private void Replay(int counter)
    {
        target.position = pos[counter];

        target.rotation = rot[counter];

    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(replayKey))
        {
            replaying =!replaying;
        }

        if (replaying)
        {
            Replay(counter);
            counter++;

            // Restart the play
            if( counter >= pos.Length)
            {
                counter = 0;
            }
        }
    }

    // If don't want to show the OnDrawGizmos, just collapse the script in the inspector
    private void OnDrawGizmos()
    {
        // Check if the LoadingData() has run or not. if pos[] is empty, then run LoadingData() once
        if (pos.Length < 2)
        {
            LoadingData();
        }

        // After running the LoadingData(), the pos[] is populated with points from recorded data
        //Set color
        Gizmos.color = Color.red;

        // Draw lines, for every 10 points, so the increment here is 10, which looks fine, 100 is too coarse
        for (int i = 0; i < pos.Length - 11 ; i +=10 )
        {
            Gizmos.DrawLine(pos[i], pos[i+10]);
        }

    }

}
