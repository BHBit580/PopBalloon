using System.IO;
using UnityEngine;

public class GameParameters : MonoBehaviour
{
    public float balloonSpawnRate;
    public float balloonSpeed;

    void Start()
    {
        // Read the JSON file
        string json = File.ReadAllText("game_parameters.json");

        // Parse the JSON file
        GameParameters parameters = JsonUtility.FromJson<GameParameters>(json);

        // Set the game parameters
        balloonSpawnRate = parameters.balloonSpawnRate;
        balloonSpeed = parameters.balloonSpeed;
    }
}