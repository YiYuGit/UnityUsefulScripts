using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Globalization;
using System.Threading;

public class FindAllObjectWithTag : MonoBehaviour
{
    // The objects that will be find
    public GameObject[] paintedMarks;

    // Start is called before the first frame update
    void Start()
    {
        //Write the head of the csv file, adjust for different purpose accordingly
        WriteToFile("\n" + "ObjectName" + "," + "position-x" + "," + "position-y" + "," + "position-z" + "," + "rotation-x" + "," + "rotation-y" + "," + "rotation-z" + "," + "rotation-w");
    }

    // Update is called once per frame
    void Update()
    {
        // At the end of game, Press a key to find all gameobject with "mark" tag in the scene
        if (Input.GetKeyDown("l"))
        {
            FindMakrObjects();

        }
    }

    void FindMakrObjects()
    {
        //Find all gameobject with the tag 
        paintedMarks = GameObject.FindGameObjectsWithTag("mark");

        {
            // For each tag, find their prefab info ,and transform info, print in the console and write to file for recording

            foreach (GameObject paintedMark in paintedMarks)
            {

                print(paintedMark.name);
                print(paintedMark.transform.position);
                print(paintedMark.transform.rotation);

                // Write to file
                WriteToFile("\n" + paintedMark.name + "," + paintedMark.transform.position.x + "," + paintedMark.transform.position.y + "," + paintedMark.transform.position.z + "," + paintedMark.transform.rotation.x + "," + paintedMark.transform.rotation.y + "," + paintedMark.transform.rotation.z + "," + paintedMark.transform.rotation.w + "," + paintedMark.transform.rotation);


            }

        }


        //print("space key was pressed");
    }

    public void WriteToFile(string message)
    {
        // The path is in assets folder, can be changed to other path
        string path = Application.dataPath + "/ObjectLocationData.csv";
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

}
