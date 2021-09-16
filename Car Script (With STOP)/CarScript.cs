using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class CarScript : MonoBehaviour {
    private WaypointProgressTracker tracker;
    private Rigidbody rigidBody;
    public CrosswalkScript crosswalk;
    private bool crossing = true;


	private bool othercar = false;
	private bool user = false;

    private const float TURNING_RATE = 100;
    //private const float ACCEL_RATE = 20;
	private const float ACCEL_RATE = 40;

    void Start()
    {
        tracker = GetComponentInChildren<WaypointProgressTracker>();
        rigidBody = GetComponent<Rigidbody>();
	
    }


	void OnTriggerEnter(Collider other) 
	{
		if (other.CompareTag ("car"))
			//Destroy (other.gameObject);
			othercar = true;

		if (other.CompareTag ("user"))
			//Destroy (other.gameObject);
			user = true;

		if (other.CompareTag ("green"))
			//Destroy (other.gameObject);
			user = false;

	}


    void OnTriggerStay(Collider collider)
    {
        if (!collider.isTrigger && crossing)
        {
            crosswalk = collider.GetComponent<CrosswalkScript>();
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (!collider.isTrigger)
        {
            CrosswalkScript temp = collider.GetComponent<CrosswalkScript>();

            if (temp != null && temp == crosswalk)
            {
                crosswalk = null;
                crossing = true;
            }
        }

		if (collider.CompareTag ("car"))
			//Destroy (other.gameObject);
			othercar = false;

		if (collider.CompareTag ("user"))
			//Destroy (other.gameObject);
			user = false;

    }

    void Update()
    {
        Vector3 aggForce = tracker.target.position - transform.position;
        aggForce = aggForce.normalized * 8;
        aggForce.y = 0;
		if (crosswalk != null && (crosswalk.waitingPedestrians.Count >= 2 || crosswalk.userPresent) )
        {
            rigidBody.velocity = Vector3.zero;
            aggForce = Vector3.zero;
            
            crossing = false;
        }

		if (othercar == true || user == true ) 
		{
			rigidBody.velocity = Vector3.zero;
			aggForce = Vector3.zero;

			crossing = false;


		}


        if (aggForce != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(aggForce, Vector3.up), Time.deltaTime * TURNING_RATE);
            //transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
        //if (rigidBody.velocity != Vector3.zero)
        //{
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(rigidBody.velocity, Vector3.up), Time.deltaTime * TURNING_RATE);
        //    //transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        //}
        rigidBody.velocity = Vector3.MoveTowards(rigidBody.velocity, aggForce, Time.deltaTime * ACCEL_RATE);//transform.forward*10;

        Debug.DrawLine(transform.position, transform.position + transform.forward, Color.blue);
    }
}
