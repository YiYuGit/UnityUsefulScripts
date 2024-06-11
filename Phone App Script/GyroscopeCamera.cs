

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Get info from 
/// https://gamedev.stackexchange.com/questions/174107/unity-gyroscope-orientation-attitude-wrong
/// This script is for 360 photo view phone app. 
/// Maybe also useful for 360 video view phone app. 
/// </summary>

public class GyroscopeCamera : MonoBehaviour
{
    private Gyroscope phoneGyro;
    private Quaternion correctionQuaternion;


    // Start is called before the first frame update
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Input.gyro.enabled = true;

        phoneGyro = Input.gyro;
        phoneGyro.enabled = true;
        correctionQuaternion = Quaternion.Euler(90f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        GyroModifyCamera();
    }


    // The Gyroscope is right-handed.  Unity is left handed.
    // Make the necessary change to the camera.
    private void GyroModifyCamera()
    {
        Quaternion gyroQuaternion = GyroToUnity(Input.gyro.attitude);
        // rotate coordinate system 90 degrees. Correction Quaternion has to come first
        Quaternion calculatedRotation = correctionQuaternion * gyroQuaternion;
        transform.rotation = calculatedRotation;
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }


    public void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }

}
