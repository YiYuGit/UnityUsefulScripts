using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script can turn off the object meshrender when player touch the object trigger,
/// and turn the object meshrener back on when player leave the object trigger
/// This will make the effect of touching and making object transparent.
/// Touch - On - Off
/// </summary>

public class TouchOnOff : MonoBehaviour
{
    // the meshrender will be get set on Start()
    public MeshRenderer rend;


    void Start()
    {
        rend = GetComponent<MeshRenderer>();

        //rend = GetComponentsInChildren<MeshRenderer>();
        rend.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rend.enabled = false;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rend.enabled = true;
        }
    }

}



