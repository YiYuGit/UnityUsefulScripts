using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script can use the Rigidbody to calculate the speed of game object
/// And the speed is displayed with Text
/// The speed is available in both MPH and KPH
/// Also, this script can be integrated into the player controller script.
/// </summary>



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
        
        // The "Convert.ToInt32()" can be relaced by Mathf.Round()
        // The Text can be replaced by "TextMeshProUGUI" , need "using TMPro" in script, and "TextMeshPro - Text" in inspector
        
        // If using "WheelCollider.rpm", a simple simulated RPM display can also be achieved. RPM can also be simulated by Modulus/Remainder operator (%) of speed
        // Like ((speed % 30) *40)
        



    }
}
