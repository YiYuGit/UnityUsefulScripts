using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// This script use points positions to create polygon procedural mesh.
/// Attach this to an empty object and set MeshFilter and Meshrender. If need using the mesh as mesh collider, it can be added later, and remember to add Rigidboy, 
/// select no gravity and is kinematic.
/// The first point is the center of the polygon, and script will draw triangles around it.
/// </summary>

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class PolygonMesh : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    // Getting the transform of points objects, the first point is the center points.
    // If the polygon shape is very complicated, may cut it into several parts, then use this script for each part.
    [Header("Drop points here, first is center point")]
    public Transform[] points;

    void Start()
    {
        //Make mesh
        mesh = new Mesh();
        
        // Give it a name based on the object name
        mesh.name = this.name + " Mesh";
        
        // Set the mesh to the object mesh filter
        GetComponent<MeshFilter>().mesh = mesh;
        
        // Creat shape base on the points. 
        CreateShape();

        // Update the mesh
        UpdateMesh();

    }


    void CreateShape()
    {
        // Get positions from points list
        vertices = points.Select(x=>x.position).ToArray();

        // Setting the size of arrary based on the length of the points arrary length.
        // for n points, there are ( n-1 ) triangles, so the size of the array is 3*(n-1). 
        // ( This is becasue, the first point in the "points" is used a the center of the polygon, and all triangles were arranged around it.
        triangles = new int[3 * (points.Length - 1)];

        // Writing triangles
        // This is the triangles except the last one.
        for (int i = 1; i < points.Length - 1; i++)
        {
            int j = 3 * i;
            triangles[j - 3] = 0;
            triangles[j - 2] = i;
            triangles[j - 1] = i + 1;
        }

        // This is the last trianglem, it connect from center point, to last point, then the first point
        triangles[3 * (points.Length - 1) - 3] = 0;
        triangles[3 * (points.Length - 1) - 2] = (points.Length - 1);
        triangles[3 * (points.Length - 1) - 1] = 1;

    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
