using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to a missile launcher object, which can detect player/user when they are in it's collider's range
/// and then will aim the launcher toward the player/user's direction, 
/// then shoot the weapon (bullet/missile/rocket...prefab)) in the direction of the target at the setting shoot interval
/// </summary>

public class MissileLauncher : MonoBehaviour {

    //Put the user/player here as the aircraft
    public GameObject aircraft;
    //Weapon turning speed
    public float turningSpeed = 10f;
    //The detecting range of the missile launcher, note that the range is for deteting and rotate launhcer, the shooting status is determined by the sphere trigger collider of the object
    //Ideally, the launhcer will detect the aircraft first at a longer distance, and start shooting when hit the trigger at a shorter distance.
    public float missileDetectRange = 100f;


    //Drag in the Bullet Emitter from the Component Inspector.
    public GameObject Bullet_Emitter;

    //Drag in the Bullet Prefab from the Component Inspector.
    public GameObject Bullet;

    //Enter the Speed of the Bullet from the Component Inspector.
    public float Bullet_Forward_Force;

    //The CD time of weapon
    public float shootIntervals = 3;

    private bool alive = false;

    // Use this for initialization
    void Start ()
    {
            //StartCoroutine("Shooting");

    }

    IEnumerator Shooting()
    {
        while (alive)
        {

            yield return new WaitForSeconds(shootIntervals);
            //The Bullet instantiation happens here.
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

            //Sometimes bullets may appear rotated incorrectly due to the way its pivot was set from the original modeling package.
            //This is EASILY corrected here, you might have to rotate it from a different axis and or angle based on your particular mesh.
            //Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

            //Retrieve the Rigidbody component from the instantiated Bullet and control it.
            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

            //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force. 
            Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);

            //Basic Clean Up, set the Bullets to self destruct after 10 Seconds, I am being VERY generous here, normally 3 seconds is plenty.
            Destroy(Temporary_Bullet_Handler, 1.6f);

            

            /*
            if (Vector3.Distance(aircraft.transform.position, transform.position) < missileDetectRange)
            {
                yield return new WaitForSeconds(shootIntervals);

                //The Bullet instantiation happens here.
                GameObject Temporary_Bullet_Handler;
                Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

                //Sometimes bullets may appear rotated incorrectly due to the way its pivot was set from the original modeling package.
                //This is EASILY corrected here, you might have to rotate it from a different axis and or angle based on your particular mesh.
                Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

                //Retrieve the Rigidbody component from the instantiated Bullet and control it.
                Rigidbody Temporary_RigidBody;
                Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

                //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force. 
                Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);

                //Basic Clean Up, set the Bullets to self destruct after 10 Seconds, I am being VERY generous here, normally 3 seconds is plenty.
                Destroy(Temporary_Bullet_Handler, 4.0f);

            }
            */

        }

    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("user"))
        {

            alive = true;
            StartCoroutine("Shooting");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("user"))
        {

            alive = false;
            //StartCoroutine("Shooting");
        }
    }

    // Update is called once per frame
    void Update () {

        //Detect if aircraft is in range of missile launcher

        if (Vector3.Distance(aircraft.transform.position, transform.position) < missileDetectRange)
        {
          
            //Rotate object toward target
            Vector3 targetDirTemp = aircraft.transform.position - transform.position;

            float x = targetDirTemp.x;
            float y = targetDirTemp.y;
            float z = targetDirTemp.z;
            Vector3 targetDir = new Vector3(x, y, z);

            //Debug.Log(targetDir);

            // The step size is equal to speed times frame time.
            float step = turningSpeed * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            Debug.DrawRay(transform.position, newDir, Color.red);

            // Move our position a step closer to the target.
            transform.rotation = Quaternion.LookRotation(newDir);

        }
        

    }
}
