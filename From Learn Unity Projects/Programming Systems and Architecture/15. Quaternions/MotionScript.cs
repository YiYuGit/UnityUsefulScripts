using UnityEngine;
using System.Collections;

/// <summary>
/// Move object along the Horizontal axis, at the speed of 3
/// </summary>

public class MotionScript : MonoBehaviour
{
    public float speed = 3f;


    void Update()
    {
        transform.Translate(-Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0);
    }
}