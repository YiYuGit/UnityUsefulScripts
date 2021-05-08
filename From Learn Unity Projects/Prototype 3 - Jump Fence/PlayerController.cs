using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the player controller in the Unity learn Prototype 3, jumping fence game
/// Attah it to the player object, it controls the player movement, animation, particle system and sound
/// </summary>


public class PlayerController : MonoBehaviour
{
    // Jumping force
    public float jumpForce;
    
    // Rigidbody gravity modifier
    public float gravityModifier;

    // Character grounding status
    public bool isOnGround = true;
    
    // Game status
    public bool gameOver = false;

    // Particle system of gameover explosion
    public ParticleSystem explosionParticle;

    //  Particle system of character running dirt effect
    public ParticleSystem dirtParticle;

    // Jump sound
    public AudioClip jumpSound; 
    
    // Crash sound
    public AudioClip crashSound;

    // The audio player
    private AudioSource playerAudio;

    // Rigidbody
    private Rigidbody playerRb;

    // Player's animator
    private Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        // Get all componenet
        playerRb = GetComponent<Rigidbody>();

        playerAnim = GetComponent<Animator>();

        playerAudio = GetComponent<AudioSource>();

        // Change gravity by multiplying with the modifier number
        Physics.gravity *= gravityModifier;


    }

    // Update is called once per frame
    void Update()
    {
        // Jump is available when player is grounded and game not over
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            // Jump using addForce
            playerRb.AddForce(Vector3.up * jumpForce ,ForceMode.Impulse);
            isOnGround = false;

            // Play Jumping animation
            playerAnim.SetTrigger("Jump_trig");

            // Not playing dirt particle when in the air
            dirtParticle.Stop();

            // Play the jump sound
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // When collided with ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        // When run into obstacles
        else if (collision.gameObject.CompareTag("Obstacle"))
        { 
            gameOver = true;
            Debug.Log("Game Over!");

            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);

            explosionParticle.Play();
            dirtParticle.Stop();

            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
  
    }
}
