//========= Copyright 2018, HTC Corporation. All rights reserved. ===========
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Assertions;
using System.IO;
using System;

// Need to be used with Vive SR
// This script will record user eye focus locatoin at certain layers.

namespace ViveSR
{
    namespace anipal
    {
        namespace Eye
        {
            public class EyeFocusLocation : MonoBehaviour
            {
                public GameObject ball;
                public int LengthOfRay = 25;
                public float hitForce = 100f;

                private static EyeData eyeData = new EyeData();
                private bool eye_callback_registered = false;
                private void Start()
                {
                    WriteToFile("gaze-x" + "," + "gaze-y" + "," + "gaze-z" + "," + "head-x" + "," + "head-y" + "," + "head-z" + "hitObjectTag");

                    if (!SRanipal_Eye_Framework.Instance.EnableEye)
                    {
                        enabled = false;
                        return;
                    }
                    
                }


                public void WriteToFile(string message)
                {
                    string path = @"C:\Users\RISE\Desktop\unityOutput\EyeTrackingData.csv";
                    try
                    {
                        StreamWriter filewriter = new StreamWriter(path, true);
                        filewriter.Write(message);
                        filewriter.Close();
                    }
                    catch
                    {
                        Debug.LogError("cannot write to the file");
                    }

                }






                private void Update()
                {
                    if (SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.WORKING &&
                        SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.NOT_SUPPORT) return;

                    if (SRanipal_Eye_Framework.Instance.EnableEyeDataCallback == true && eye_callback_registered == false)
                    {
                        SRanipal_Eye.WrapperRegisterEyeDataCallback(Marshal.GetFunctionPointerForDelegate((SRanipal_Eye.CallbackBasic)EyeCallback));
                        eye_callback_registered = true;
                    }
                    else if (SRanipal_Eye_Framework.Instance.EnableEyeDataCallback == false && eye_callback_registered == true)
                    {
                        SRanipal_Eye.WrapperUnRegisterEyeDataCallback(Marshal.GetFunctionPointerForDelegate((SRanipal_Eye.CallbackBasic)EyeCallback));
                        eye_callback_registered = false;
                    }

                    Vector3 GazeOriginCombinedLocal, GazeDirectionCombinedLocal;

                    if (eye_callback_registered)
                    {
                        if (SRanipal_Eye.GetGazeRay(GazeIndex.COMBINE, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal, eyeData)) { }
                        else if (SRanipal_Eye.GetGazeRay(GazeIndex.LEFT, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal, eyeData)) { }
                        else if (SRanipal_Eye.GetGazeRay(GazeIndex.RIGHT, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal, eyeData)) { }
                        else return;
                    }
                    else
                    {
                        if (SRanipal_Eye.GetGazeRay(GazeIndex.COMBINE, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal)) { }
                        else if (SRanipal_Eye.GetGazeRay(GazeIndex.LEFT, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal)) { }
                        else if (SRanipal_Eye.GetGazeRay(GazeIndex.RIGHT, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal)) { }
                        else return;
                    }

                    Vector3 GazeDirectionCombined = Camera.main.transform.TransformDirection(GazeDirectionCombinedLocal);
                    RaycastHit hit;

                    if (Physics.Raycast(Camera.main.transform.position, GazeDirectionCombined, out hit, Mathf.Infinity,((1 << 9)|(1<<8))))
                    {
                        //Debug.DrawRay(Camera.main.transform.position, hit.point, Color.yellow);
                        Debug.Log("Did Hit");
                        Debug.Log(hit.point);
                        //Instantiate(ball);
                        //ball.transform.position = hit.point;

                        //Covnert hit point location to string and remove the brackets
                        string hitlocation = hit.point.ToString();
                        string camlocation = Camera.main.transform.position.ToString();
                        hitlocation = hitlocation.Substring(1, hitlocation.Length - 2);
                        camlocation = camlocation.Substring(1, camlocation.Length - 2);
                        WriteToFile("\n" + hitlocation + "," + camlocation + "," + hit.collider.gameObject.tag);


                        //WriteToFile("\n" + DateTime.Now +" "+ hit.point + " " + hit.collider.gameObject.name);


                        /*
                        if (hit.rigidbody != null)
                        {
                            hit.rigidbody.AddForce(GazeDirectionCombined * hitForce);
                        }
                        */



                    }



                    //GazeRayRenderer.SetPosition(0, Camera.main.transform.position - Camera.main.transform.up * 0.05f);
                    //GazeRayRenderer.SetPosition(1, Camera.main.transform.position + GazeDirectionCombined * LengthOfRay);
                }
                private void Release()
                {
                    if (eye_callback_registered == true)
                    {
                        SRanipal_Eye.WrapperUnRegisterEyeDataCallback(Marshal.GetFunctionPointerForDelegate((SRanipal_Eye.CallbackBasic)EyeCallback));
                        eye_callback_registered = false;
                    }
                }
                private static void EyeCallback(ref EyeData eye_data)
                {
                    eyeData = eye_data;
                }
            }
        }
    }
}
