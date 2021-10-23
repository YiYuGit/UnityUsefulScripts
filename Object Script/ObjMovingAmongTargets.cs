using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script attach to a gameobject with rigidbody to move the object following target locations.
/// Uncheck the use gravity option in rigidbody.
/// </summary>


public class ObjMovingAmongTargets : MonoBehaviour
{
    // Input the target amounts and drop target by the moving sequence
    public Transform[] target;

    // Moving speed
    public float speed = 10f;

    // Approaching distance between object and target
    public float approachDist = 1f;

    // Start with the first target point
    private int current = 0;
    private Rigidbody body;


    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.isKinematic = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = Vector3.Distance(transform.position, target[current].position);
        if (dist > approachDist)
        //if (transform.position != target[current].position)

        {
            // Move object
            Vector3 pos = Vector3.MoveTowards(transform.position, target[current].position, speed * Time.deltaTime);
            //GetComponent<Rigidbody>().MovePosition(pos);
            body.MovePosition(pos);


            //Rotate object toward target (not including y axis)
            Vector3 targetDirTemp = transform.position - target[current].position;

            float x = targetDirTemp.x;
            float z = targetDirTemp.z;
            Vector3 targetDir = new Vector3(x, 0, z);

            //Rotate object toward target (including y axis)
            //float x = targetDirTemp.x;
            //float y = targetDirTemp.y;
            //float z = targetDirTemp.z;
            //Vector3 targetDir = transform.position - target[current].position;


            //Debug.Log(targetDir);

            // The step size is equal to speed times frame time.
            float step = speed * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            //Debug.DrawRay(transform.position, newDir, Color.red, 2 , false);

            // Move our position a step closer to the target.
            transform.rotation = Quaternion.LookRotation(newDir);

        }
        else
        {
            if (current == target.Length - 1)
            {
                current = 0;
            }
            else
            {
                current++;
            }
            //current = (current + 1) % target.Length;
        }


        //DebugLog to see the velocity in meter/s  when rigidbody isKinematic
        //Debug.Log(Convert.ToInt32(body.velocity.magnitude));

    }
}
