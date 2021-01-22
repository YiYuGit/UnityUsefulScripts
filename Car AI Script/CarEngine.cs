using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is based on the Unity Car AI tutorial https://www.youtube.com/watch?v=o1XOUkYUDZU&ab_channel=EYEmaginary with some modification
/// Using this script, in combination with CarWheel,CatulPath, SpawnCone
/// User can build a self driving car with obstacle avoiding capability
/// Attach this to the car object, Drag in the wheel colliders, and carpath, put the car in a road environment
/// </summary>


public class CarEngine : MonoBehaviour
{
    [Header("CarPath")]
    public CatmulPath path;

    [Header("WheelColliders")]
    public float maxSteerAngle = 45f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;

    //if 4 wheel drive or use as brake wheel
    public WheelCollider wheelRL;
    public WheelCollider wheelRR;

    public float approachDistance = 4f;
    public float turnSpeed = 6f;
    public float maxMotorTorque = 150f;
    public float maxBrakeTorque = 1500f;
    public float currentSpeed;
    public float maxSpeed = 100f;
    public Vector3 massCenter;

    public bool isBraking = false;
    public Texture2D textureNormal;
    public Texture2D textureBraking;
    public Renderer taillight;

    [Header("Sensors")]
    public float sensorLength = 8f;
    public Vector3 frontCenterSensorPos = new Vector3(0f, 0, 1.5f);
    public float frontSideSensorPos = 1f;
    public float frontSideAngle = 45f;
    public float frontSideAngle2 = 90f;
    public Vector3 rearLeftSensorLocation = new Vector3(-1f, 0, -1.8f);
    public Vector3 rearRightSensorLocation = new Vector3(1f, 0, -1.8f);
    public float rearSideAngle = 20f;


