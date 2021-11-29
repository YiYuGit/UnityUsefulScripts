using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

/// <summary>
/// This script is similar to the data recording script. 
/// Attach this to gameobject, the script will record gameobject movement infomation(time and position)
/// When the object hit other trigger, it will compare their tag(example: door, wall), and record the hit info
/// </summary>



public class TouchLogInfo : MonoBehaviour
{
    public void WriteToFile(string message)
    {
        string path = @"c:\temp\MyTest.txt";
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

    private void FixedUpdate()
    {
        Transform pos = this.transform;
        WriteToFile("\n" + DateTime.Now + "," + pos.position);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            Debug.Log("Hit Wall");
            WriteToFile("\n"+ DateTime.Now +"," + " Hit Wall");
        }

        if (other.gameObject.CompareTag("door"))
        {
            Debug.Log("Hit Door");
            WriteToFile("\n" + DateTime.Now + "," + " Hit Door");
        }
    }

}



