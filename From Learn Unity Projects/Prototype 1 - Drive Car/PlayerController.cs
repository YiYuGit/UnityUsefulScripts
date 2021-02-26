using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float speed = 10.0f;

    private float turnSpeed = 25.0f;

    private float horizontalInput;

    private float forwardInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get user input
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // Move the vehicle forward
        //transform.Translate(0, 0, 1);

        // Moving 
        transform.Translate(Vector3.forward * (Time.deltaTime * speed * forwardInput));
        // Turning
        // When moving forward, the rotate is positive, when moving backward, the rotate is negative, so the vehicle act like real car
        if (forwardInput > 0)
        {
            transform.Rotate(Vector3.up * (Time.deltaTime * turnSpeed * horizontalInput));
        }
        else
        {
            transform.Rotate(Vector3.up * ( - Time.deltaTime * turnSpeed * horizontalInput));
        }
        

    }
}
