using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System;
using Esri.ArcGISMapsSDK.Components;
using System.Collections;
//using static UnityEditor.FilePathAttribute;

/// <summary>
///  
/// This script read the gpx file and plot the GPS points on the ArcGIS map
///  It also calculate the center of all the GPS points, assign that value to the ArcGIS map origin and the ArcGIS Camera 
///  (using AssignGPSLocation and ArcGISLocationComponent)
///  So the video path will be in the center of the map.
///  
/// The height of the camera may need to be exposed and linked to UI to give the user the option to zoom in/out.
///  
/// </summary>
public class StandaloneGPXReaderPlotter : MonoBehaviour
{

    // Put the name of the GPX file, without ".gpx". The file should be placed in the StreamingAssets folder's GPX folder
    public string GPXFileName = string.Empty;

    // These numbers are the calculated center coordinates of this gpx file. The center of the GPX points is used as the map origin of the ArcGIS map
    public double centerLat;
    public double centerLon;

    // Variables to store minimum and maximum latitude/longitude, they are used in the center point calculation
    [SerializeField]
    private float minLatitude = float.MaxValue;
    [SerializeField]
    private float maxLatitude = float.MinValue;
    [SerializeField]
    private float minLongitude = float.MaxValue;
    [SerializeField]
    private float maxLongitude = float.MinValue;

    // Define a data structure to hold the GPX data
    struct GPXData
    {
        public int sequence;
        public float latitude;
        public float longitude;
        public float elevation;
        public string timestamp;
    }

    // List to store GPX data
    private List<GPXData> gpxDataList = new List<GPXData>();

    // Drop the prefab of the GPS point here
    public GameObject pointPrefab;

    // Drop the holder of the GPS points here, in this case, the ArcGISMap gameobject
    public GameObject pointHolder;

    // The additional value to added to the raw elevation of the GPS points, to make sure they appear above ground
    public float heightOffset = 50f;

    // Referencing the AssignArcGISMapOrigin script. To assign the origin of the map.
    public AssignArcGISMapOrigin assignMapOrigin;

    // Temp use of these two scripts to assigin the location of each GPS point.
    private AssignGPSLocation location;
    private ArcGISLocationComponent locationComponent;


    // Start is called before the first frame update
    void Start()
    {
        // Assemble the Path to the GPX file. The GPX file name(without ".gpx") should be the same with the video (.mp4) file. 
        // So other script can link assign these names together
        string gpxFilePath = Application.streamingAssetsPath + "/GPX/" + GPXFileName + ".gpx";

        // Read the GPX file, and save data to the "gpxDataList"
        ReadGPXFile(gpxFilePath);

        // Afer reading the GPX file, the max/min of the lat/long are updated. Based on these numbers, the center can be calculated
        CalculateOriginAndCenter();

        // Assign center lat/long to the ArcGISMap (the AssignMapOrigin() will also update the map camera infomation)
        assignMapOrigin.longitude = centerLon;
        assignMapOrigin.latitude = centerLat;
        assignMapOrigin.AssignMapOrigin();

        // Plot all GPS points on the map
        PlotGPSPoints(gpxDataList);

    }


    private void PlotGPSPoints(List<GPXData> gpxDataList)
    {
        foreach (var data in gpxDataList)
        {
            // Instantiate prefab for each data entry in the list and put them under the ArcGISMap object
            GameObject gpsPoint = Instantiate(pointPrefab, pointHolder.transform);

            // Get the ArcGISLocationComponent from prefab and enable it ( this component cannot be enabled outside ArcGISMap parent)
            locationComponent = gpsPoint.GetComponent<ArcGISLocationComponent>();
            locationComponent.enabled = true;

            // Get the AssignGPSLocation component and assign the lat/long/alt value to the component
            location = gpsPoint.GetComponent<AssignGPSLocation>();
            location.latitude = Convert.ToDouble(data.latitude);
            location.longitude = Convert.ToDouble(data.longitude);
            location.altitude = Convert.ToDouble(data.elevation + heightOffset);

            // Rename the prefab by gpx file name and sequence number
            gpsPoint.name = GPXFileName + Convert.ToInt16(data.sequence);

            // Run the AssignPointInfo() function to push the info to the ArcGISLocationComponent
            location.AssignPointInfo();

            // Get the ClickPlayVideoByNameAndTime component from the prefab
            ClickPlayVideoByTime play = gpsPoint.GetComponent<ClickPlayVideoByTime>();

            // Assign some of the data to (the information from GPX is not complete, that's why this is a quick preview)
            play.latitude = Convert.ToDouble(data.latitude);
            play.longitude = Convert.ToDouble(data.longitude);
            play.elevationM = Convert.ToSingle(data.elevation);
            play.fileName = GPXFileName;
            play.startTime = data.sequence;

        }
    }


    private void CalculateOriginAndCenter()
    {
        // The center of the of the GPX data area is the half of max and min value
        centerLat = Convert.ToDouble((minLatitude + maxLatitude) / 2);
        centerLon = Convert.ToDouble((minLongitude + maxLongitude) / 2);


        // In global mode arcgis map, the origin is the centerpoint
        //originLat = centerLat - Convert.ToDouble((maxLatitude - minLatitude) / 2 * originFactor);
        //originLon = centerLon - Convert.ToDouble((maxLongitude - minLongitude) / 2 * originFactor);
    }


    private void ReadGPXFile(string filePath)
    {
        try
        {
            // Read the GPX file as the xml document
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            // Namespace manager for handling namespaces in GPX XML
            XmlNamespaceManager nsManager = new XmlNamespaceManager(xmlDoc.NameTable);
            nsManager.AddNamespace("gpx", "http://www.topografix.com/GPX/1/1");

            XmlNodeList trackPoints = xmlDoc.SelectNodes("//gpx:trkpt", nsManager);
            int currentSequence = 0; // Initialize sequence to 0

            foreach (XmlNode point in trackPoints)
            {
                GPXData data = new GPXData();
                data.sequence = currentSequence;
                data.latitude = float.Parse(point.Attributes["lat"].Value);
                data.longitude = float.Parse(point.Attributes["lon"].Value);
                data.elevation = float.Parse(point.SelectSingleNode("gpx:ele", nsManager).InnerText);
                data.timestamp = point.SelectSingleNode("gpx:time", nsManager).InnerText;

                gpxDataList.Add(data);

                // Update min/max latitude and longitude values
                minLatitude = Mathf.Min(minLatitude, data.latitude);
                maxLatitude = Mathf.Max(maxLatitude, data.latitude);
                minLongitude = Mathf.Min(minLongitude, data.longitude);
                maxLongitude = Mathf.Max(maxLongitude, data.longitude);

                currentSequence++; // Increment the sequence for the next point
            }
        }
        catch (IOException e)
        {
            Debug.LogError($"Error reading GPX file: {e.Message}");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
