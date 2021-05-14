using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a ball player's script, attach to an sphere object with collider and rigidbody
/// The movement direction is determined by the focal point's forward direction
/// The powerup is a status that ball player can push enemy at a high speed
/// The powerup indicator is another object the indicate the status of powerup
/// </summary>

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    private GameObject focalPoint;

    private Rigidbody playerRb;

    public bool hasPowerup = false;

    private float powerupStrength = 15.0f;

    public GameObject powerupIndicator;

    // Start is called before the first frame update
    void Start()
    {
        // Get rigidbody
        playerRb = GetComponent<Rigidbody>();

        // Find the focal point by name
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        // Update control the movement by add force
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);

        // Move the indicator with player
        powerupIndicator.transform.position = transform.position;
        // Give indicator some self rotation just for visual effect
        powerupIndicator.transform.Rotate(Vector3.up * 60 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // What to do when hit a powerup object
       if(other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());

            powerupIndicator.gameObject.SetActive(true);
            
        }

    }

   
    private void OnCollisionEnter(Collision collision)
    {
        // If hittting an enemy, what will happen
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            //Debug.Log("got it");

            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        // The powerup have time limit, set here
        yield return new WaitForSeconds(10);
        hasPowerup = false;

        powerupIndicator.gameObject.SetActive(false);
    }
}
