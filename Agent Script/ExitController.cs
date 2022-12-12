using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Control the active status of scripts on agent.
/// When pressing "q", the agent will change their status from follow path to go to exit
/// </summary>
public class ExitController : MonoBehaviour
{

    //public GameObject cube;
    public GameObject[] cubes;

    void Start()
    {
        cubes = GameObject.FindGameObjectsWithTag("people");
        foreach (GameObject cube in cubes)
        {
            cube.gameObject.GetComponent<AgentFollowPath>().enabled = true;
            cube.gameObject.GetComponent<AgentGoToExit>().enabled = false;
        }

    }
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {

            foreach (GameObject cube in cubes)
            {
                cube.gameObject.GetComponent<AgentFollowPath>().enabled = false;
                cube.gameObject.GetComponent<AgentGoToExit>().enabled = true;
            }

        }
    }
}
