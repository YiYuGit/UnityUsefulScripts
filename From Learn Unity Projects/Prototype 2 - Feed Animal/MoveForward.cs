using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Attach to something like a bullet, it will move forward at the public float speed.
///  Vector3.forward can be change to a specific vector.
///  This is a simple script without using rigidbody.
/// </summary>

public class MoveForward : MonoBehaviour
{

    public float speed = 50f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
