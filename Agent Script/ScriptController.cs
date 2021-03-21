using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptController : MonoBehaviour
{

    /// <summary>
    /// This script is used for switching the acive status of different scripts on the same object.
    /// On Awake, this script find all gameobjects with tag"agent", then turn on all their script1, turn off script2
    /// during the game, if user press key 'q', the scirpt will turn on script2 and turn off script1
    /// keep pressing 'q' can switch back and forth
    /// </summary>


    public GameObject[] agents;


    void Awake()
    {
        agents = GameObject.FindGameObjectsWithTag("agent");
        foreach (GameObject agent in agents)
        {
            //cube.gameObject.GetComponent<AgentFollowPath>().enabled = true;
            //cube.gameObject.GetComponent<AgentGoToExit>().enabled = false;

           AgentFollowPath script1;
           AgentGoToExit script2;

           script1 = agent.gameObject.GetComponent<AgentFollowPath>();
           script2 = agent.gameObject.GetComponent<AgentGoToExit>();

            script1.enabled = true;
            script2.enabled = false;

        }

    }
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {

            foreach (GameObject agent in agents)
            {
                //cube.gameObject.GetComponent<AgentGoToExit>().enabled = true;
                //cube.gameObject.GetComponent<AgentFollowPath>().enabled = false;

                AgentFollowPath script1;
                AgentGoToExit script2;

                script1 = agent.gameObject.GetComponent<AgentFollowPath>();
                script2 = agent.gameObject.GetComponent<AgentGoToExit>();

                script1.enabled = !script1.enabled;
                script2.enabled = !script2.enabled;

            }

        }
    }
}
