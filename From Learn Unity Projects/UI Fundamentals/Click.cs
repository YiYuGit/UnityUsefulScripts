/**
* Click.cs when button is clicked, audio is played and console log displayed
* Author:  Lisa Walkosz-Migliacio  http://evilisa.com  12/18/2018
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour {

    public AudioSource audio;

	
    public void onClick()
    {
        if (audio) audio.Play();
        Debug.Log("clicked");
    }

}
