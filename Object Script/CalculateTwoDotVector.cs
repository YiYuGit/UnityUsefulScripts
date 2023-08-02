using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script calculate the vector3 between two points. 
/// The object can be find by name, or modify it to the drag and drop 
/// On start, the script find the object and use their transform.location to calculate the heading from first dot to second dot.
/// The original heading vector, the normalized heading, and the angle from normalized heading to north( 0,0,1) are shown in Debug.Log
/// 
/// The normalization of the heading vector seems to be not necessary.
/// 
/// The content of this script is also included in the CalculateCameraHeading.cs
/// 
/// 
/// </summary>

public class CalculateTwoDotVector : MonoBehaviour
{
    public string firstDotName;
    public string secondDotName;
    public Vector3 heading;


    private GameObject firstDot;
    private GameObject secondDot;
    [SerializeField]
    private Vector3 gpsHeadingNormalized;

    public Vector3 north = new Vector3(0, 0, 1);

    [SerializeField]
    private float northToGpsHeading;


    // Start is called before the first frame update
    void Start()
    {
        firstDot = GameObject.Find(firstDotName);
        secondDot = GameObject.Find(secondDotName);

        // The heading from first dot to the second dot
        heading = secondDot.transform.position - firstDot.transform.position;

        Debug.Log("Heading");
        Debug.Log(heading);
        gpsHeadingNormalized = heading.normalized;
        Debug.Log("headingNormalized");
        Debug.Log(gpsHeadingNormalized);

        // The angle from north to the previously calcualted heading. pay attention to the signed angle, if you need all positive number, add 360 to the negative values.
        northToGpsHeading = Vector3.SignedAngle(north, gpsHeadingNormalized, Vector3.up);
        Debug.Log("North to GPS heading");
        Debug.Log(northToGpsHeading);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
