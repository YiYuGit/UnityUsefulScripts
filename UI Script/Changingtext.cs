using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is attached to a moving gameobject, and on every frame, update the 3d position of the object to the UI text.
/// Drag and drop the Text to the "changing" slot
/// </summary>



public class Changingtext : MonoBehaviour
{
    // Canvas UI text
    public Text changing;

    // Temp string message store the text
    private string message;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        message = this.transform.position.ToString();
        changing.text = message;


    }
}
