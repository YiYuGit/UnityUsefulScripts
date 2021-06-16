using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player control script
/// </summary>
public class FollowPlayerX : MonoBehaviour
{
    public GameObject plane;
    private Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        // Initialize the offset by the initial 
        offset = transform.position - plane.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = plane.transform.position + offset;
    }
}
