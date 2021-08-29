using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script controls the camera to follow the player.
/// using LateUpdate for rendering related operations. 
/// </summary>
/// 

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    // Two way of offset, first is by set values, second is by initial position;
    //private Vector3 offset = new Vector3(0, 6, -7);
    [SerializeField] private Vector3 offset; 

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the offset by the initial 
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Update the Camera position with Player's initial offset
        transform.position = player.transform.position + offset; 
    }
}
