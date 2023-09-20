using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using TMPro;

/// <summary>
/// This script can read the content from the csv file and display it on the UI text.
/// It can also read the png file name, make the corresponding image into a material and assign it to the display object.
/// The delete function have a confirm function, so the user can confirm if 
/// they want to delete the current comment and the txt and png file with the same name.
/// 
/// To use this script, attach it to an empty object, and link the public funcitons with UI buttons and display image object.
/// The csv file is used for linking the png and txt, also for deleting them.
/// Pay attention to the file path.
/// 
/// This can be very useful in displaying text and image in Unity and deleting files in Streaming Assets folder.
/// </summary>


public class ShowAndDeleteBookmark : MonoBehaviour
{
    // Reference to the UI Text component for displaying CSV lines
    public TMP_Text displayText; 

    // Two buttons for confirming the deletion, they are turned off during browsing. Only turned on when confirming deletion
    public GameObject confirmDeleteButton;
    public GameObject cancelDeleteButton;

    // The object for displaying the current image
    public GameObject imageDisplayObject;

    // private variables
    private string csvFilePath;
    private string[] csvLines;
    private int currentLineIndex;

    private void Start()
    {
        // Turn off the buttons on start
        confirmDeleteButton.SetActive(false);
        cancelDeleteButton.SetActive(false);

        // Set the path to the bookmark CSV file's name in StreamingAssets
        csvFilePath = Application.streamingAssetsPath + "/CSV_log/" + "MBookmark.csv"; 

        // Read the CSV file
        ReadCSVFile();

        // Display the first line
        ShowCurrentLine();
        ShowCurrnetImage();
    }

    private void ReadCSVFile()
    {
        // if the csv bookmark file exist, read all lines to the string list of csvLines
        if (File.Exists(csvFilePath))
        {
            // Read all lines from the CSV file
            csvLines = File.ReadAllLines(csvFilePath);

            // Initialize the current line index
            currentLineIndex = 1;
        }
        else
        {
            Debug.LogError("CSV file not found: " + csvFilePath);
        }
    }

    // Called by other function to find the current line 
    private void ShowCurrentLine()
    {
        // make sure the csvLines is not empty and the currentlineIndex is smaller than the length of the list
        if (csvLines != null && currentLineIndex < csvLines.Length)
        {
            // These 3 lines are not necessary here, just showing how line section can be break into individual strings and combine again
            // Will be useful in accessing specific cell location content

            //string line = csvLines[currentLineIndex];
            //string[] lineSections = line.Split(',');
            //string lineText = string.Join(",", lineSections);

            string lineText = csvLines[currentLineIndex];
            // This is used to replace ; with , . to make comment back to normal. Because when writing text to csv, the ","s were replace with ";" ,is this reverse
            lineText = lineText.Replace(";", ",");
            displayText.text = lineText;
        }
    }

    // Called by other script to find the current image and make it into a new material, also assign it to the display object's renderer
    private void ShowCurrnetImage()
    {
        // get the current line, split it into string list. Then, in this case, the file name is in the second cell of each line, thus using [1]
        // assemble the file name and streaming asssets path with file name to get teh pngFilePath
        string line = csvLines[currentLineIndex];
        string[] lineSections = line.Split(',');
        string fileNameBase = lineSections[1];
        string pngFileName = fileNameBase + ".png";
        string pngFilePath = Application.streamingAssetsPath + "/capture/" + pngFileName;

        // Make a new texture, read all bytes from the image path and load the image Bytes to the texture
        Texture2D texture = new Texture2D(2, 2);
        byte[] imageBytes = System.IO.File.ReadAllBytes(pngFilePath);
        texture.LoadImage(imageBytes);

        // Make a new material with standard shader, assign the previous texture to the mainTexture of this material
        Material material = new Material(Shader.Find("Standard"));
        material.mainTexture = texture;

        // Based on experience, for new material, the tiling new to be -1 -1 to match the image direction. Adjust in your case.
        material.mainTextureScale = new Vector2(-1, -1);

        // Get the renderer fro mthe display object, if not null, assign the new material to the renderer
        Renderer renderer = imageDisplayObject.GetComponent<Renderer>();
        if (renderer != null )
        {
            renderer.material = material;
        }
        else
        {
            Debug.LogError("No renderer");
        }

    }


