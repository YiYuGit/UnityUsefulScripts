﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// This script attach to a parent gameobject, and take a public gameobject 'a'. 
    /// On start, the script will create 'a's at all the children gameobject position and rotations.
    /// This can be used when you need to create gameobjects at certain locations. 
    /// Empty gameobject can be used as place holder
    /// The place holder can be turned off (optional) after new object creation is done.
    /// </summary>

public class CreateObjectToChildrenPos : MonoBehaviour
{

    public GameObject a;

    // Start is called before the first frame update
    void Start()
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            GameObject temp = Instantiate(a);
            temp.transform.position = child.position;
            temp.transform.rotation = child.rotation;

            //child.gameObject.SetActive(false);
        }
    }
}
