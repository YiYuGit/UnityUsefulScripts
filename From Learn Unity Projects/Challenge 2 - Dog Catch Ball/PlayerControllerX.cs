﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    //dog prefab
    public GameObject dogPrefab;

    //cool down time
    private float cdTime = 1.0f;

    // timer time
    private float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        // update our timer 
        timer += Time.deltaTime;

        // On spacebar press, send dog
        // if timer is up and spacebar press, send dog
        if (timer > cdTime && Input.GetKeyDown(KeyCode.Space))
        { 
           Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);

            timer = 0.0f;
        }
    }
}
