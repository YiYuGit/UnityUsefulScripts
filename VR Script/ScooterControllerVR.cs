using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class ScooterControllerVR : MonoBehaviour
{
    private Rigidbody rb;
    public Transform centerOfMass;

    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    public bool isBreaking;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;

    public Transform frontWheelModel;
    public Transform rearWheelModel;
    //public Transform frontWheelSpokeModel;
    //public Transform rearWheelSpokeModel;
    public Transform Tbar;


    //private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device ControllerRight
    {
        get { return SteamVR_Controller.Input(3); }
    }

    private SteamVR_Controller.Device ControllerLeft
    {
        get { return SteamVR_Controller.Input(4); }
    }

    /*
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    */


    private void Start()
    {
       rb = GetComponent<Rigidbody>();
       rb.centerOfMass = centerOfMass.localPosition;
    }


    private void FixedUpdate()
    {
        if (ControllerRight.GetHairTriggerDown())
        {
            verticalInput = 1f;
        }

        if (ControllerRight.GetHairTriggerUp())
        {
            verticalInput = 0f;
        }

        if (ControllerLeft.GetHairTriggerDown())
        {
            isBreaking = true;
        }

        if (ControllerLeft.GetHairTriggerUp())
        {
            isBreaking = false;
        }



        //GetInput();
        VRSteering();
        HandleMotor();
        HandleSteering();
        UpdateWheels();

    }
    private void Update()
    {
        UpdateTbar(frontWheelModel, Tbar);
    }

    private void VRSteering()
    {
        float max = -0.78f;
        float min = -0.95f;

        float input = ControllerRight.transform.pos.x;
        float average = (min + max) / 2;
        float range = (max - min) / 2;
        float normalized_x = (input - average) / range;
        float output = 2 * normalized_x - 1;


        horizontalInput = output;
        Debug.Log(output);

    }


    private void GetInput()
    {
        //horizontalInput = Input.GetAxis(HORIZONTAL);
        //Debug.Log(horizontalInput);
        //verticalInput = Input.GetAxis(VERTICAL);
        //isBreaking = Input.GetKeyDown(KeyCode.Space);
        
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        if (isBreaking)
        {
            ApplyBreaking();
        }
        else
        {
           ReleaseBreaking();
        }
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        //rearLeftWheelCollider.brakeTorque = currentbreakForce;
        //rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void ReleaseBreaking()
    {
        frontRightWheelCollider.brakeTorque = 0f;
        frontLeftWheelCollider.brakeTorque = 0f;
        //rearLeftWheelCollider.brakeTorque = currentbreakForce;
        //rearRightWheelCollider.brakeTorque = currentbreakForce;
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
        Quaternion rot = new Quaternion();
        rot.Set(Tbar.transform.localRotation.x, frontWheelModel.localRotation.y, Tbar.transform.localRotation.z, 1);
        Tbar.localRotation = rot;
    }
}
