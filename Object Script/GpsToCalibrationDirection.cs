using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Experimental script, currently not in use.
/// 
/// It calculate the signed angle between two vectors (two vectors come from two set of dots)
/// 
/// This calculate the angle from gps (two dots) to clibration(two objects).
/// 
/// If angle is needed for one object, use the TestEulerAngle instead.
/// 
/// angle = target.transform.eulerAngles.y;
/// 
/// Also, when calcualting angle between two Vector3s, it seems that the Normalized is not necessary.
/// 
/// 
/// </summary>

public class GpsToCalibrationDirection : MonoBehaviour
{
    public string firstDotName;
    public string secondDotName;
    public Vector3 gpsHeading;

    public string calibrationDotName1;
    public string calibrationDotName2;
    public Vector3 calibrationHeading;


    private GameObject firstDot;
    private GameObject secondDot;

    private GameObject calibrationDot1;
    private GameObject calibrationDot2;

 
    private Vector3 gpsHeadingNormalized;
    private Vector3 calibrationHeadingNormalized;

    [SerializeField]
    private float gpsHeadingToCalibration;


    // Start is called before the first frame update
    void Start()
    {
        firstDot = GameObject.Find(firstDotName);
        secondDot = GameObject.Find(secondDotName);

        gpsHeading = secondDot.transform.position - firstDot.transform.position;

        calibrationDot1 = GameObject.Find(calibrationDotName1);
        calibrationDot2 = GameObject.Find(calibrationDotName2);

        calibrationHeading = calibrationDot2.transform.position - calibrationDot1.transform.position;


        gpsHeadingNormalized = gpsHeading.normalized;

        calibrationHeadingNormalized = calibrationHeading.normalized;

        gpsHeadingToCalibration = Vector3.SignedAngle(gpsHeadingNormalized, calibrationHeadingNormalized, Vector3.up);

    }
}
