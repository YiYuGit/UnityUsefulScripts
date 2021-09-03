using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a simple player controller to move player horizontally.
/// </summary>

public class PlayerController : MonoBehaviour
{

    public float horizontalInput;
    public float speed = 20f;
    public float xBoundary = 15f;

    public GameObject projectilePrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        // Keep player in boundary
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xBoundary, xBoundary), 0.0f, 0.0f);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        }
    }

}
