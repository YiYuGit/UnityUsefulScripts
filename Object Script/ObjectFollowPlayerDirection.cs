using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used to attach to the vision area polygon mesh,
/// since the mesh is a plane on the x and z surface, 
/// it will keep updating the relative x and z position with player( or just player head)
/// and for y position it stay on the same height
/// for rotation , it only rotate same degree with player( player head)
/// If there are difficulty direct linking object and player, a layer of empty gameobject can be used as a "shell"
/// 
/// In this way, the mesh can sweep the ground and detect ground grid mesh to record the visibility .
/// </summary>
public class ObjectFollowPlayerDirection : MonoBehaviour
{
    //Put player object here
    public GameObject player;

    // The initial offset,
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
        // Update the mesh position with Player's initial offset
        //transform.position = player.transform.position + new Vector3(offset.x, offset.y , offset.z);
        transform.position = player.transform.position + offset;

        // Update the mesh rotation with Player's y rotation, x and z keep the same
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, player.transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
