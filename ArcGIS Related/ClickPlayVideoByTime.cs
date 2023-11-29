using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to GPS point prefab to keep the number and values of each point from the gpx data file.
/// This script work with StandaloneVideoController
/// StandaloneGPXReaderPlotter read data from source gpx file, instantiate the prefab with this script, 
/// move the prefab(point) to the correct posisiton, and assign the values to this scirpt variables on that point.
/// 
/// In runtime, when user click on the point, the value on the point will be given to the StandaloneVideoController to 
/// change the video clip and the start time of the video.
/// Other variables on this script can be used to record data into screenshot/txt file for photo log generating tasks.
/// 
/// Remember to put the point prefab into the layer of "MainCamIgnore" to be invisible in main camera.
/// 
///         // Put the things that you don't want to show in Main camera into this cullingMask layer in the inspector,
///         //if that layer does not exist, use " Add layer" to create a layer of your choice
///         Camera.main.cullingMask = ~(1 << LayerMask.NameToLayer("MainCamIgnore"));
/// 
/// Currently, to avoid the user mouse click in main camera view, this script use the top map camera active status to determine if 
/// the "on mouse down" should be executed.
/// In some cases, the top view may need to be turned off, consider disable the sphere collider component on this prefab to achieve same result of avoid click.
/// 
/// This is the standalone ArcGIS Basemap, 360 video app version.
/// 
/// </summary>

public class ClickPlayVideoByTime : MonoBehaviour
{
    // video player object, no need to drag and drop, it will be find by script
    public GameObject videoPlayer;

    // The "ObjActiveController" is the object that contain the SetActiveTF script.
    public GameObject activeController;

    // Drop the top map view camera gameobjet here, this is used to determine if a click on this gps point should trigger the OnMouseDown() function
    public GameObject topMapCamera;

    // Start time is assigned by plot gps point script
    public int startTime;

    // These are used for recording the data of this GPS point.
    public double latitude;
    public double longitude;
    public float elevationM;
    public double northing;
    public double easting;
    public float elevationFt;
    public string timeStamp;
    public string xdimM;
    public string ydimM;

    // This is for setting the video clip on the video player
    public string fileName;

    // Two materials for showing the status of the point. the clicked points will turn yellow
    public Material red;
    public Material yellow;

    // renderer is used to show if the point has been clicked
    private MeshRenderer rend;

    // the StandaloneVideoController script
    private StandaloneVideoController vidControl;

    // the SetActiveTF script
    private SetActiveTF activeControl;


    private void OnMouseDown()
    {
        // The function only execute when top map camera is active. so in video viewing mode, even if user clicked on the gps points,
        // because the top map camera is off, the content of this function won't be executed
        if (topMapCamera.activeSelf)
        {
            // Find the 360 video player
            videoPlayer = GameObject.Find("360VideoPlayer");

            // Find the ObjActiveController
            activeController = GameObject.Find("ObjActiveController");

            // Get the MultiVideoControler component
            vidControl = videoPlayer.GetComponent<StandaloneVideoController>();

            // Get the SetActiveTF component
            activeControl = activeController.GetComponent<SetActiveTF>();

            // Pass the video name and start time from GPS point to the StandaloneVideoController
            vidControl.JumpToVideoTime(startTime);

            // Once the passing of the start time is done, switch the active status of UI, TopViewMap, TopViewCam, some should be on, some should be off.
            activeControl.SwitchActive();

            // Optional, once clicked, change the color from red to yellow to highlight the clicked points
            //rend.material = yellow;
        }
        
    }


    // Start is called before the first frame update
    void Start()
    {
        // On start, find the TopMapCamera
        topMapCamera = GameObject.Find("ArcGISCamera");

        rend = GetComponent<MeshRenderer>();
    }

}
