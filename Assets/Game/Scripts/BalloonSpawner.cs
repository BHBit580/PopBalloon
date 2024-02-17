using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] balloonPrefabs;
    [SerializeField] private GameObject leftBound;
    [SerializeField] private GameObject rightBound;
    [SerializeField] private float spawnIntervalMin = 2f;
    [SerializeField] private float spawnIntervalMax = 5f;
    [SerializeField] private int minBalloonsPerSpawn = 1;
    [SerializeField] private int maxBalloonsPerSpawn = 3;
    [SerializeField] private float minDistanceBetweenBalloons = 1.0f;

    private float nextSpawnTime;
    public List<GameObject> balloonsList = new List<GameObject>();
    private Transform balloonsParent; 
    private void Start()
    {
        // Set the initial spawn time
        nextSpawnTime = Time.time + Random.Range(spawnIntervalMin, spawnIntervalMax);
        balloonsParent = new GameObject("Balloons").transform;
    }

    private void Update()
    {
        // Check if it's time to spawn balloons
        if (Time.time >= nextSpawnTime)
        {
            // Determine the number of balloons to spawn
            int balloonsToSpawn = Random.Range(minBalloonsPerSpawn, maxBalloonsPerSpawn + 1);

            for (int i = 0; i < balloonsToSpawn; i++)
            {
                // Spawn a balloon at a random position between left and right bounds
                Vector3 spawnPosition = GetRandomBalloonPosition();

                // Ensure the new balloon is not too close to existing balloons
                bool isValidSpawn = !CheckOverlapping(spawnPosition);

                // If the spawn position is valid, instantiate the balloon
                if (isValidSpawn)
                {
                    GameObject balloon = Instantiate(balloonPrefabs[Random.Range(0 , balloonPrefabs.Length)], spawnPosition, Quaternion.identity);
                    balloon.transform.SetParent(balloonsParent);
                    balloonsList.Add(balloon);
                }
            }

            // Set the next spawn time within the interval
            nextSpawnTime = Time.time + Random.Range(spawnIntervalMin, spawnIntervalMax);
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
}
