wusing System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach this script to an object with trigger, on trigger enter of user, the script will instantiate the prefab object at the 'location' position
/// The 'location'object can be empty or with component.
/// </summary>


public class TouchToInstantiateObj : MonoBehaviour {

    [SerializeField]
    // Drop prefab here
    public GameObject prefab;
    // Drop the location object here
    public GameObject location;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("user"))
        {
            GameObject newItem = Object.Instantiate(prefab);
            newItem.transform.position = location.transform.position;
        }
    }
}
