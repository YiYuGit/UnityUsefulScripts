using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used to rotate rotor/propeller gameobject in helopter/plane
/// attach the script to object, find the right axis using (  x y z in Vector3(0, 0, Time.deltaTime * rotateSpeed))
/// and set the rotateSpeed
/// </summary>


public class RotatingRotor : MonoBehaviour {

	//Rotate speed
    public int rotateSpeed =1800;
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(new Vector3(0, 0, Time.deltaTime * rotateSpeed));
	}
}