    // This function need to be called by button or other script to show the next bookmark.
    public void ShowNextBookmark()
    {
        if (csvLines.Length != 1)
        {
            currentLineIndex++;
        }
            
        if (currentLineIndex >= csvLines.Length)
        {
            currentLineIndex = 1;
        }
        ShowCurrentLine();
        ShowCurrnetImage();

    }

    // This function need to be called by button or other script to show the previous bookmark.
    public void ShowPrevBookmark()
    {
        if (csvLines.Length != 1)
        {
            currentLineIndex--;
        }

        if (currentLineIndex < 1)
        {
            currentLineIndex = csvLines.Length - 1;
        }
        ShowCurrentLine();
        ShowCurrnetImage();
    }

    // This is linked to a "delete" button, when clicked, UI will turn on these two buttons to let user confirm.
    public void ShowConfirmDeleteButton()
    {
        confirmDeleteButton.SetActive(true);
        cancelDeleteButton.SetActive(true);
    }

    // This is linked to a "cancel" button, when clicked, these two buttons will be turned off. nothing changed on the files.
    public void HideConfirmDeleteButton()
    {
        confirmDeleteButton.SetActive(false);
        cancelDeleteButton.SetActive(false);
    }

    // This is linked the real delete button or usually named "confirm", to really delete the record in the csv file and delete the corresponding png and txt file
    // The lines in this file can be used to manipulate the content of csv file. 
    // ********Consider adding an edit function for the content. Should be something like replace part of the string list and write all lines to file.************
    public void DeleteCurrentLine()
    {
        if (csvLines != null && currentLineIndex < csvLines.Length)
        {
            // Delete the corresponding png and txt file from "capture" and "Txt_log" folder
            string line = csvLines[currentLineIndex];
            string[] lineSections = line.Split(',');
            string fileNameBase = lineSections[1];
            string pngFileName = fileNameBase + ".png";
            string txtFileName = fileNameBase + ".txt";
            string pngFilePath = Application.streamingAssetsPath + "/capture/" + pngFileName;
            string txtFilePath = Application.streamingAssetsPath + "/Txt_log/" + txtFileName;

            // Check if the file exists
            if (File.Exists(pngFilePath))
            {
                // Delete the file
                File.Delete(pngFilePath);
                Debug.Log("File deleted: " + pngFileName);
            }
            else
            {
                Debug.LogError("File not found: " + pngFileName);
            }

            if (File.Exists(txtFilePath))
            {
                // Delete the file
                File.Delete(txtFilePath);
                Debug.Log("File deleted: " + txtFileName);
            }
            else
            {
                Debug.LogError("File not found: " + txtFileName);
            }



            // Remove the current line from the array
            var tempList = new List<string>(csvLines);
            tempList.RemoveAt(currentLineIndex);
            csvLines = tempList.ToArray();

            // Update the file with the modified lines
            File.WriteAllLines(csvFilePath, csvLines);

            // Show the next line (if any)
            if (currentLineIndex < csvLines.Length)
            {
                ShowCurrentLine();
                ShowCurrnetImage();
            }
            else
            {
                // No more lines, clear the UI
                displayText.text = "";

                // One of the option for image after deletion of the record. make it null 
                //Renderer renderer = imageDisplayObject.GetComponent<Renderer>();
                //renderer.material = null;

                //  Another option. turn off the display object
                imageDisplayObject.SetActive(false);
            }



            // After deletion, turn off the two buttons
            confirmDeleteButton.SetActive(false);
            cancelDeleteButton.SetActive(false);


        }
    }
}
