using UnityEngine;
using System.Collections;

/// <summary>
/// Game class
/// </summary>

public class Game  MonoBehaviour
{
    void Start()
    {
        Player myPlayer = new Player();

        // Properties can be used just like variables
        myPlayer.Experience = 5;
        int x = myPlayer.Experience;
    }
}