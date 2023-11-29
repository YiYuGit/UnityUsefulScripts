using Esri.ArcGISMapsSDK.Components;
using Esri.ArcGISMapsSDK.Utils.GeoCoord;
using Esri.GameEngine.Geometry;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  This script work with the ArcGISMap object. The Map Type of the ArcGISMap is global. 
///  The script will take user input of longitude, latitude and altitude and assign the value to the ArcGISMap and ArcGISCamera
///  so the game view is right above the lon/lat corrdinate at the designed height(altitude)
///  this can be used with the GPX reader/plotter script. That script calculate the center of the GPX path area and assign the value to this script.
///  In this way, the map can be automatically determined by the GPX inptut.
///  
/// To use this script, the OriginPosition in the ArcGISMap script need to be changed to public.
/// The AssignGPSLocaiton script also need to change the ArcGISLocationComponent, to expose the Position and Rotation. 
/// </summary>

public class AssignArcGISMapOrigin : MonoBehaviour
{
    //The ArcGISMapComponent to be changed
    private ArcGISMapComponent mapComponent;

    // Map origin data
    public double longitude;
    public double latitude;
    public double altitude;

    // The height of the camera.For larger area, this number should be bigger.
    public double ArcGISCameraHeightOffset = 1000d;

    // The map origin data will be written into this "originPosition" and assigned to the ArcGISMapComponent
    public ArcGISPoint originPosition;
    public ArcGISSpatialReference WKID;

    private AssignGPSLocation assignCameraLocation;


    // Start is called before the first frame update
    void Start()
    {
        //AssignMapOrigin();
    }

    public void AssignMapOrigin()
    {
        // This script should be attached to the game object with ArcGISMapComponent
        mapComponent = GetComponent<ArcGISMapComponent>();

        // default set tt 4326 (WGS 84)
        // See https://developers.arcgis.com/documentation/spatial-references/
        WKID = new ArcGISSpatialReference(4326);

        originPosition = ArcGISPoint.CreateWithM(longitude, latitude, 0d, 1d, WKID);

        mapComponent.originPosition = originPosition;

        // Assign map origin first, then update the camera
        UpdateCamera();

    }

    private void UpdateCamera()
    {
        // The ArcGISCamera is the child of the map obejct
        Transform childTransform = transform.Find("ArcGISCamera");

        if (childTransform != null)
        {
            // Get the AssignGPSLocation component from the child GameObject
            assignCameraLocation = childTransform.GetComponent<AssignGPSLocation>();

            if (assignCameraLocation != null)
            {
                assignCameraLocation.longitude = longitude;
                assignCameraLocation.latitude = latitude;
                assignCameraLocation.altitude = altitude + ArcGISCameraHeightOffset;

                assignCameraLocation.AssignPointInfo();
            }
            else
            {
                Debug.LogWarning("Component not found on the child GameObject.");
            }
        }
        else
        {
            Debug.LogWarning("Child GameObject not found.");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
