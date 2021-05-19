using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Attach this script to all Easy/Medium/Hard level selection button
/// </summary>

public class DifficultyButton : MonoBehaviour
{
    // Button, will be initialized at start
    private Button button;

    // On start, find the GameManager and get the component to access its methods
    private GameManager gameManager;

    // Set the value in the inspector, for different difficulty value, the gameManager.StartGame(difficulty) take this int to adjust spawn time.
    public int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        // Get Button component
        button = GetComponent<Button>();

        // Get gameManager script on the Game Manager Object by the name
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // Create "onClick.AddListener", when clicked, the SetDifficulty() will execute.(Another method for this is to set the On Click in the inspector button property.)
        button.onClick.AddListener(SetDifficulty);
    }

    void SetDifficulty()
    {
        gameManager.StartGame(difficulty);
    }
}
