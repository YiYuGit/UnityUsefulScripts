using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Work with VidepPlayer,VideoController,GPS dots holder, PlotTimeStampDots and other script and object to highlight the current gps dot on the top view map.
/// (to hide the gps dot from the main camera, put them in the Layer of MainCamIgnore and set the corresponding culling mask)
/// (example:
///         //Put the things that you don't want to show in Main camera into this cullingMask layer in the inspector
///         Camera.main.cullingMask = ~(1 << LayerMask.NameToLayer("MainCamIgnore"));
///         //
///  )
/// 
/// This script is used to highlight the current on the top view map.
/// It read the current gps dot name from the video controller, and get the current dot's Renderder
/// Turn the current dot's Renderer off. 
/// In the Update, when the video move to the next gps dot, the previous dot's Renderer will be turned back on.
/// By keep doing this, the current do on the map will be moving with the video play.
/// Highlighted by turning them on and off.
/// </summary>


public class HighLightCurrentDot : MonoBehaviour
{
    // Drag and drop the videoplayer/video controller object here. Or modifiy the script to find the object and get component of the video controller on start.
    public VideoController videoController;


    //public GameObject topViewCamera;

    private string currentNumber = "0";

    [SerializeField]
    private GameObject previousDot;
    
    private GameObject currentDot;

    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        currentNumber = videoController.gpsDotNumber.ToString();

        currentDot = GameObject.Find(currentNumber);

        rend = currentDot.GetComponent<Renderer>();

        rend.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (currentNumber != videoController.gpsDotNumber.ToString())
        {
            rend.enabled = true;

            currentNumber = videoController.gpsDotNumber.ToString();

            rend = GameObject.Find(currentNumber).GetComponent<Renderer>();

            rend.enabled = false;


        }

    }
}
