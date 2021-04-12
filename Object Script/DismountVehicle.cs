using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach this script to an object with trigger and put at the location you want to collide with vehicle
/// CameraRig is where you put player/VRCameraRig.When hte cameraRig is on the vehicle, the cameraRig's parenet is the vehicle
/// On Trigger Enter, if the vehicle is the tag.
/// The script will switch the camerarig parent to the LocationCube and change the camerarig location.
/// In this way, the player dismount the vehicle
/// 
/// </summary>
public class DismountVehicle : MonoBehaviour {

    //Put the player and the dismount location here
    public GameObject cameraRig;
    public GameObject LocationCube;

    // Use this for initialization
    void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("vehicle"))
        {

            cameraRig.transform.parent = LocationCube.transform;
            cameraRig.transform.position= LocationCube.transform.position;
            Debug.Log("cameraRig's Parent: " + cameraRig.transform.parent.name);
        }
    }
}
