using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;

public class LaunchExternalExe : MonoBehaviour
{
    // This script launch the external exe
    // Path of the external exe, using notepad as example
    
    [Header("Press key 'p' to launch the Exe ")]
    [Header("Exe Path")]
    public string path = "C:\\WINDOWS\\system32\\notepad.exe";



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            try
            {
                System.Diagnostics.Process.Start(path); 
            }
            catch (Exception error)
            {
                print(error);
            }
        }
    }
}
