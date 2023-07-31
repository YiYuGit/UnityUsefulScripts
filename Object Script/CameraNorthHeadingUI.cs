using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This script is attached to the TMP text on the canvas
/// It can read the camera north heading and display on the UI
/// if you want it to be captured by the screen shot, place this tmp text in the canvas that is camera mode
/// otherwise, put it in canvas - overlay mode
/// </summary>


public class CameraNorthHeadingUI : MonoBehaviour
{
    // The UI text for displaying the camera heading
    public TMP_Text angleText;

    // The empty gameobject that have attached script of "CalculateCameraHeading"
    public GameObject cameraAngle;

    // The script of "CalculateCameraHeading"
    private CalculateCameraHeading heading;

    // The camera heading angle (from north)
    private float angle;


    // Start is called before the first frame update
    void Start()
    {
        // Get the TMP text
        angleText = GetComponent<TMP_Text>();

        // Get the CalculateCameraHeading
        heading = cameraAngle.GetComponent<CalculateCameraHeading>();

        // Read the angle of north to camera
        angle = heading.northToCameraAngle;

    }

    // Update is called once per frame
    void Update()
    {
        angle = heading.northToCameraAngle;

        if (angle < 0)
        {
            angle = angle + 360f;
        }

        // Update the text of angle text to correct format
        angleText.text = Mathf.RoundToInt(angle).ToString();

    }
}
