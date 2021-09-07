using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Once triggered by user, the ca stopbox will be moved to the side, so the car can start drive
/// </summary>


public class CarStartTrigger : MonoBehaviour {

	public GameObject stopbox;

	// Use this for initialization
	void Start () 
	{
		stopbox.SetActive(true);

	}


	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("user"))
			transform.Translate(0f,0f,-1000f*Time.deltaTime);
	}


}
