using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

/// <summary>
/// This script is designed to perform following tasks:
/// 1. make polygon mesh from a set of points,
/// 2. use the same set of points, find the max and min of x and z boundary, then instantiate a larger retangular shaped cube grid
/// 3. use the polygon mesh from step1 to cut the rectangular grid, the cut out grid will be put into a holder object 
/// 4. rename the gird cubes in the holder to unique names
/// 5. the holder object containing correct grid cubes save as prefab
/// 6. the prefab then can be used to be detected by any collider 
/// 
/// How to use:
/// 1. make an empty objct, and reset the Transform.
/// 2. add all the required components.
/// 3. in the inspector, drop in all the required items. (the "points" should be arranged to the shape of your choice)
/// 4. run the game, and while to see the prefab in the prefab folder
/// 
/// </summary>


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]

public class PolygonGridsMaker : MonoBehaviour
{

    // Getting the transform of points objects, the first point is the center points.
    // After putting the first point, the subsequent points should be place on a clockwise order, so the mesh is facing up.
    // If the polygon shape is very complicated, may cut it into several parts, then use this script for each part.
    // Remember, the generated grid should intersect in space, so " points" should be roughly same height with the yHeight

    [Header("Drop all polygon control points here, first is center point")]
    public Transform[] points;

    // Drop the container object here to hold the newly instantiated prefabs
    [Header("Drop the container for polygon grid cubes here")]
    public Transform container;
    // Drop the prefab here (grid cube), the grid cube need to have box collider and set as "isTrigger"
    [Header("Drop the cube prefab here")]
    public GameObject prefab;
    //This is designed to work on the same height y plane. Can be modified to work on other plane or even 3d space
    [Header("The height of the cubes")]
    public float yHeight;

    // The spacing is will be * with the col and row number
    [Header("Spacing of grid centers")]
    public float spacing;

    // Type the base name here 
    [Header("Children grid cube baseName")]
    public string baseName;

    // The cubes in export polygon container
    private GameObject[] children;

    // Export polgyon grid container
    [Header("Drop the export polygon container here")]
    public Transform exportContainer;



    //Rigidbody of the mesh object
    Rigidbody rb;
    //mesh
    Mesh mesh;

    // points and triangles
    Vector3[] vertices;
    int[] triangles;

    // Array to store x and z values from transform.position ( use for finding max and min of x and z)
    private float[] positionsX;
    private float[] positionsZ;

    // Max and min of x and z, from the "points"
    private float xMax;
    private float xMin;

    private float zMax;
    private float zMin;



    // Start is called before the first frame update
    void Start()
    {
        // The content of Start() can be moved into on method and called by pressing key. 

        //Set rigidbody
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;

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

        // Set the mesh to the object mesh collider
        mesh = GetComponent<MeshFilter>().mesh;

        GetComponent<MeshCollider>().sharedMesh = mesh;


        //Find value on start
        FindMaxAndMin();

        // Create the full range grid cubes (will be cut by polygon mesh collider) 
        //InstantiateObj();
        Invoke("InstantiateObj", 2f);


        // After InstantiateObj, the rectangular shaped grid is formed, and the OntriggerStay will move the cut cubes into exportContainer
        //Then rename the cubes in exportContainer
        Invoke("RenameChildren", 4f);

        //After renaming, the exportContainer will be saved as a prefab.
        Invoke("CreatePrefab", 6f);



    }

    void CreateShape()
    {
        // Get positions from points list
        vertices = points.Select(x => x.position).ToArray();

        // Setting the size of arrary based on the length of the points arrary length.
        // for n points, there are ( n-1 ) triangles, so the size of the array is 3*(n-1). 
        // (This is becasue, the first point in the "points" is used a the center of the polygon, and all triangles were arranged around it.
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

    void InstantiateObj()
    {
        // Get z and x range. z for row, x for column 
        int zRange = (int)(zMax - zMin);

        int xRange = (int)(xMax - xMin);

        // Two for loop to go through each point in the grid.
        // Since the range is converted from float to int, add one and use <= in the for loop to make sure the range is large enough
        // This can also use Math.Ceiling() ,Math.Floor() or Math.Round() Function depending on actual situation
        for (int row = 0; row <= zRange + 1; row++)
        {
            for (int col = 0; col <= xRange + 1; col++)
            {
                // Instantiated the prefab at the grid point position and set the parent object to be the container object
                Vector3 instantPos = new Vector3(xMin + col * spacing, yHeight, zMin + row * spacing);
                GameObject _prefab = Instantiate(prefab, instantPos, Quaternion.identity);

                // Edit the tag for the prefab (so the collider can use tag to cut grid)
                _prefab.tag = "mark";
                _prefab.transform.SetParent(container);
            }
        }
    }


    // This is using the mesh collider to intersect with generated grid
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("mark"))
        {
            other.gameObject.transform.SetParent(exportContainer.transform);
        }
    }

    private void RenameChildren()
    {
        // Set the size of array based on child count
        children = new GameObject[exportContainer.childCount];

        // Get every children objects and rename them, also debug log their new names
        for (int i = 0; i < exportContainer.childCount; i++)
        {
            children[i] = exportContainer.GetChild(i).gameObject;
            children[i].name = baseName + " " + i;
            //Debug.Log(children[i].name);
            //Debug.Log("and the instanceID is " + children[i].GetInstanceID());
        }
    }


    // This function will take exportContainer that is already filled with correct shaped grid cubes and save it as a prefab
    private void CreatePrefab()
    {

        var savePath = "Assets/Prefabs/" + exportContainer.name + ".prefab";

        PrefabUtility.SaveAsPrefabAsset(exportContainer.gameObject, savePath, out bool success);

    }


    // Use onDrawGizmos to show the boundary in Editor
    // If don't want to show the OnDrawGizmos, just collapse the script in the inspector
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
        Gizmos.DrawSphere(new Vector3(xMax, 1, zMax), 0.3f);
        Gizmos.DrawSphere(new Vector3(xMax, 1, zMin), 0.3f);
        Gizmos.DrawSphere(new Vector3(xMin, 1, zMax), 0.3f);
        Gizmos.DrawSphere(new Vector3(xMin, 1, zMin), 0.3f);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
