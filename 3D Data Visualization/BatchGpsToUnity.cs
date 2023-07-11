using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary>
/// This scipt take the "GPSpoints.csv" file in the Resources folder, use CSVReader to read the first two columns data, 
/// the first column is latitude_x, the second column is longitude_y.
/// In the Unity scene, two points should be marked out,(can be based on basemap), put to the locaiton of the north west and south east of the GPS area.
/// Drop these two points to the place in Inspector.
/// A mark prefab and a holder object is used to mark each translated points and hold them.
/// On start, the script will read each row 's lat and lon data, translate into Unity coordinates and instantiate a mark at the Unity location.
/// </summary>



public class BatchGpsToUnity : MonoBehaviour
{

    [Header("Input two points GPS data")]
    public Vector2 northWestLatLon;
    public Vector2 southEastLatLon;

    [Header("Drop two Unity points")]
    public Transform northWestUnity;
    public Transform southEastUnity;


    private Vector3 gpsInUnity;

    private Vector3 northWestUnity1;

    private Vector3 southEastUnity1;


    // Name of the input file, no extension, the input CSV file should be in the Resources folder under the Assets
    // Remember to actually type this "string" into the slot in the editor/inspector
    public string GPSpoints;

    // List for holding data from CSV reader
    private List<Dictionary<string, object>> markList;

    // Indices for columns to be assigned

    public int lat_x = 0;
    public int lon_y = 1;

    //public int objectName = 0;
    //public int position_x = 1;
    //public int position_y = 2;
    //public int position_z = 3;
    //public int rotation_x = 4;
    //public int rotation_y = 5;
    //public int rotation_z = 6;
    //public int rotation_w = 7;

    // Full column names
    public string name0;
    public string name1;
    //public string name0;
    //public string name1;
    //public string name2;
    //public string name3;
    //public string name4;
    //public string name5;
    //public string name6;
    //public string name7;


    // The prefab for the data points that will be instantiated
    [Header("Drop target prefab here")]
    public GameObject markPrefab;

    // Object which will contain instantiated prefabs in hiearchy
    [Header("Drop mark holder here")]
    public GameObject markHolder;







    //class that converts latitude / longitude to Unity position and the reverse
    //Got the formula from here
    //https://stackoverflow.com/questions/929103/convert-a-number-range-to-another-range-maintaining-ratio

    //convert a coordinate from one set of ranges to another set of ranges
    private float convertCoordinate(float oldValue, float oldMin, float oldMax, float newMin, float newMax)
    {
        float oldRange = oldMax - oldMin;
        float newRange = newMax - newMin;
        float returnValue = (((oldValue - oldMin) * newRange) / oldRange) + newMin;
        return returnValue;
    }

    //A LatLon Vector2 includes Latitude as the x value and Longitude as the y value
    //A Unity world coordinate has x as the west/east (longitude) and z as the north/sounth (latitude)

    //This method takes a LatLon Vector2 and translates it into this zone's game world coordinates
    //It does this by taking two points, a Noth West point and South East point in both LatLon and Unity world space positions to do the translation
    public Vector3 GetUnityPosition(Vector2 latLonPosition, Vector2 northWestLatLon, Vector2 southEastLatLon, Vector3 northWestUnity, Vector3 southEastUnity)
    {
        //check if this zone covers the antimeridian (where 180 and -180 degress longitude meet)
        if (southEastLatLon.y < northWestLatLon.y)
        {
            //Add 360 to any negative longitude positions so that longitude values are lower the further west
            southEastLatLon = new Vector2(southEastLatLon.x, southEastLatLon.y + 360f);
            if (latLonPosition.y < 0f)
            {
                latLonPosition = new Vector2(latLonPosition.x, latLonPosition.y + 360f);
            }
        }
        float newUnityLat = convertCoordinate(latLonPosition.x, southEastLatLon.x, northWestLatLon.x, southEastUnity.z, northWestUnity.z);
        float newUnityLon = convertCoordinate(latLonPosition.y, southEastLatLon.y, northWestLatLon.y, southEastUnity.x, northWestUnity.x);
        Vector3 unityWorldPosition = new Vector3(newUnityLon, 200f, newUnityLat);
        return unityWorldPosition;
    }





    // Start is called before the first frame update
    void Start()
    {
        // Get the Unity Vector3s
        northWestUnity1 = northWestUnity.position;

        southEastUnity1 = southEastUnity.position;



        // Set markList to results of function Reader with argument inputfile
        markList = CSVReader.Read(GPSpoints);

        //Log to console
        //Debug.Log(markList);


        // Declare list of strings, fill with keys (column names)
        List<string> columnList = new List<string>(markList[1].Keys);

        // Print number of keys (using .count)
        Debug.Log("There are " + columnList.Count + " columns in the CSV file");

        foreach (string key in columnList)
            Debug.Log("Column name is " + key);


        // Assign column name from columnList to Name variables
        name0 = columnList[lat_x];
        name1 = columnList[lon_y];


        //Loop through Pointlist
        //for (var i = 0; i < markList.Count; i++)
        for (var i = 0; i < markList.Count; i++)
        {

            // Get position and rotation value in markList at its "row", in "column" Name    
            Vector2 latlonInput;
            latlonInput.x = Convert.ToSingle(markList[i][name0]);
            latlonInput.y = Convert.ToSingle(markList[i][name1]);

            Vector3 gpsInUnity = GetUnityPosition(latlonInput, northWestLatLon, southEastLatLon, northWestUnity1, southEastUnity1);

            gpsInUnity.y = markPrefab.transform.position.y;


            // Instantiate as gameobject variable so that it can be manipulated within loop, the index comes from previous steps, pos and rot is from CSV
            GameObject markPoint = Instantiate(markPrefab, gpsInUnity, Quaternion.identity);

            // Make the markPoint as child of markHolder object, to keep points within container in hierarchy
            markPoint.transform.parent = markHolder.transform;


        }


    }


}
