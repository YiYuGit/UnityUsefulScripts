using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


/// <summary>
/// This script is used together with the FocusTest script.
/// This script should be attached to an empty object to use.
/// Read the recorded eye focus infomation, and reproduce them in the new scene with a sphere for each head location.
/// 
/// </summary>
public class ReproduceHeadLocation: MonoBehaviour
{
    // Name of the input file, no extension, the input CSV file should be in the Resources folder under the Assets
    // Remember to actually type this "string" into the slot in the editor/inspector
    public string EyeFocusData;

    // List for holding data from CSV reader
    private List<Dictionary<string, object>> eyeFocusList;

    // Indices for columns to be assigned
    public int gaze_x = 0;
    public int gaze_y = 1;
    public int gaze_z = 2;
    public int head_x = 3;
    public int head_y = 4;
    public int head_z = 5;


    // Full column names
    public string name0;
    public string name1;
    public string name2;
    public string name3;
    public string name4;
    public string name5;

    // The prefab for the data points that will be instantiated
    public GameObject ballPrefab;

    // Object which will contain instantiated prefabs in hiearchy
    public GameObject headHolder;


    // Start is called before the first frame update
    void Start()
    {

        // Set markList to results of function Reader with argument inputfile
        eyeFocusList = CSVReader.Read(EyeFocusData);

        //Log to console
        Debug.Log(eyeFocusList);

        // Declare list of strings, fill with keys (column names)
        List<string> columnList = new List<string>(eyeFocusList[1].Keys);

        // Print number of keys (using .count)
        Debug.Log("There are " + columnList.Count + " columns in the CSV file");

        foreach (string key in columnList)
            Debug.Log("Column name is " + key);

        // Assign column name from columnList to Name variables
        name0 = columnList[gaze_x];
        name1 = columnList[gaze_y];
        name2 = columnList[gaze_z];
        name3 = columnList[head_x];
        name4 = columnList[head_y];
        name5 = columnList[head_z];


        //Loop through Pointlist
        for (var i = 0; i < eyeFocusList.Count; i++)
        {

            // Get position and rotation value in markList at its "row", in "column" Name    ( this is only for eye focus, can also be changed to head position)
            float p_x = Convert.ToSingle(eyeFocusList[i][name3]);
            float p_y = Convert.ToSingle(eyeFocusList[i][name4]);
            float p_z = Convert.ToSingle(eyeFocusList[i][name5]);


            // Assemble the position and rotation of mark object
            Vector3 pos = new Vector3(p_x, p_y, p_z);

            // Instantiate as gameobject variable so that it can be manipulated within loop, the index comes from previous steps, pos and rot is from CSV
            GameObject markPoint = Instantiate(ballPrefab, pos, Quaternion.identity);

            // Make the markPoint as child of markHolder object, to keep points within container in hierarchy
            markPoint.transform.parent = headHolder.transform;


        }


    }

}
