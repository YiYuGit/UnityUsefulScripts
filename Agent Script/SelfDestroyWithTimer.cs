using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach this script to any game object, and set the self destroy time
/// The object will be destroyed at the set time
/// And the remaining time will be displayed
/// 
/// </summary>
public class SelfDestroyWithTimer : MonoBehaviour
{
    // Set self destroy time
    public float destroyTime = 100f;

    [SerializeField]
    private bool timerIsRunning = false;
    [SerializeField]
    private float timeRemaining;

    // Start is called before the first frame update
    void Start()
    {
        // Starts the timer and destroy count down on start
        timerIsRunning = true;
        Destroy(gameObject, destroyTime);
        timeRemaining = destroyTime;

    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                //Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }
}
