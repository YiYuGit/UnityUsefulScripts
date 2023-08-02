using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to an empty game object or the canvas component (usually the Screen Space - Overlay) canvas
/// When F1 (or assign another key in the code) is pressed. 
/// The help text will come up.
/// Press the same button again, the help text will disappear.
/// 
/// </summary>

public class ToggleHelp : MonoBehaviour
{
    // Drop the help text here
    public GameObject helpText;

    // Start is called before the first frame update
    void Start()
    {
        helpText.SetActive(false);
    }

    // Make the toggle help text into a function that can be called by other script
    public void ToggleHelpText()
    {
        helpText.SetActive(!helpText.activeSelf);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ToggleHelpText();
        }
    }
}
