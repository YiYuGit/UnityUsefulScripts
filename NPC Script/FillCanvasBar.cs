using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Partially come from https://www.youtube.com/watch?v=xz4E1OnWHuk
/// Attach canvas to gameobject as child, under the canvas put image as fill bar background, and under fill bar background put the corresponding fill image
/// Also use the 'CameraBillboard' script to rotate the canvas always towards the camera
/// This can be used as status bar, health bar...
/// </summary>

public class FillCanvasBar : MonoBehaviour
{
    // Put fill image here, choose image type to filled, horizontal, fill from left or right.
    public Image filledBar;

    // The fill percentage, will be a float between 0 to 1
    public float fill;

    // Find player on start, and calculate distance, the canvas will be activate/deactive based on distance
    public GameObject player;

    // Player approaching distance
    public float approachDistance = 5f;

    //Canvas object
    private Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        // Find Canvas in children
        canvas = GetComponentInChildren<Canvas>();

        // Find Player using tag
        if (player == null)
            player = GameObject.FindWithTag("Player");
   
        // Full fill on start
        fill = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        // Using the distane to determine the Canvas active status
        if (Vector3.Distance(player.transform.position, transform.position) < approachDistance)
        {
            canvas.gameObject.SetActive(true);
        }
        else
        {
            canvas.gameObject.SetActive(false);
        }
        
        //Am example that let fill change over time
        //fill -= Time.deltaTime * 0.05f;
        
        // fill amount equals to fill
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
    // for example, the step is 0.05, so there are 1/0.05 =20 steps in the fill bar
    public void Add()
    {
        fill += 0.05f;
    }

    public void Minus()
    {
        fill -= 0.05f;
    }

}
