using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script can turn the sign object on and off by touch the attached object with trigger.
/// For example, the 'sign' object can be a UI/hint/light or anything you want to control with a switch.
/// 
/// </summary>

public class TouchSwitch : MonoBehaviour {

    public GameObject sign;
	// Use this for initialization
	void Start () 
    {
        sign.SetActive(false);

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sign.SetActive(!sign.activeSelf);
       
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
