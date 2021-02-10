using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeScreenShot : MonoBehaviour
{
    /// <summary>
    /// Attach this to an empty gameobject to make a screenshot camera
    /// drag and drop main camera into the slot and define the desired resolution
    /// If you want to include UI elements in the screen shot, you can change the UI canvas Render mode to 
    /// Screen Space - Camera
    /// 
    /// </summary>


    [Header("Drop main camera here")]
    [Header("Press 'k' to take screen shot")]
   
    public Camera cam;

    [Header("Define resolution")]
    public int resolutionWidth = 1920;
    public int resolutionHeight = 1080;

   
    private bool takeHiResShot = false;


    public static string ScreenShotName(int width, int height)
    {
        // Define the file path and file name 
        return string.Format("{0}/capture/screenshot_{1}x{2}_{3}.png",
                             Application.dataPath,
                             width, height,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    public void TakeHiResShot()
    {
        takeHiResShot = true;
    }

    void LateUpdate()
    {
        takeHiResShot |= Input.GetKeyDown("k");
        if (takeHiResShot)
        //if (Input.GetKeyDown("k"))   
        // If used alone, the " TakeHiResShot() part can be replaced with the line above
        {
            RenderTexture rt = new RenderTexture(resolutionWidth, resolutionHeight, 24);
            cam.targetTexture = rt;
            Texture2D screenShot = new Texture2D(resolutionWidth, resolutionHeight, TextureFormat.RGB24, false);
            cam.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resolutionWidth, resolutionHeight), 0, 0);
            cam.targetTexture = null;
            RenderTexture.active = null; // JC: added to avoid errors
            Destroy(rt);
            byte[] bytes = screenShot.EncodeToPNG();
            string filename = ScreenShotName(resolutionWidth, resolutionHeight);
            System.IO.File.WriteAllBytes(filename, bytes);
            Debug.Log(string.Format("Took screenshot to: {0}", filename));
            takeHiResShot = false;
        }
    }
}