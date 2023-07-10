using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

/// <summary>
///  This script is attached to icon object in Unity,(need collider)
///  set the video name and start, end time
///  On click to the icon obejct. 
///  The VLC player will be called to play the corresponding video clip
///  the icon object will be turned off and back on for a cool down time
/// </summary>

public class OnMouseDownPlay : MonoBehaviour
{

    [SerializeField]

    public string startTime = string.Empty;
    public string endTime = string.Empty;
    //public string videoName = string.Empty;
    //[SerializeField]
    private string videoName = "Y:\\VideoLAN\\VLC\\test1.mp4";

    public MeshRenderer rend;

    private void Start()
    {
        rend = GetComponent<MeshRenderer>();
    }
    void OnMouseDown()
    {

        // Turn the objet off for  seconds

        StartCoroutine(CDtime());


        try
            {
                Process myProcess = new Process();
                myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Maximized; // or Minimized, Hidden

                //myProcess.StartInfo.FileName = "C:\\Windows\\system32\\cmd.exe";

                myProcess.StartInfo.FileName = "Y:\\VideoLAN\\VLC\\vlc.exe";

                myProcess.StartInfo.Arguments = " --start-time=" + startTime + " --stop-time=" + endTime + " " + videoName;
                // Video is stored in the Unity Project Root folder

                myProcess.Start();


            }
            catch (Exception error)
            {
                print(error);
            }
        


    }

    IEnumerator CDtime()
    {
        rend.enabled = false;

        yield return new WaitForSeconds(.5f);

        rend.enabled = true;

        yield return new WaitForSeconds(.5f);

        rend.enabled = false;

        yield return new WaitForSeconds(.5f);

        rend.enabled =true;
    }

}

