using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will loop the Debug.Log part of IEnumerator
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
