using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This makes the background image go back to initial position to make seamless background
/// For the Jump Fence game
/// </summary>


public class RepeatBackground : MonoBehaviour
{

    private Vector3 startPos;

    private float repeatWidth;

    // Start is called before the first frame update
    void Start()
    {
        // On Start, get the initial position
        startPos = transform.position;

        // The repeatWith is calculated by using half of BoxCollier.size.x
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
