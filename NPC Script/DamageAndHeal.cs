using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to the same object with 'FillCanvasBar'
/// The object should have collider and at least one of the colliding object should have rigidbody.
/// On trigger enter, the script can use tag or name to determine it's a weapon(should take damage) or medicine(should heal)
/// Whick will use the Minus() and Add() to change the fill bar
/// </summary>


public class DamageAndHeal : MonoBehaviour
{

    //The FillCanvas bar script
    public FillCanvasBar barScript;

    // Start is called before the first frame update
    void Start()
    {
        // Find the script
        barScript = GetComponent<FillCanvasBar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        // When hit weapon, take damage
        if (other.gameObject.CompareTag("weapon"))
        {
            barScript.Minus();
        }

        // When hit medicine, take heal
        // Can use object name or tag to execute trigger 
        //if (other.gameObject.CompareTag("medicine"))
        if (other.gameObject.name == "medicine")
        {
            barScript.Add();
        }
    }
}
