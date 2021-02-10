using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentFollowPath : MonoBehaviour
{
    /// <summary>
    /// Attach this script to an agent, the agent will follow the agentpath, walk in a loop
    /// </summary>
    /// 
    // Drag and drop agentpaht here
    public AgentPath path;

    // Approach distance is the when agent get to the waypoint close enough, then swtich to next point
    public float approachDistance = 1.5f;

    // The agent needs to be equiped with animator and navmeshagent
    Animator animator;

    NavMeshAgent myNavMeshAgent;

    private List<Vector3> nodes = new List<Vector3>();
    private int currentNode = 0;

    void Start()
    {
        // Get the target from the agent path
        nodes = path.target;

        // Get the animator from agent
        animator = GetComponent<Animator>();

        // Get the navmeshagent from agent
        myNavMeshAgent = GetComponent<NavMeshAgent>();

        // Coroutine to random time and speed
        StartCoroutine(RandomStartTime());

        //Start the coroutine we define below named ExampleCoroutine.

        //animator.speed = 0.2f;
    }


    IEnumerator RandomStartTime()
    {
        // When genterating different agents, this random time and speed will make them act more naturally
        float time = Random.Range(0.0f, 1.0f);
        float speed = Random.Range(1.0f, 1.5f);

        //yield on a new YieldInstruction that waits for random seconds.
        yield return new WaitForSeconds(time);

        animator.SetBool("IsWalking", true);
        myNavMeshAgent.speed = speed;
    }


    // Update is called once per frame
    void Update()
    {
        // check current waypoint distance and set the destination point
        CheckWaypointDistance();
        myNavMeshAgent.SetDestination(nodes[currentNode]);
    }

    private void CheckWaypointDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode]) < approachDistance)
        {

            if (currentNode == nodes.Count - 1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if ((currentNode <= nodes.Count - 1)&& (this.gameObject.GetComponent<AgentFollowPath>().enabled == true))
        {
            Gizmos.DrawLine(transform.position, nodes[currentNode]);
        }
    }
}
