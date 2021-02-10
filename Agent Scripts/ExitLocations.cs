using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// This script attach to an empty object, then user can put actual or empty objects as children of the empty object
/// The children obejcts can use tag 'exit' to identify themselves
/// The ExitLocation() can use the children locations to generate location as exits location for agents.
/// The OnDrawGizmos() will execute ExitLocation() and draw the locaiton with sphere in editor.
/// </summary>


public class ExitLocations : MonoBehaviour
{
    //Read and store exitPoints transform in exitPoints
    private List<Transform> exitPoints = new List<Transform>();

    //Copy of exitpoints for public access
    public List<Vector3> exitLocations = new List<Vector3>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitLocation()
    {
        exitLocations.Clear();

        exitPoints = GetComponentsInChildren<Transform>().Where(r => r.tag == "exit").ToList();

        for (int i = 0; i < exitPoints.Count; i++)
        {
            Vector3 C = exitPoints[i].position;
            exitLocations.Add(C);
        }

    }

    void OnDrawGizmos()
    {

        ExitLocation();
        Gizmos.color = Color.red;
        foreach (Transform temp in exitPoints)
        {
            Vector3 pos = new Vector3(temp.position.x, temp.position.y, temp.position.z);
            Gizmos.DrawSphere(pos, 0.5f);
        }

    }

}
