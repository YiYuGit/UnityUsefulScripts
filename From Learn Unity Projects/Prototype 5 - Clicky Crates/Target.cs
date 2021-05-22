using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is attached to all target objects.
/// </summary>


public class Target : MonoBehaviour
{
    // The point value for the object, "Good"object is positive, "Bad" object is negative
    public int pointValue;

    // Assign a particle system for each type of target
    public ParticleSystem explosionParticle;

    // Each target has  rigidbody
    private Rigidbody targetRb;

    // The GameManager component of Game Manager object
    private GameManager gameManager;

    // The speed and ranges of the target when spawn
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;

    void Start()
    {

        // Get component and apply rigidbody movements
        targetRb = GetComponent<Rigidbody>();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPos();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }


    // Next three are randomizing the target
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }


    // OnMouseDown, destroy target object and apply update score
    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            gameManager.UpdateScore(pointValue);

            Instantiate(explosionParticle, transform.position, transform.rotation);
        }

    }

    // Whne colliding the buttom collider trigger, destroy object, but if it's "Good" target,that means player missed good target, the game is over

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (gameObject.CompareTag("Good"))
        {
            gameManager.GameOver();
        }
    }
}
