using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{

    //public GameObject vehicle;
    public Rigidbody vehicle;
    public Text speedText;
    private int speed;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>(vehicle);
        
    }

    // Update is called once per frame
    void Update()
    {
        // MPH
        speed = Convert.ToInt32(vehicle.velocity.magnitude * 2.237);
        speedText.text = "E-Scooter Speed: " + speed + " MPH";

        // KPH
        //speed = Convert.ToInt32(rb.velocity.magnitude * 3.6);
        //speedText.text = "E-Scooter Speed: " + speed + " KPH";



    }
}
