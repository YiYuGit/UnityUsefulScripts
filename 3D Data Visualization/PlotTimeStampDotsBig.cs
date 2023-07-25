using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// This script read the coordiante data from ACAD exported csv file (unit should be meter, ft can also be used, but please pay attention to conversion problem)
/// The column sequence may need to be adjusted, in Unity, the plane surface is the  X-Z horizontal surface, Y is the the vertical elevation.
/// so the normal XYZ may need to be change to XZY. If data source is not clear, you may plot once and check the distribution of the dots and adjust the column sequence.
/// 
/// Script will read the XYZ info and instantiate GPS dots at the location.
/// The prefab object of the GPS dot should have a "OnMouseDownPlayUnityVideo" video control script attached to it.

/// The start time and end time of the video will be assigne to the "OnMouseDownPlayUnityVideo" script on the GPS dot, so user can click on the dot and jump to the video play
/// The start time come from the data file.
/// 
/// Function expansion idea:
/// 1.add additional data attributes in the data file, after instantiation, assign the data to the GPS dots.
/// it can be real world latitude/longitude, project specific notes,etc
/// Some of the additional data can be linked to a UI text or world space TMP text to show on mouse hover or mouse click
/// 
/// 2. add notes taking function and report generatin function.
/// 
/// 3.Calculate the direction the user is looking in 360 video.
/// (play 360 video inside unity with first person view, calculate in Unity coordiante systemn)
/// 
/// </summary>


public class PlotTimeStampDotsBig : MonoBehaviour
{
    [Header("Name of data file in Resources folder")]
    public string GPSpoints;

    // The prefab for the data points that will be instantiated
    [Header("Drop GPS dot prefab here")]
    public GameObject markPrefab;

    // Object which will contain instantiated prefabs in hierarchy
    [Header("Drop dots holder here")]
    public GameObject markHolder;

    // Optional Extra Endtime for the video play
    [Header("Optional Extra Endtime")]
    public string exEndTime;

    // List for holding data from CSV reader
    private List<Dictionary<string, object>> markList;

    // Indices for columns to be assigned

    [Header("Data info")]
    [SerializeField]
    private int count = 0;
    [SerializeField]
    private int latitude = 1;
    [SerializeField]
    private int longitude = 2;
    [SerializeField]
    private int elevationM = 3;
    [SerializeField]
    private int timestamp = 4;
    [SerializeField]
    private int northing = 5;
    [SerializeField]
    private int easting = 6;
    [SerializeField]
    private int elevationFt = 7;
    [SerializeField]
    private int positionX = 8;
    [SerializeField]
    private int positionY = 9;
    [SerializeField]
    private int positionZ = 10;


    // Full column names
    //[Header("Data column names")]
    //[SerializeField]
    private string name0, name1, name2, name3, name4, name5, name6, name7, name8, name9, name10, name11;
    //private string name1;
    //private string name2;
    //private string name3;
    //private string name4;



    // Start is called before the first frame update
    void Start()
    {
        // Set markList to results of function Reader with argument inputfile
        markList = CSVReader.Read(GPSpoints);

        // Declare list of strings, fill with keys (column names)
        List<string> columnList = new List<string>(markList[1].Keys);


        // Print number of keys (using .count)
        Debug.Log("There are " + columnList.Count + " columns in the CSV file");

        foreach (string key in columnList)
            Debug.Log("Column name is " + key);

        // Assign column name from columnList to Name variables, please pay attention to the Y and Z, they may need to be switched

        name0 = columnList[count];
        name1 = columnList[latitude];
        name2 = columnList[longitude];
        name3 = columnList[elevationM];
        name4 = columnList[timestamp];
        name5 = columnList[northing];
        name6 = columnList[easting];
        name7 = columnList[elevationFt];
        name8 = columnList[positionX];
        name9 = columnList[positionY];
        name10 = columnList[positionZ];



        for (var i = 0; i < markList.Count; i++)
        {
            // Get position value in markList at its "row", in "column" Name
            float p_x = Convert.ToSingle(markList[i][name8]);
            float p_y = Convert.ToSingle(markList[i][name10]);
            float p_z = Convert.ToSingle(markList[i][name9]);

            // Get start and end time for each position
            string s_t = Convert.ToString(markList[i][name0]);


            // Assemble the position
            // Pay attention to the y and z value, many other software use z for height, unity use y for height, so the y and z sequence may need to be 
            // switched in data file or here
            Vector3 pos = new Vector3(p_x, p_y, p_z);

            // Instantiate as gameobject variable so that it can be manipulated within loop, the index comes from previous steps, pos and rot is from CSV
            GameObject markPoint = Instantiate(markPrefab, pos, Quaternion.identity);

            // Make the markPoint as child of markHolder object, to keep points within container in hierarchy
            markPoint.transform.parent = markHolder.transform;

            // Rename the markPoint's name to GPSpts + startTime
            markPoint.name =s_t;




            //Change the start time and end time of the markPoint's "OnMouseDownPlay" script
            OnMouseDownPlayUnityVideo play = markPoint.GetComponent<OnMouseDownPlayUnityVideo>();

            // Assign otherdata
            play.latitude = Convert.ToSingle(markList[i][name1]);
            play.longitude = Convert.ToSingle(markList[i][name2]);
            play.elevationM = Convert.ToSingle(markList[i][name3]);
            play.timeStamp = Convert.ToString(markList[i][name4]);
            play.northing = Convert.ToSingle(markList[i][name5]);
            play.easting = Convert.ToSingle(markList[i][name6]);
            play.elevationFt = Convert.ToSingle(markList[i][name7]);


            //Assign the start time value
            play.startTime = Convert.ToInt32(s_t);


        }

    }

    void Update()
    {
        // for Quiting the application
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }

    }


}
