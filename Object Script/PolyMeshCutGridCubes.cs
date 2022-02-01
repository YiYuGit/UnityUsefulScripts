using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used to attach to a mesh object and use the mesh to cut grid cubes and then move the cutout cubes to the container object
/// This should be used with PolyMesh.cs , SaveMeshInEditor.cs and RenameChildrenObjects.cs
/// 
/// </summary>

public class PolyMeshCutGridCubes : MonoBehaviour
{
    // The empty object that hold the grid cubes that are cut by poly mesh collider
    public GameObject cutContainer;


    //Use OnTriggerEnter to collide with the cubes and set them as the children of cutContainer
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("mark"))
        {
            other.gameObject.transform.SetParent(cutContainer.transform);
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
