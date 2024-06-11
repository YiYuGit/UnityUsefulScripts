using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This script is for displaying the current location and heading of the phone on the screen.
/// </summary>

public class UIController : MonoBehaviour
{
    public TMP_Text gpsText;
    public TMP_Text compassStatusText;

    void Start()
    {
        // Check if location services are enabled
        if (Input.location.isEnabledByUser)
        {
            Input.location.Start();
        }
        else
        {
            gpsText.text = "Location services are not enabled.";
        }

        Input.compass.enabled = true;


        // Check if compass is enabled
        if (Input.compass.enabled)
        {
            compassStatusText.text = "Compass is enabled.";
        }
        else
        {
            compassStatusText.text = "Compass is not enabled.";
        }
    }

    void Update()
    {
        // Get the latitude and longitude
        float latitude = Input.location.lastData.latitude;
        float longitude = Input.location.lastData.longitude;
        float altitude = Input.location.lastData.altitude;

        // Display the location information on the GPS UI text
        gpsText.text = "Latitude: " + latitude.ToString() + "\nLongitude: " + longitude.ToString() + "\nAltitude: " + altitude.ToString();

        // Get the compass heading
        float heading = Input.compass.trueHeading;

        // Display compass status and heading
        compassStatusText.text = "Compass is " + (Input.compass.enabled ? "enabled" : "not enabled") + "\nHeading: " + heading.ToString("0.00") + " degrees";
    }
}
