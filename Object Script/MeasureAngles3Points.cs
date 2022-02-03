using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a simple tool that measure the angle between two vectors(formed between 3 points)
/// It use OnDrawGizmos() to visualze the two vector lines in Editor and show the angles in Console
/// </summary>

public class MeasureAngles3Points : MonoBehaviour
{
    // p0 is the center point
    public Transform p0;

    // p1 is the left point
    public Transform p1;

    // p2 is the right point
    public Transform p2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir1 = p1.position - p0.position;
        Vector3 dir2 = p2.position - p0.position;

        float angle = Vector3.Angle(dir1, dir2);

        Debug.Log("The angle is " + (int)angle);


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(p1.position, p0.position);
        Gizmos.DrawLine(p2.position, p0.position);

    }




}
