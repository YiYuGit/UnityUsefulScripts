using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to the UI compass image to show the heading of the phone.
/// Pay attention to the Quaternion.Eular(), in this script, the image is on the UI canvas, the rotation is on Z axis
/// In other environment, consider chaning the Z axis to x or y, or add minus sign infront of the current heading bumber
/// </summary>



public class CompassController : MonoBehaviour
{
    // Adjust this value for smoothing (0.1 - 0.5 recommended)
    public float smoothingFactor = 0.2f;

    private float currentHeading;
    void Update()
    {
        // Get the compass heading
        float rawHeading = Input.compass.trueHeading;

        // Apply smoothing to the heading using Exponential Moving Average (EMA)
        currentHeading = Mathf.LerpAngle(currentHeading, rawHeading, smoothingFactor);

        // Rotate an arrow or object to show the compass direction
        transform.rotation = Quaternion.Euler(0, 0, currentHeading);
    }
}
