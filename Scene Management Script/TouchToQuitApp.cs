using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script is to quit the current scene when user touch the attached object trigger
/// or 
/// UI use the ClickQuitAppButton()
/// or 
/// press the key
/// </summary>

public class TouchToQuitApp : MonoBehaviour
{
    // for Quiting the application during game, press Esc key
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
         // save any game data here
         #if UNITY_EDITOR
         // Application.Quit() does not work in the editor so
         // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
         UnityEditor.EditorApplication.isPlaying = false;
         #else
            Application.Quit();
         #endif
        }

    }


    // for quiting the application using UI button
    public void ClickQuitAppButton()
    {
        // save any game data here
        #if UNITY_EDITOR
         // Application.Quit() does not work in the editor so
         // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
         UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    // for quiting the application using trigger

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // save any game data here
        #if UNITY_EDITOR
         // Application.Quit() does not work in the editor so
         // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
         UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
        }
    }


}
