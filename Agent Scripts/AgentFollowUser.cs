using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentFollowUser : MonoBehaviour
{
    /// <summary>
    /// Attach this script to agent. On start, all agents with this script will wait a random range time and then follow the player(user)
    /// 
    /// </summary>
    /// 

    // Put user here
    public GameObject user;

    Animator animator;

    NavMeshAgent myNavMeshAgent;

    void Start()
    {
        animator = GetComponent<Animator>();

        myNavMeshAgent = GetComponent<NavMeshAgent>();

        StartCoroutine(RandomStartTime());

    }


    IEnumerator RandomStartTime()
    {
        // a random time between 0 and 1 second to make each agent walk different
        float time = Random.Range(0.0f, 1.0f);

        //yield on a new YieldInstruction that waits for random seconds.
        yield return new WaitForSeconds(time);

        //After waiting, the animator IsWalking become true.
        animator.SetBool("IsWalking", true);

    }


    // Update is called once per frame
    void Update()
    {
        // Update user position to the agent
        myNavMeshAgent.SetDestination(user.transform.position);
    }
}
