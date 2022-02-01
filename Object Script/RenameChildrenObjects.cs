using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used to rename the children object to the same base name and the sequence number.
/// </summary>
public class RenameChildrenObjects : MonoBehaviour
{
    // Type the base name here
    public string baseName;

    //This is used to form an array for children objects
    private GameObject[] children;

    // Start is called before the first frame update
    void Start()
    {
        // Set the size of array based on child count
        children = new GameObject[transform.childCount];

        // Get every children objects and rename them, also debug log their new names
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i).gameObject;
            children[i].name = baseName + " " + i;
            Debug.Log(children[i].name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
