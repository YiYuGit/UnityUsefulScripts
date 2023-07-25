using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is similar to the "TakeScreenShot" script. 
/// Attach this to an empty gameobject to make a screenshot camera
/// drag and drop main camera into the slot and define the desired resolution
/// If you want to include UI elements in the screen shot, you can change the UI canvas Render mode to 
/// Screen Space - Camera
/// 
/// This script can toggle another canvas (contains a full screen while image) on and off during the screen shot so the user can get a feedback,
/// like a flash of white screen.
/// 
/// Suggest putting all UI element that you want to include in the screenshot in one canvas and,
/// put other UI elements in another canvas, the first canvas change to Screen Space - Camera, the second canvas should be screen space - Overlay
/// 
/// </summary>
public class ScreenShotWithUI : MonoBehaviour
{

    [Header("Drop main camera here")]
    [Header("Press 'k' to take screen shot")]
    public Camera cam;

    [Header("Define resolution")]
    public int resolutionWidth = 1920;
    public int resolutionHeight = 1080;

    [Header("Drop the non-capturing canvas here")]
    public GameObject canvas;

    //This can also link to the WriteTxt script, when taking screen shot, also record a txt file containing the heading degrees
    [Header("Drop the write txt log here")]
    public WriteTxt writeTxt;


    //private bool takeHiResShot = false;


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
        //takeHiResShot = true;

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

        //Write corresponding txt file
        writeTxt.CreateTxtFile();

        Debug.Log(string.Format("Took screenshot to: {0}", filename));

        //takeHiResShot = false;
    }


    // Depending the purpose, the active sequence can be reversed, for example
    // only turn the UI on for the screen shot and then turn off
    // or turn off the UI so it do not show up in the screen shot 
    // or just turn it on and off right before or after the screenshot to give use some feedback
    public IEnumerator TakeScreenShot()
    {
        // Wait till the last possible moment before screen rendering to hide the UI
        yield return null;
        canvas.SetActive(true);

        // Wait for screen rendering to complete
        yield return new WaitForEndOfFrame();

        // wait time
        yield return new WaitForSeconds(0.1f);

        // Turn off canvas
        canvas.SetActive(false);

        // Take screenshot
        TakeHiResShot();


    }


        void LateUpdate()
    {
        if(Input.GetKeyDown("k"))
        {
            StartCoroutine(TakeScreenShot());
        }

    }

}
