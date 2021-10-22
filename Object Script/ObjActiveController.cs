using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script attach to an empty object as the controller, drop all controlled objects in to objs in the desired sequence.
/// Set the time interval in waitTime.
/// Once started, the controller will switch between the objects, set one object active at a time, loop through all objects in the list.
/// </summary>


public class ObjActiveController : MonoBehaviour
{

    // Put the wanted objects here
    public GameObject[] objs;

    // Time interval
    public float waitTime = 3;

    // Start with the first target point
    private int current = 0;

    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        // Turn off all objects on start
        foreach( GameObject obj in objs)
        {
            obj.SetActive(false);
        }

        // Start function WaitAndPrint as a coroutine.

        coroutine = WaitAndChange(2.0f);
        StartCoroutine(coroutine);

    }

    private IEnumerator WaitAndChange(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            ChangeObj();
        }
    }

    void ChangeObj()
    {
        foreach (GameObject obj in objs)
        {
            obj.SetActive(false);
        }

        objs[current].SetActive(true);

        if (current < objs.Length)
        {
            current++;
        }

        if (current >= objs.Length)
        {
            current = 0;
        }
    }
}
