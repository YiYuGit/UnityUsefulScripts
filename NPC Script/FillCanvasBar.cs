using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// From https://www.youtube.com/watch?v=xz4E1OnWHuk
/// 
/// Attach canvas to gameobject, under the canvas put image as fill bar background, and under fill bar background put the fill image
/// Also use the 'ameraBillboard' script to rotate the canvas always towards the camera
/// This can be used as status bar, health bar...
/// </summary>

public class FillCanvasBar : MonoBehaviour
{
    // Put fill image here, choose image type to filled, horizontal, fill from left or right.
    public Image filledBar;

    // The fill percentage, 0 to 1
    public float fill; 

    // Start is called before the first frame update
    void Start()
    {
        // Full fill on start
        fill = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //fill -= Time.deltaTime * 0.05f;
        filledBar.fillAmount = fill;

        // This part will limit fill always fall into 0 to 1
        if(fill > 1f)
        {
            fill = 1f;
        }

        if(fill < 0f)
        {
            fill = 0f;
        }    

    }

    // Add and minus function can be used when triggered or set as button
    // for example, the step is 0.05, so 1/0.05 =20 steps
    public void Add()
    {
        fill += 0.05f;
    }

    public void Minus()
    {
        fill -= 0.05f;
    }

}
