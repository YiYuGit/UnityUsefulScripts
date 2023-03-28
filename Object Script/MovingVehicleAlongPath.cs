using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// This script is connected to a vehicle model and move the mode along a path.
/// 
/// If the vehicle's x y z axis are not aligned with the Unity's coordinate system
///  use a layer of empty gameobject to correct them, this script should be attached to the outmost layer
/// </summary>
public class MovingVehicleAlongPath : MonoBehaviour
{
    // Drop path object here
    public CatmulPath path;

    // Set moving speed
    public float speed;

    // Approaching distance is used to determine if next waypoint target shoule be loaded
    public float approachDistance = 1.0f;

    // Starting point of the target points
    public int current = 0;

    private List<Vector3> targetList;
    
    private Vector3[] target;


    // Use this for initialization
    void Start()
    {
        // Get path target points from catmul spline path
        targetList = path.target;
        target = targetList.ToArray();

    }

    // Update is called once per frame
    void Update()
    {
        //if (transform.position != target[current])
        // Check if the vehicle is approaching target point
        if (Vector3.Distance(transform.position, target[current]) >= approachDistance)
        {
            // use the MoveToWards to move vehicle to the target point
            transform.position = Vector3.MoveTowards(transform.position, target[current], speed * Time.deltaTime);

            //Rotate vehicle object toward target
            Vector3 targetDirTemp = target[current] - transform.position;

            float x = targetDirTemp.x;
            float z = targetDirTemp.z;
            Vector3 targetDir = new Vector3(-x, 0, -z);


            //Debug.Log(targetDir);

            // The step size is equal to speed times frame time.
            float step = speed * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            
            Debug.DrawRay(transform.position, newDir, Color.red);

            // Move our position a step closer to the target.
            transform.rotation = Quaternion.LookRotation(newDir);

        }

        else current = (current + 1) % target.Length;

    }
}
