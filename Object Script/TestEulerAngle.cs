using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Experimental Script.
/// 
/// This script check the Euler Angle y of the target object
/// the number in degree is updated every frame and is shown in the Inspector
/// 
/// Or show the angle on UI
///   
///   to read the euler angles around y axle
///   angle = target.transform.eulerAngles.y;
///   
///   to calculate a vector3
///   gpsDotHeading = secondDot.transform.position - firstDot.transform.position;
///   
///   to calculate the angle between two vector3
///   northToGpsAngle = Vector3.SignedAngle(north, gpsDotHeading, Vector3.up);
/// 
/// 
/// 
/// </summary>


public class TestEulerAngle : MonoBehaviour
{
    // Drop the game object here, usually the camera or player
    public GameObject target;

    // Show the Euler angle around y in degrees at runtime in the Inspector
    public float angle;


    // Start is called before the first frame update
    void Start()
    {
        angle = target.transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        angle = target.transform.eulerAngles.y;
    }
}
