using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used to rotate rotor/propeller gameobject in helopter/plane
/// attach the script to object, find the right axis and set the rotateSpeed
/// move the "Time.deltaTime*rotateSpeed" to the right position in the Vector3
/// This is for self-rotating objects. For roatating wheels ,there are other scripts.
/// </summary>


public class RotatingRotor : MonoBehaviour 

{

	//Rotate speed of the object
    public int rotateSpeed =1800;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(new Vector3(0, Time.deltaTime*rotateSpeed, 0));
	}
}
