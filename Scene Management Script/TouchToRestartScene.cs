using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script is to restart the current scene when user touch the attached object trigger
/// or 
/// UI use the ClickRestartButton()
/// or 
/// pressing the key "l"
/// </summary>


public class TouchToRestartScene : MonoBehaviour
{
    // for restarting the scene during game, press L key
    void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }


    public void ClickRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


}
