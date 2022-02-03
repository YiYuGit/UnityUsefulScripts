using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// This script is used to find the max and min of X and Z values among a set of point positions.
/// And using the OnDrawGizmos() to draw the rectangular boundary of the area.
/// Y value can also be find using the same method. 
/// This can be used to draw boundary of a play area.
/// This can be used alone or be incorporated into other script, used as one method.
/// Can add a public bool to control the OnDrawGizmos() status
/// </summary>

public class FindMaxAndMinInArray : MonoBehaviour
{
    // Array to store x and z values from transform.position
    float[] positionsX;
    float[] positionsZ;

    // Place to hold input points
    [Header("Drop points here, to find max and min x,z values")]
    public Transform[] points;

    // Results and they can be access from other scripts
    public static float xMax ;
    public static float xMin ;

    public static float zMax ;
    public static float zMin ;

    // Start is called before the first frame update
    void Start()
    {
        //Find value on start
        FindMaxAndMin();
    }

    void FindMaxAndMin()
    {
        // Read x and z value from transform.position and put into array.
        positionsX = points.Select(x => x.position.x).ToArray();
        positionsZ = points.Select(x => x.position.z).ToArray();

        // Get the max and min 
        xMax = Mathf.Max(positionsX);
        xMin = Mathf.Min(positionsX);

        zMax = Mathf.Max(positionsZ);
        zMin = Mathf.Min(positionsZ);

        //Debug.Log("The x max is " + Mathf.Max(positionsX));
        //Debug.Log("The x min is " + Mathf.Min(positionsX));

        //Debug.Log("The z max is " + Mathf.Max(positionsZ));
        //Debug.Log("The z min is " + Mathf.Min(positionsZ));
    }

    private void OnDrawGizmos()
    {
        // Find the value
        FindMaxAndMin();

        //Set color
        Gizmos.color = Color.red;

        // Draw lines
        Gizmos.DrawLine(new Vector3(xMax, 1, zMax), new Vector3(xMax, 1, zMin));
        Gizmos.DrawLine(new Vector3(xMax, 1, zMin), new Vector3(xMin, 1, zMin));
        Gizmos.DrawLine(new Vector3(xMin, 1, zMin), new Vector3(xMin, 1, zMax));
        Gizmos.DrawLine(new Vector3(xMin, 1, zMax), new Vector3(xMax, 1, zMax));

        // Draw corner spheres
        Gizmos.DrawSphere(new Vector3(xMax, 1, zMax), 0.5f);
        Gizmos.DrawSphere(new Vector3(xMax, 1, zMin), 0.5f);
        Gizmos.DrawSphere(new Vector3(xMin, 1, zMax), 0.5f);
        Gizmos.DrawSphere(new Vector3(xMin, 1, zMin), 0.5f);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
