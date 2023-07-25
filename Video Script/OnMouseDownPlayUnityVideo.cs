
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to the GPS dot prefab, the dot prefab need to have collider.
/// The video player object have VideoPlayer and Video Controller component.
/// The active controller have Object controller component
/// When instantiated, the startTime is assigned by the PlotTimeStampDots script, the data is from the GPS points csv file
/// After instantiation, each dot can be clicked.
/// When clicked, the script will find video and active controller object by name and get their component.
/// The dot it self will flicker a few times, indicating the dot was clicked.
/// Then, the video play time is changed the start time on this obejct.
/// In the end all Top map realted object will be set inactive to give the 360 video a clean view
/// 
/// After viewing the video section. User can turn the top map back by pressing a button, that is on the video controller script.
/// 
/// There are many variables stored in this script, they will be assigned by the PlotTimeStampDots script, and may be used for data logging or visualization 
/// 
/// </summary>


public class OnMouseDownPlayUnityVideo : MonoBehaviour
{
    // video player object, no need to drag and drop, it will be find by script
    public GameObject videoPlayer;

    // The "ObjActiveController" is the object that contain the SetActiveTF script.
    public GameObject activeController;

    // Start time is assigned by plot gps dot script
    public int startTime;

    // These are used for recording the data of this GPS point.
    public float latitude;
    public float longitude;
    public float elevationM;
    public float northing;
    public float easting;
    public float elevationFt;
    public string timeStamp;









    // renderer is used to show the dot clicked
    private MeshRenderer rend;

    // the VideoController script
    private VideoController vidControl;

    // the SetActiveTF script
    private SetActiveTF activeControl;

    private void OnMouseDown()
    {


        // find the 360VideoPlayer
        videoPlayer = GameObject.Find("360VideoPlayer");

        // find the ObjActiveController
        activeController = GameObject.Find("ObjActiveController");

        // get the VideoController component
        vidControl = videoPlayer.GetComponent<VideoController>();

        // get the SetActiveTF component
        activeControl = activeController.GetComponent<SetActiveTF>();

        // pass the video start time from gps dot to the video player
        vidControl.SetTime(startTime);

        // once the passing of the start time is done, turn off the active icons 
        activeControl.SwitchActive();

        // toggle the object's visibilit,y make the object flicker, 
        StartCoroutine(CDtime());
    }


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
    }

    IEnumerator CDtime()
    {
        rend.enabled = false;

        yield return new WaitForSeconds(.5f);

        rend.enabled = true;

        yield return new WaitForSeconds(.5f);

        rend.enabled = false;

        yield return new WaitForSeconds(.5f);

        rend.enabled = true;

        yield return new WaitForSeconds(.5f);

        rend.enabled = false;

    }
}
