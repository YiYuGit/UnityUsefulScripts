using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is to attach to an object with trigger, and linked to an object like an agent with animator
/// On trigger enter or the condition is met, the script will change parameters in animator to make changes
/// In this example, the target will be set active false on start, when trigger is hit by player, the target will be set active and a 'move' bool will be set true
/// The corresponding action will take place.
/// </summary>

public class AccessAnimator : MonoBehaviour
{
    // The target gameobject ( object with Animator)
    public GameObject target;

    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = target.GetComponent<Animator>();
        target.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
            target.SetActive(true);
            animator.SetBool("move", true);
            //Destroy(target, 3);
        }
    }

}
