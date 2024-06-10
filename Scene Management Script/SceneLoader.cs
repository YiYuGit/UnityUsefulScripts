using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// In use.
/// Used for loading the user take initial heading and loading back to the main scene.
/// </summary>

public class SceneLoader : MonoBehaviour
{
    // Specify the scene name or index you want to load
    public string sceneToLoad; // You can also use int for scene index if you prefer
    public int sceneIndexToLoad; // Specify scene index if using int

    // Update is called once per frame
    void Update()
    {
        /* 
         // Disable this when only using button
        // Check for a key press, for example the 'L' key
        if (Input.GetKeyDown(KeyCode.L))
        {
            // Load the specified scene by name or index
            if (!string.IsNullOrEmpty(sceneToLoad))
            {
                LoadSceneByName();
            }
            else if (sceneIndexToLoad >= 0)
            {
                LoadSceneByIndex();
            }
            else
            {
                Debug.LogError("Scene name or index is not specified.");
            }
        }
        */
    }

    public void LoadSceneByName()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadSceneByIndex()
    {
        // Check if the scene index is valid
        if (sceneIndexToLoad < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndexToLoad);
        }
        else
        {
            Debug.LogError("Invalid scene index specified.");
        }
    }
}
