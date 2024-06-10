using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// In use. 
/// Restart current scene in the F1 help page OK button.
/// </summary>

public class RestartScene : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {

    }

    public void RestartCurrentScene()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Reload the current scene
        SceneManager.LoadScene(currentSceneIndex);
    }
}
