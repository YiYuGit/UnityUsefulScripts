using UnityEngine;

/// <summary>
/// This script is usd for requesting compass and location permissions at the start of the app.
/// Also, the first line request the screen to never sleep.
/// </summary>

public class PermissionController : MonoBehaviour
{
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        // Check if compass is not enabled; if not, request location service
        // On first use, the app will ask for the locaiton permission from user on the phone.
        if (!Input.compass.enabled)
        {
            Input.location.Start();
        }
    }
}
