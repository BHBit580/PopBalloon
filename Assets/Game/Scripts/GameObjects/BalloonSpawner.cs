using System;
using System.Collections;
using System.Collections.Generic;
using IronPython.Hosting;
using IronPython.Runtime;
using UnityEngine;
using Random = UnityEngine.Random;

public class BalloonSpawner : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO gameOver;
    [SerializeField] private GameObject[] balloonPrefabs;
    [SerializeField] private GameObject leftBound;
    [SerializeField] private GameObject rightBound;

    
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private int balloonsPerSpawn = 3;
    
    private float nextSpawnTime;
    private Transform balloonsParent;
    private dynamic difficultyAdjuster;

    private void Awake()
    {
        gameOver.RegisterListener(TurnOffSpawning);
    }
    
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
            balloonsPerSpawn = (int)difficultyAdjuster.getBalloonSpawn();
            
            
            int spawnedBalloons = 0;
            while (spawnedBalloons < balloonsPerSpawn)
            {
                // Spawn a balloon at a random position between left and right bounds
                Vector3 spawnPosition = GetRandomBalloonPosition();
                GameObject balloon = Instantiate(balloonPrefabs[Random.Range(0 , balloonPrefabs.Length)], spawnPosition, Quaternion.identity);
                balloon.GetComponent<Balloon>().speed = difficultyAdjuster.balloonSpeed();
                balloon.transform.SetParent(balloonsParent);
                spawnedBalloons++;
            }

            spawnInterval = (float)difficultyAdjuster.spawnRate();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private Vector3 GetRandomBalloonPosition()
    {
        float randomX = Random.Range(leftBound.transform.position.x, rightBound.transform.position.x);
        return new Vector3(randomX, leftBound.transform.position.y, 0f);
    }
    
    private void InitialisePythonCode()
    {
        Debug.Log("Initialising Python Code...");
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
    
    private void TurnOffSpawning() => gameObject.SetActive(false);
    
    private void OnDestroy() => gameOver.UnregisterListener(TurnOffSpawning);
    
}
