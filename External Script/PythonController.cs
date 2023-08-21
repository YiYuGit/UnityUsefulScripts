using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class PythonController : MonoBehaviour
{
    // Path to the Python executable
    string pythonExe = "C:\\Program Files\\Python310\\python.exe"; // Replace with the actual path to the python executable

    // Path to the Python script
    string pythonScriptPath = "Y:\\PhotoLogGen.py"; // Replace with the actual path to your Python script

    public void RunPythonScript()
    {
        // Create a process to run the Python script
        Process process = new Process();
        process.StartInfo.FileName = pythonExe;
        process.StartInfo.Arguments = pythonScriptPath;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.CreateNoWindow = true;

        // Start the process
        process.Start();

        // Read the output and error messages
        string output = process.StandardOutput.ReadToEnd();
        string errors = process.StandardError.ReadToEnd();

        // Wait for the process to exit
        process.WaitForExit();

        // Display the output and errors (for debugging)
        UnityEngine.Debug.Log("Python Output:\n" + output);
        UnityEngine.Debug.LogError("Python Errors:\n" + errors);
    }
}
