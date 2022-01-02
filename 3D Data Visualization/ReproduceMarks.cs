using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// This script is used together with the FindAllObjectWithTag script (a data recording script that record the location and rotation of gameobjets) to record and reproduce objects that are
/// instantiated during the gameplay
/// This script should be attached to an empty object to use.
/// Read the recorded infomation, and reproduce them in the new scene on Start.
/// 
/// Change the column names to fit your application
/// 
/// </summary>

public class ReproduceMarks : MonoBehaviour
{
    // Name of the input file, no extension, the input CSV file should be in the Resources folder under the Assets
    // Remember to actually type this "string" into the slot in the editor/inspector
    public string ObjectLocationData;

    // List for holding data from CSV reader
    private List<Dictionary<string, object>> markList;

    // Indices for columns to be assigned
    public int objectName = 0;
    public int position_x = 1;
    public int position_y = 2;
    public int position_z = 3;
    public int rotation_x = 4;
    public int rotation_y = 5;
    public int rotation_z = 6;
    public int rotation_w = 7;

    // Full column names
    public string name0;
    public string name1;
    public string name2;
    public string name3;
    public string name4;
    public string name5;
    public string name6;
    public string name7;

    // The prefab for the data points that will be instantiated
    public GameObject[] markPrefab;

    // Object which will contain instantiated prefabs in hiearchy
    public GameObject markHolder;

    // Start is called before the first frame update
    void Start()
    {

        // Set markList to results of function Reader with argument inputfile
        markList = CSVReader.Read(ObjectLocationData);

        //Log to console
        Debug.Log(markList);

        // Declare list of strings, fill with keys (column names)
        List<string> columnList = new List<string>(markList[1].Keys);

        // Print number of keys (using .count)
        Debug.Log("There are " + columnList.Count + " columns in the CSV file");

        foreach (string key in columnList)
            Debug.Log("Column name is " + key);

        // Assign column name from columnList to Name variables
        name0 = columnList[objectName];
        name1 = columnList[position_x];
        name2 = columnList[position_y];
        name3 = columnList[position_z];
        name4 = columnList[rotation_x];
        name5 = columnList[rotation_y];
        name6 = columnList[rotation_z];
        name7 = columnList[rotation_w];


        // Another array that will be based one the markPrefab's object names, use for finding marks by name.
        string[] prefabName;
        prefabName = new string[markPrefab.Length];


        //Fill the prefabName string array with markPrab names
        for (int i = 0; i < markPrefab.Length; i++)
        {
            prefabName.SetValue(Convert.ToString(markPrefab[i].name),i);
        }

        //Loop through Pointlist
        for (var i = 0; i < markList.Count; i++)
        {
            // Get name of the object to be instantiated, from the first column
            string o_name = Convert.ToString(markList[i][name0]);

            // Get position and rotation value in markList at its "row", in "column" Name
            float p_x = Convert.ToSingle(markList[i][name1]);
            float p_y = Convert.ToSingle(markList[i][name2]);
            float p_z = Convert.ToSingle(markList[i][name3]);

            float r_x = Convert.ToSingle(markList[i][name4]);
            float r_y = Convert.ToSingle(markList[i][name5]);
            float r_z = Convert.ToSingle(markList[i][name6]);
            float r_w = Convert.ToSingle(markList[i][name7]);

            // Assemble the position and rotation of mark object
            Vector3 pos = new Vector3(p_x, p_y, p_z);

            Quaternion rot = new Quaternion(r_x, r_y, r_z, r_w);


            // Get the index of the "o_name" from the prefabName list
            int index = System.Array.IndexOf(prefabName, o_name);

            // Instantiate as gameobject variable so that it can be manipulated within loop, the index comes from previous steps, pos and rot is from CSV
            GameObject markPoint = Instantiate(markPrefab[index], pos, rot);

            // Make child of markHolder object, to keep points within container in hiearchy
            markPoint.transform.parent = markHolder.transform;

        }


    }

}
