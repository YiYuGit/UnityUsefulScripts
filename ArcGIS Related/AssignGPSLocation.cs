using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Esri.ArcGISMapsSDK.Utils.GeoCoord;
using Esri.ArcGISMapsSDK.Utils.Math;
using Esri.GameEngine.Geometry;
using Esri.HPFramework;
using Unity.Mathematics;
using Esri.ArcGISMapsSDK.Components;

/// <summary>
/// This script is used with ArcGIS unity sdk to assign lat/long/alt GPS location and heading/pitch/roll rotation of a point/object 
/// 
/// The longitude; latitude; altitude; heading; pitch; roll; are all double float.
/// 
/// The objects with ArcGISLocationComponents need to be under ArcGISMap object. If you are instantiating objects with this component.
/// consider directly instantiate the object as child "Instantiate(someObject, someParent);" 
/// or turn off this compoenent on the prefab, after instantiaion and move obejct as child to ArcGISMap
/// then enable the ArcGILLocation component.
/// 
/// To use this AssignGPSLocaiton script, need to change the ArcGISLocationComponent script in the ArcGSI SDK,
/// to expose the Position and Rotation variables to public. 
/// </summary>

public class AssignGPSLocation : MonoBehaviour
{
    private ArcGISLocationComponent locationComponent;

    public double longitude;
    public double latitude;
    public double altitude;

    public double heading;
    public double pitch;
    public double roll;

    public ArcGISPoint pointPosition;
    public ArcGISRotation pointRotation;
    public ArcGISSpatialReference WKID;

    // Start is called before the first frame update
    void Start()
    {

        //AssignPointInfo();

    }

    public void AssignPointInfo()
    {
        // Get the ArcGISLocationComponent from the same game object
        locationComponent = GetComponent<ArcGISLocationComponent>();

        // Set the WKID
        // Reference: https://developers.arcgis.com/documentation/spatial-references/ 
        WKID = new ArcGISSpatialReference(4326);

        // Assemble two variable for the ArcGISLocationComponent
        pointPosition = ArcGISPoint.CreateWithM(longitude, latitude, altitude, 1d, WKID);

        pointRotation = new ArcGISRotation(heading, pitch, roll);

        // Assign the position and rotation
        locationComponent.Position = pointPosition;

        locationComponent.Rotation = pointRotation;

    }


    // Update is called once per frame
    void Update()
    {

    }
}
