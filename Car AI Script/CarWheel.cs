using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used to rotate the car wheel model to the same as wheel colider.
/// Attach this to wheel mesh and drop the corresponding wheelcollider to the target, the wheel mesh will rotate with the wheel collider
/// </summary>

public class CarWheel : MonoBehaviour
{

    public WheelCollider target;

    private Vector3 wheelPosition = new Vector3();
    private Quaternion wheelRotation = new Quaternion();

   
    // Update is called once per frame
    void Update()
    {
        target.GetWorldPose(out wheelPosition, out wheelRotation);
        transform.position = wheelPosition;
        transform.rotation = wheelRotation;
    }
}
