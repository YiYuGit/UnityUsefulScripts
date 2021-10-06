using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScooterController : MonoBehaviour
{
    // Get rigidbody of the vehicle player, and change center of mass 
    private Rigidbody rb;
    public Transform centerOfMass;

    // Get input from keyboard
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbrakeForce;
    private bool isBraking = false;

    [SerializeField] private float motorForce;
    [SerializeField] private float brakeForce;
    [SerializeField] private float maxSteerAngle;

    // Speed limit
    public int speedLimit;
    private int currentSpeed;


    // Wheel colliders, four wheel for stable vehicle
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;

    // For bike, motorcycle, scooter, only rotate two wheel models, no wheel colliders on them
    public Transform frontWheelModel;
    public Transform rearWheelModel;

    // The T shape handle for vehicle, rotate for visual 
    public Transform Tbar;


    private void Start()
    {
       rb = GetComponent<Rigidbody>();
       rb.centerOfMass = centerOfMass.localPosition;

        // Use this when build into exe, and the speedLimit value need to be set to a value before the actual playing scene.
        // For in editor playing, just assign a value manually.
        //speedLimit = PlayerPrefs.GetInt("SpeedLimit");
    }


    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        UpdateTbar(frontWheelModel, Tbar);
        SpeedLimit();
    }
    private void Update()
    {
        //UpdateTbar(frontWheelModel, Tbar);
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        //isBraking = Input.GetKeyDown(KeyCode.Space);

        // space key turn brake on and off
        if (Input.GetKeyDown("space"))
        {
            isBraking = !isBraking;
        }
    }

    private void HandleMotor()
    {

        // front wheel drive, and front wheel brake
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbrakeForce = isBraking ? brakeForce : 0f;
        if (isBraking)
        {
            ApplyBraking();
        }
        else
        {
            ReleaseBrake();
        }
    }

    private void ReleaseBrake()
    {
        frontRightWheelCollider.brakeTorque = 0f;
        frontLeftWheelCollider.brakeTorque = 0f;
        //rearLeftWheelCollider.brakeTorque = 0f;
        //rearRightWheelCollider.brakeTorque = 0f;
    }

    private void ApplyBraking()
    {
        frontRightWheelCollider.brakeTorque = currentbrakeForce;
        frontLeftWheelCollider.brakeTorque = currentbrakeForce;
        //rearLeftWheelCollider.brakeTorque = currentbrakeForce;
        //rearRightWheelCollider.brakeTorque = currentbrakeForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
        //Tbar.transform.Rotate(0f, currentSteerAngle*0.03f, 0f, Space.Self);
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontWheelModel);
        UpdateSingleWheel(rearLeftWheelCollider, rearWheelModel);

        
        //UpdateSingleWheel(frontLeftWheelCollider, frontWheelSpokeModel);
        //UpdateSingleWheel(rearLeftWheelCollider, rearWheelSpokeModel);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        //wheelTransform.position = pos;
    }

    private void UpdateTbar(Transform frontWheelModel, Transform Tbar)
    {
        //Vector3 pos;
        //Quaternion rot;
        //wheelCollider.GetWorldPose(out pos, out rot);
        //Tbar.rotation = Quaternion.Euler(transform.rotation.x, horizontalInput*maxSteerAngle, transform.rotation.z);
        // Make a new Quaternion rotation according to hit.normal, this make the new image in next step attach to hit point and parallel
        //Quaternion rot = Quaternion.FromToRotation((new Vector3(0, 1, 0)), frontWheelModel.up);
        //Tbar.rotation = Quaternion.Euler(transform.rotation.x, -rot.y, transform.rotation.z);
        //Tbar.Rotate(0, frontWheelModel.eulerAngles.y, 0, Space.Self);

        //Vector3 relativePos = frontWheelModel.position - Tbar.position;
        //Quaternion rotation = Quaternion.LookRotation(-relativePos, Vector3.forward);      
        //Tbar.rotation = Quaternion.Slerp(Tbar.rotation, rotation, Time.deltaTime * 2.0f);

        // Let T bar follow front wheels y local rotation
        Quaternion rot = new Quaternion();
        rot.Set(Tbar.transform.localRotation.x, frontWheelModel.localRotation.y, Tbar.transform.localRotation.z, 1);
        Tbar.localRotation = rot;
    }

    private void SpeedLimit()
    {
        currentSpeed = Convert.ToInt32(rb.velocity.magnitude * 2.237);
        if (currentSpeed >= speedLimit)
        {
            frontLeftWheelCollider.motorTorque = 0f;
            frontRightWheelCollider.motorTorque = 0f;
        }


    }

}
