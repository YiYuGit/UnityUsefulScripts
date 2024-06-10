using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

public class LocationNoteTxtWriter : MonoBehaviour
{
    // Drop the input field here
    [Header("Drop user comment input field here")]
    public TMP_InputField inputField;

    // Put the text showing the note has been recorded.
    public TMP_Text noteTakenText;


    // Temp value storage
    private float latitude;
    private float longitude;
    private float altitude;
    private float heading;


    // Start is called before the first frame update
    void Start()
    {
        // Set the screen to never sleep
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        // Turn on the location 
        if (!Input.compass.enabled)
        {
            Input.location.Start();
            Input.compass.enabled = true;
        }

        // Set path for the field note file
        Directory.CreateDirectory(Application.persistentDataPath + "/Txt_log/");


    }


    public void CreateTxtFile()
    {
        string txtFileName = Application.persistentDataPath + "/Txt_log/" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".txt";

        // Check if the file exists
        if (!File.Exists(txtFileName))
        {

            // Get the current location
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            altitude = Input.location.lastData.altitude;

            // Get the compass heading
            heading = Input.compass.trueHeading;

            // Get the current date and time
            string currentDateTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

            // Get the user comment from inputField
            string userComment = inputField.text;

            File.WriteAllText(txtFileName, currentDateTime);
            File.AppendAllText(txtFileName, "\n");
            File.AppendAllText(txtFileName, latitude.ToString());
            File.AppendAllText(txtFileName, "\n");
            File.AppendAllText(txtFileName, longitude.ToString());
            File.AppendAllText(txtFileName, "\n");
            File.AppendAllText(txtFileName, altitude.ToString());
            File.AppendAllText(txtFileName, "\n");
            File.AppendAllText(txtFileName, heading.ToString());
            File.AppendAllText(txtFileName, "\n");
            File.AppendAllText(txtFileName, userComment);


            // Secction some of the comment for displaying
            string subSectionUserComment = inputField.text;
            if (subSectionUserComment.Length >= 20)
            {
                subSectionUserComment = "The comment of: " + subSectionUserComment.Substring(0, 19) + " . . . . has been recorded." + "\n" + "Location: " + latitude.ToString() + ", " + longitude.ToString() + "\n" + "Heading: " + heading.ToString();
            }
            else
            {
                subSectionUserComment = "The comment of: " + subSectionUserComment + " . . . . has been recorded." + "\n" + "Location: " + latitude.ToString() + ", " + longitude.ToString() + "\n" + "Heading: " + heading.ToString();
            }



            // empty the input field
            inputField.text = null;

            // Feedback to user
            noteTakenText.text = subSectionUserComment;

        }

    }



    // Link to a button for quit
    public void QuitButton()
    {

        Application.Quit();

    }


    // Update is called once per frame
    void Update()
    {

    }
}
