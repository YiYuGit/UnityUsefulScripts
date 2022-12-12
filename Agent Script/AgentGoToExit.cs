using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// On start, this script will get a random exit number from exitlocaitons, and the navmesh agent will set that location as destination.
/// When reaching the destination, isWalking change to false, the agen stop walking
/// </summary>

public class AgentGoToExit : MonoBehaviour
{
    public ExitLocations exits;
    private Vector3 exit;
    private int exitNumber;

    Animator animator;

    NavMeshAgent myNavMeshAgent;
    void Start()
    {
        animator = GetComponent<Animator>();
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        GetRandomExit();

    }

    private void GetRandomExit()
    {
        exitNumber = Random.Range(0, exits.exitLocations.Count);
        exit = exits.exitLocations[exitNumber];
        myNavMeshAgent.speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        myNavMeshAgent.SetDestination(exit);
        CheckIfReachExit();
    }

    private void CheckIfReachExit()
    {
        if (Vector3.Distance(transform.position, exit) < 1.5f)
        {

            animator.SetBool("IsWalking", false);
            myNavMeshAgent.speed = 0f;

        }

    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if ((this.gameObject.GetComponent<AgentGoToExit>().enabled == true))
        {
            Gizmos.DrawLine(transform.position, exit);
        }

    }

}