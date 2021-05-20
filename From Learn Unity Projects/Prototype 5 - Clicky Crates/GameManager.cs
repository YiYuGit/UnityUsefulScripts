using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This is the Game Manager for the Clicky Crates Game
/// Managing the Start/Restart/Over... of the game, and Spawn Target, Update Score,
/// </summary>

public class GameManager : MonoBehaviour
{
    // Put target prefabs here
    public List<GameObject> targets;

    // Text
    public TextMeshProUGUI scoreText;

    // Text
    public TextMeshProUGUI gameOverText;

    // Button
    public Button restartButton;

    // Using empty gameobject to organize UI elements of title screen
    public GameObject titleScreen;

    // Game status
    public bool isGameActive;

    // Spawn Rate, later used to determine difficulty level by changing this value
    private float spawnRate = 1.0f;

    // Player's score
    private int score;

    // Start is called before the first frame update
    void Start()
    {

    }


    // Spawn the target while game is active, the rate in the wait for seconds is determined by the difficulty level
    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    // Update the score on UI
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    // When game over, change text, change bool, set restart button active
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);

        isGameActive = false;

        restartButton.gameObject.SetActive(true);

    }

    // When click restart game, load the scene again
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Get difficulty int from UI
    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);

        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);

        titleScreen.gameObject.SetActive(false);

        spawnRate /= difficulty;

    }
}
