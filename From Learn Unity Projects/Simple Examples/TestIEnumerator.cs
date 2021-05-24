using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script test the IEnumerator function,
/// StartCoroutine() inside a StartCoroutine()
/// It will loop Debug.Log every 5 seconds
/// </summary>


public class TestIEnumerator : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(MyCoroutine());
    }

    private IEnumerator MyCoroutine()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Test");
        StartCoroutine(MyCoroutine());
    }
}
