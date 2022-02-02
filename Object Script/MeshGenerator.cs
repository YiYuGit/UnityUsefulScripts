using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a simple procedural mesh generator. That use several points to draw mesh.
/// "vertices" stores the point positions, and "triangles" stores which three points form one triangle
/// </summary>

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{
    // This control the on and off status of the Gizmo to help you adjust the shape of mesh
    public bool gizmoOnOff = true;

    //The mesh
    Mesh mesh;
    // The points and triangles
    Vector3[] vertices;
    int[] triangles;

    // For example, this use 4 points, minimum is 3.
    public Transform point1;
    public Transform point2;
    public Transform point3;
    public Transform point4;


    // Start is called before the first frame update
    void Start()
    {
        //new mesh
        mesh = new Mesh();

        // Rename the mesh
        mesh.name = this.name + " Mesh";

        // Assign the mesh to meshfilter
        GetComponent<MeshFilter>().mesh = mesh;

        // Create the shape of the mesh
        CreateShape();

        // Update the mesh 
        UpdateMesh();

    }

    void CreateShape()
    {
        vertices = new Vector3[]
        {
            // each line is one point
            point1.position,
            point2.position,
            point3.position,
            point4.position
        };

        triangles = new int[]
        {
            // each line is one triangle
            0,1,2,
            1,3,2

        };
    }

    void UpdateMesh()
    {
        // update the mesh with new shape information
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }



    // Update is called once per frame
    void Update()
    {
       // If using them in update, the mesh can be changed on the fly
        //CreateShape();

       //UpdateMesh();
    }

    // Visualize the vertices
private void OnDrawGizmos()
    {
        // this help to visualize the mesh in editor
        if(gizmoOnOff == true)
        {
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;

            CreateShape();

            UpdateMesh();

            Vector3[] vertices = mesh.vertices;

            for (int i = 0; i < vertices.Length; i++)

            {

                Gizmos.DrawWireSphere(center: vertices[i], radius: 0.2f);
            }

        }
        else
        {
            mesh.Clear();
        }


    }
    


}
