using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System;
using System.Globalization;
using System.Threading;
using System.Text.RegularExpressions;

/// <summary>
/// This script is used to test the coverage of a mesh collider in certain area(populated with cubes, cubes tag with "mark")
/// (created by the "PolygonGridsMaker.cs")
/// The script is attached to a mesh object, the mesh collider is acted as the detector. The detector is usually tied to a moving player.
/// The ground is prepared with ground grid cubes(they are the children of a holder object)
/// Once started, the script get the mesh from meshfilter to the mesh collider.(in 1 second)
/// Player then go through the field and collected the detected grids in the visualList.
/// At the end of each run. Press the saveFileKey to save the data in a CSV file. 
/// The visible rate is automatically calculated and saved in the CSV file.
/// </summary>


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]

public class VisibilityTest : MonoBehaviour
{
    // Key code for save function
    public KeyCode saveFileKey = KeyCode.P;

    // Name for the csv file
    public string saveFileName = "VisibilityData";

    // Drag and drop the ground grid cubes holder, later used to calculate visible rate
    public GameObject groundGridHolder;

    //Make a list for holding the detected visible grid object
    public List<GameObject> visualList = new List<GameObject>();

    //New material for visible object if detected, optional
    public Material visibleMaterial;

    //The mesh for making the detector
    Mesh mesh;

    //Select if you want the mesh to be seen or not
    public bool meshShown = true;

    // OnTriggerStay is used to make sure it detect the other object
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("mark"))
        {
            if(visualList.Contains(other.gameObject))
            {
                // ignore or debug out information
                //Debug.Log("Hit " + other.name);

                // for visualization purpose, can change the material
                other.GetComponent<MeshRenderer>().material = visibleMaterial;
            }
            else
            {
                // If not already in the visualList, add it to the list
                visualList.Add(other.gameObject);
            }

        }
    }

    private void LoadMeshFromFilter()
    {
        // Set the mesh to the object mesh collider
        mesh = GetComponent<MeshFilter>().mesh;

        GetComponent<MeshCollider>().sharedMesh = mesh;

    }

    private void HandleMeshRender()
    {
        //Check if meshShown is true or false, if false, turn off the mesh render, so user won't see it on screen.
        if(meshShown == false)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
    }

    // Sorting the visualList by name
    private void SortList()
    {
        List<GameObject> sortedVisualList = visualList.OrderBy(x => x.name).ToList();
        visualList = sortedVisualList;
        Debug.Log("Sorted");
    }

    //The data saving method
    private void SaveVisibilityData()
    {

        // Get the total count of ground grid cubes
        int gridCount = groundGridHolder.transform.childCount;

        // Take the date and time as the first ling for the recording file
        WriteToFile("\n" + "\n" + "\n" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

        //Sort the list before write into the file
        SortList();

        //var amount = visualList.Count;
        //Count and write the total number of cubs in visualList
        WriteToFile("\n" + "Total visible cubes: " + visualList.Count);

        //Count and write the total number of cubs in ground grid holder
        WriteToFile("\n" + "Total ground cubes: " + gridCount);

        //Calculate the visible rate
        float visibleRate = (float)visualList.Count/(float)gridCount;
        WriteToFile("\n" + "Visible rate is " + Math.Round(visibleRate,3));

        //The beginning of visible cubes list
        WriteToFile("\n" + "List of visible cubes are: ");

        // Write each gameobject's name into the file
        foreach (GameObject cube in visualList)
        {
            var nameText = cube.name;

            WriteToFile("\n" + nameText + ",");

        }
    }

    // Write method 
    public void WriteToFile(string message)
    {
        // The path is in assets folder, can be changed to other path
        string path = Application.dataPath + "/CSV/" + saveFileName + ".csv";
        try
        {
            StreamWriter filewriter = new StreamWriter(path, true);
            filewriter.Write(message);
            filewriter.Close();
        }
        catch
        {
            Debug.LogError("cannot write to the file");
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        // On start, determine the mesh render should be on or off
        HandleMeshRender();

        // public void Invoke(string methodName, float time); Invokes the method methodName in time seconds.
        Invoke("LoadMeshFromFilter", 1.0f);


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(saveFileKey))
        {
            SaveVisibilityData();
        }
    }
}
