using System.Collections;
using System.Collections.Generic;
using IronPython.Hosting;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] balloonPrefabs;
    [SerializeField] private GameObject leftBound;
    [SerializeField] private GameObject rightBound;

    
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private int balloonsPerSpawn = 3;
    [SerializeField] private float minDistanceBetweenBalloons = 1.0f;
    
    [HideInInspector]
    public List<GameObject> balloonsList = new();
    
    private float nextSpawnTime;
    private Transform balloonsParent;
    private dynamic difficultyAdjuster;
    
    private void Start()
    {
        InitialisePythonCode();
        nextSpawnTime = Time.time + spawnInterval;
        balloonsParent = new GameObject("Balloons").transform;
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            balloonsPerSpawn = difficultyAdjuster.randomBalloonSpawn();
            
            // Determine the number of balloons to spawn
            int balloonsToSpawn = balloonsPerSpawn;

            int spawnedBalloons = 0;
            while (spawnedBalloons < balloonsToSpawn)
            {
                // Spawn a balloon at a random position between left and right bounds
                Vector3 spawnPosition = GetRandomBalloonPosition();

                // Ensure the new balloon is not too close to existing balloons
                bool isValidSpawn = !CheckOverlapping(spawnPosition);

                // If the spawn position is valid, instantiate the bal loon
                if (isValidSpawn)
                {
                    GameObject balloon = Instantiate(balloonPrefabs[Random.Range(0 , balloonPrefabs.Length)], spawnPosition, Quaternion.identity);
                    balloon.GetComponent<Balloon>().speed = difficultyAdjuster.randomBalloonSpeed();
                    balloon.transform.SetParent(balloonsParent);
                    balloonsList.Add(balloon);
                    spawnedBalloons++;
                }
            }
            
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private Vector3 GetRandomBalloonPosition()
    {
        float randomX = Random.Range(leftBound.transform.position.x, rightBound.transform.position.x);
        return new Vector3(randomX, leftBound.transform.position.y, 0f);
    }

    private bool CheckOverlapping(Vector3 position)
    {
        // Check if the new position overlaps with existing balloons
        foreach (GameObject balloon in balloonsList)
        {
            float distance = Vector3.Distance(position, balloon.transform.position);
            if (distance < minDistanceBetweenBalloons)
            {
                return true;
            }
        }
        return false;
    }

    private void InitialisePythonCode()
    {
        var engine = Python.CreateEngine();

        ICollection<string> searchPaths = engine.GetSearchPaths();
    
        //Path to the folder of greeter.py
        searchPaths.Add(Application.dataPath);
        //Path to the Python standard library
        searchPaths.Add(Application.dataPath + @"\Plugins\Lib\");
        engine.SetSearchPaths(searchPaths);

        dynamic py = engine.ExecuteFile(Application.dataPath + "/Game/Scripts/Python/DifficultyAdjuster.py");
        difficultyAdjuster = py.DifficultyAdjuster();
    }

}
