using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Video;

/// <summary>
/// This script work with several other script to calculate the camera heading in the real world. ( the heading is in degree, from North direction to the camera heading, if the number is negative, add 360 degree)\
/// the input are :
/// main camera object
/// video controller that is on the video player objet
/// 
/// the current gps dot name comes from the video player's video time, this is based on the gps dots are 1 second away from each other.
/// if the time spacing between dots are different, modify the naming of the first and second dots accordingly, or change the way the script get the dots, maybe assigned by other script.
/// 
/// </summary>

public class CalculateCameraHeading : MonoBehaviour
{
    // Drop the main camera here
    public GameObject mainCam;
    
    // video player object, no need to drag and drop, it will be find by script
    private GameObject videoPlayerObj;

    // video player component of the videoPlayerObj
    //private VideoPlayer videoPlayer;
    
    // Video controller component of the video player
    private VideoController controller;

    // the current gps dot and next gps dot
    [SerializeField]
    private string firstDotName, secondDotName;

    //[SerializeField]
    private GameObject firstDot, secondDot;

    [SerializeField]
    private Vector3 gpsDotHeading;

    private Vector3 north = new Vector3(0, 0, 1);

    [Header("North to GPS Heading Angle (Degree)")]
    [SerializeField]
    private float northToGpsAngle;

    [Header("Camera Initial Heading Angle (Degree)")]
    [SerializeField]
    private float cameraInitialAngle = 0f;
    // Or use a fixed angle
    // public float cameraInitialAngle = 152f;

    [Header("Camera Current Heading Angle (Degree)")]
    [SerializeField]
    private float cameraCurrentAngle;

    [Header("North to Camera Screenshot Heading Angle (Degree)")]
    public float northToCameraAngle;



    public void FindGpsDot()
    {
        videoPlayerObj = GameObject.Find("360VideoPlayer");

        //videoPlayer = videoPlayerObj.GetComponent<VideoPlayer>();

        controller = videoPlayerObj.GetComponent<VideoController>();

        firstDotName = controller.gpsDotNumber.ToString();

        secondDotName = (controller.gpsDotNumber + 1).ToString();


    }    

    public void NorthToGpsAngle()
    {

        firstDot = GameObject.Find(firstDotName);

        secondDot = GameObject.Find(secondDotName);

        gpsDotHeading = secondDot.transform.position - firstDot.transform.position;

        northToGpsAngle = Vector3.SignedAngle(north, gpsDotHeading, Vector3.up);
    }

    public void TakeCamInitialAngle()
    {
        cameraInitialAngle = mainCam.transform.eulerAngles.y;

    }

    public void TakeCamCurrentAngle()
    {
        cameraCurrentAngle = mainCam.transform.eulerAngles.y;
    }

    public void NorthToCameraAngle()
    {
        TakeCamCurrentAngle();
        northToCameraAngle = northToGpsAngle + cameraCurrentAngle - cameraInitialAngle;
    }


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        // press one key, the script will find the gps dots, calculate the north to gps angle, and record the initial camera angle
        if (Input.GetKeyDown(KeyCode.A))
        {

            FindGpsDot();

            NorthToGpsAngle();

            TakeCamInitialAngle();
        }


        // on update, keep updating the north to camera angle
        // this number is public, can be used by other script. for example, when taking a screenshot, record the north to camera angle into a txt/csv file or 
        // link this number to the UI, so the screenshot can include the text 
        if (cameraInitialAngle != 0f)
        {
            NorthToCameraAngle();
        }
    }
}
