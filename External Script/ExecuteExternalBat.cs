using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;

public class ExecuteExternalBat : MonoBehaviour
{

    // Path of the external .bat file
    [Header("Batch file path")]
    [Space]
    [Header("Press key 'b' to execute the bat file ")]
    public string path = "C:\\Users\\User\\Desktop\\bat\\example.bat";


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("b"))
        {
            try
            {
                Process myProcess = new Process();
                myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized; // or Minimized, Hidden

                myProcess.StartInfo.FileName = "C:\\Windows\\system32\\cmd.exe";

                myProcess.StartInfo.Arguments = "/k" + path;

                myProcess.Start();

            }
            catch (Exception error)
            {
                print(error);
            }
        }
        

           



    }
}
