using System.Diagnostics;
using UnityEngine;


public class PythonRunner : MonoBehaviour
{
    void Start()
    {
        RunPythonScript();
    }

    void RunPythonScript()
    {
        // Create a new process object
        Process process = new Process();

        // Configure the process
        process.StartInfo.FileName = "python";
        process.StartInfo.Arguments = @"C:\Users\UJJAWAL SAINI\UNITYPROJECTS\PopBalloon\Assets\script.py";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;

        // Start the process
        process.Start();

        // Read the output of the Python script
        string output = process.StandardOutput.ReadToEnd();

        // Wait for the Python script to exit
        process.WaitForExit();

        // Log the output of the Python script
        UnityEngine.Debug.Log(output);
    }
}