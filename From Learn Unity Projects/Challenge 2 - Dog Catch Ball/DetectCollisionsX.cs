using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisionsX : MonoBehaviour
{
    //This script destroy object when triggered
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
