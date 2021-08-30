using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// In the Unity Learn2D project, when On Trigger Enter detect Player, set the explosion true and play the explo animation, then destroy the explosion obejct in 3 sec.
/// </summary>


public class ExplosionAnimation : MonoBehaviour
{

    public GameObject explosion;
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = explosion.GetComponent<Animator>();
        explosion.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            explosion.SetActive(true);
            animator.SetBool("explo", true);
            Destroy(explosion, 3);
        }
    }

}
