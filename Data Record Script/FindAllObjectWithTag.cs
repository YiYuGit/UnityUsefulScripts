using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Globalization;
using System.Threading;
using System.Text.RegularExpressions;

/// <summary>
/// This script is used for recording all gameobjects(with"mark" tag) that instantiated during the gameplay.
/// Attach this script to an empty object to make this recorder obejct.
/// When gameplay is over, before exit the game, press the "l" key.
/// The script will find all object with "mark' tag.
/// Write their name, position and rotation into CSV file. The script will remove the "(clone)" part of the name, so later, the name can be used to match prefab name.
/// </summary>



public class FindAllObjectWithTag : MonoBehaviour
{
    // The objects that will be find
    public GameObject[] paintedMarks;

    // Start is called before the first frame update
    void Start()
    {
        //Write the head of the csv file, adjust for different purpose accordingly
        WriteToFile("objectName" + "," + "position_x" + "," + "position_y" + "," + "position_z" + "," + "rotation_x" + "," + "rotation_y" + "," + "rotation_z" + "," + "rotation_w");
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
                // THe next few lines remove the "(Clone)" from the mark name, so when reproducing the objects, the other script can match the name.
                var nameText = paintedMark.name;

                // Remove text between brackets.
                nameText = Regex.Replace(nameText, @"\(.*\)", "");

                // Remove extra spaces. Use if needed
                //nameText = Regex.Replace(nameText, @"\s+", " ");

                print(nameText);
                print(paintedMark.transform.position);
                print(paintedMark.transform.rotation);

                // Write to file
                WriteToFile("\n" + nameText + "," + paintedMark.transform.position.x + "," + paintedMark.transform.position.y + "," + paintedMark.transform.position.z + "," + paintedMark.transform.rotation.x + "," + paintedMark.transform.rotation.y + "," + paintedMark.transform.rotation.z + "," + paintedMark.transform.rotation.w);


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
