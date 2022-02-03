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
    // Using array here may not be the best solution, may change to using List if needed.
    // Here are the basics of why a List is better and easier to use than an array:

    //An array is of fixed size and unchangeable
    //The size of a List is adjustable
    //You can easily add and remove elements from a List
    //To mimic adding a new element to an array, we would need to create a whole new array with the desired number of elements and then copy the old elements

    //Add: This adds an object at the end of List<T>.
    //Remove: This removes the first occurrence of a specific object from List<T>.
    //Clear: This removes all elements from List<T>.
    //Contains: This determines whether an element is in List<T> or not.It is very useful to check whether an element is stored in the list.
    //Insert: This inserts an element into List<T> at the specified index.
    //ToArray: This copies the elements of List<T> to a new array.


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
            //Debug.Log(children[i].name);
            //Debug.Log("and the instanceID is " + children[i].GetInstanceID());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
