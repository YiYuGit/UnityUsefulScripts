using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach this script to an object with trigger, on trigger enter, the script will instantiate at the 'location' position
/// The 'location'object can be empty or with component.
/// </summary>


public class TouchToInstantiateObj : MonoBehaviour {

    [SerializeField]
    public GameObject prefab;
    public GameObject location;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("user"))
        {
            GameObject newItem = Object.Instantiate(prefab);
            newItem.transform.position = location.transform.position;
        }
    }
}
