/**
 * goOffscreen.cs Set animator boolean to false when invoked
 * Author:  Lisa Walkosz-Migliacio  http://evilisa.com  11/28/2018
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goOffscreen : MonoBehaviour {

    public List<GameObject> MoveObjects;

	
    public void moveOffscreen()
    {
        
        foreach (GameObject obj in MoveObjects)
        {
            obj.GetComponent<Animator>().SetBool("onScreen", false);
        }
    }


