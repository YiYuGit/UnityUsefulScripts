using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/// <summary>
/// Attach this script to a collider obejct(isTrigger) with rigidbody(rigidbody turn off gravity)
/// When the collider hit pothole objct with"pothole" tag, it will trigger the two controller to shake.
/// </summary>

public class PotholeShake : MonoBehaviour
{
    // Each time setup, check the index for both controller and put them here.
    public int leftControllerIndex = 4;
    public int rightControllerIndex = 3;



    private SteamVR_Controller.Device ControllerLeft
    {
        get { return SteamVR_Controller.Input(leftControllerIndex); }
    }

    private SteamVR_Controller.Device ControllerRight
    {
        get { return SteamVR_Controller.Input(rightControllerIndex); }
    }





    private void Start()
    {


    }


    // The shaking function
    private void Shake()
    {
        ControllerLeft.TriggerHapticPulse(3500);
        ControllerRight.TriggerHapticPulse(3500);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pothole"))
        {
            Shake();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("pothole"))
        {
            Shake();
        }
    }
}
