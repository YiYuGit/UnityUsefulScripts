using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script rotoate the camera view around the player using the horizontal input.
/// </summary>
public class RotateCameraX : MonoBehaviour
{
    private float speed = 200;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontalInput * speed * Time.deltaTime);

        transform.position = player.transform.position; // Move focal point with player

    }
}