    private List<Vector3> nodes = new List<Vector3>();
    private int currentNode = 0;
    private bool avoiding = false;
    private float targetSteerAngle = 0;


    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = massCenter;
        nodes = path.target;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Sensors();
        ApplySteer();
        Drive();
        CheckWaypointDistance();
        Braking();
        LerpToSteerAngle();
    }

    private void Sensors()
    {
        RaycastHit hit;
        // front sensor start position, the location will be changed later
        Vector3 sensorStartPos = transform.position;
        sensorStartPos += transform.forward * frontCenterSensorPos.z ;
        sensorStartPos += transform.up * frontCenterSensorPos.y;

        //rear left sensor position
        Vector3 rearLeftSensorPos = transform.position;
        rearLeftSensorPos += transform.forward * rearLeftSensorLocation.z;
        rearLeftSensorPos += transform.up * rearLeftSensorLocation.y;
        rearLeftSensorPos += transform.right * rearLeftSensorLocation.x;

        // rear right sensor position
        Vector3 rearRightSensorPos = transform.position;
        rearRightSensorPos += transform.forward * rearRightSensorLocation.z;
        rearRightSensorPos += transform.up * rearRightSensorLocation.y;
        rearRightSensorPos += transform.right * rearRightSensorLocation.x;



        float avoidMultiplier = 0;
        avoiding = false;


        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength * 0.8f))
        {
            Debug.DrawLine(sensorStartPos, hit.point);
            avoiding = true;

            System.Random rnd = new System.Random();

            int caseSwitch = rnd.Next(1, 2);

            switch (caseSwitch)
            {
                case 1:
                    avoidMultiplier = Random.Range(2.75f, 4.5f);
                    break;

                case 2:
                    avoidMultiplier = Random.Range(-2.75f, -4.5f);
                    break;
            }

        }



        //front right sensor
        sensorStartPos += transform.right * frontSideSensorPos;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("terrain"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
                avoidMultiplier -= 1f;
            }
        }
        

        //front right angle sensor
        
        else if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(frontSideAngle,transform.up)*transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("terrain"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
                avoidMultiplier -= 0.5f;
            }
        }

        // front right angle sensor 2
        else if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(frontSideAngle2, transform.up) * transform.forward, out hit, sensorLength * 0.5f))
        {
            if (!hit.collider.CompareTag("terrain"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
                avoidMultiplier -= 0.25f;
            }
        }
        // rear right angle sensor 
        else if (Physics.Raycast(rearRightSensorPos, Quaternion.AngleAxis(rearSideAngle, transform.up) * transform.forward, out hit, sensorLength * 0.4f))
        {
            if (!hit.collider.CompareTag("terrain"))
            {
                Debug.DrawLine(rearRightSensorPos, hit.point);
                avoiding = true;
                avoidMultiplier -= 0.25f;
            }
        }






        //front left sensor
        sensorStartPos -= transform.right * frontSideSensorPos * 2;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("terrain"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
                avoidMultiplier += 1f;
            }
           
        }
        

        //front left angle sensor
        
        else if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-frontSideAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("terrain"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
                avoidMultiplier += 0.5f;
            }
        }


        // front left angle sensor 2
        else if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-frontSideAngle2, transform.up) * transform.forward, out hit, sensorLength * 0.5f))
        {
            if (!hit.collider.CompareTag("terrain"))
            {
                Debug.DrawLine(sensorStartPos, hit.point);
                avoiding = true;
                avoidMultiplier += 0.25f;
            }
        }

        // rear left angle sensor 
        else if (Physics.Raycast(rearLeftSensorPos, Quaternion.AngleAxis(-rearSideAngle, transform.up) * transform.forward, out hit, sensorLength * 0.4f))
        {
            if (!hit.collider.CompareTag("terrain"))
            {
                Debug.DrawLine(rearLeftSensorPos, hit.point);
                avoiding = true;
                avoidMultiplier += 0.25f;
            }
        }


        //front center sensor
        //sensorStartPos += transform.forward * frontCenterSensorPos.z;
        //sensorStartPos += transform.up * frontCenterSensorPos.y;

        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStartPos, hit.point);

            if (avoidMultiplier == 0)
            {
                if (!hit.collider.CompareTag("terrain"))
                {
                    
                    avoiding = true;
                    if(hit.normal.x < 0)
                    {
                        avoidMultiplier = -1;
                    }
                    else
                    {
                        avoidMultiplier = 1;
                    }
                }
            }
            
        }



        if (avoiding)
        {
            //wheelFL.steerAngle = maxSteerAngle * avoidMultiplier;
            //wheelFR.steerAngle = maxSteerAngle * avoidMultiplier;
            targetSteerAngle = maxSteerAngle * avoidMultiplier;
        }
        
    }


    private void ApplySteer()
    {
        if (avoiding) return;
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode]);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        //wheelFL.steerAngle = newSteer;
        //wheelFR.steerAngle = newSteer;
        targetSteerAngle = newSteer;
    }

    private void Drive()
    {
        currentSpeed = Mathf.Abs(2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000);

        if(currentSpeed < maxSpeed && !isBraking)
        {
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;

            //wheelRL.motorTorque = maxMotorTorque;
            //wheelRR.motorTorque = maxMotorTorque;
        }
        else
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;

            wheelRL.motorTorque = 0;
            wheelRR.motorTorque = 0;

        }
    }

    private void CheckWaypointDistance()
    {
        if(Vector3.Distance(transform.position,nodes[currentNode])< approachDistance)
        {
            
            if (currentNode == nodes.Count -1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }
    }

    private void Braking()
    {
        if(isBraking)
        {
            taillight.material.mainTexture = textureBraking;
            wheelRL.brakeTorque = maxBrakeTorque;
            wheelRR.brakeTorque = maxBrakeTorque;
        }
        else
        {
            taillight.material.mainTexture = textureNormal;
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
        }

    }
    private void LerpToSteerAngle()
    {
        wheelFL.steerAngle = Mathf.Lerp(wheelFL.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);
        wheelFR.steerAngle = Mathf.Lerp(wheelFL.steerAngle, targetSteerAngle, Time.deltaTime * turnSpeed);

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (currentNode <= nodes.Count - 1)
        {
            Gizmos.DrawLine(transform.position, nodes[currentNode]);
        }
            
    }
 
}
