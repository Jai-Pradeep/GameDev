using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class RandomFishSpawner : MonoBehaviour
{
    // Array to hold different fish prefabs
    public GameObject[] fishPrefabs;

    // Reference to the AR camera
    public Camera arCamera;

    // Maximum distance to spawn fish from the AR camera
    public float maxSpawnDistance = 2f;

    // Time interval to spawn fish
    public float spawnInterval = 2f;

    private float timer;

    void Start()
    {
        // If no AR camera is assigned, try to find the main camera
        if (arCamera == null)
        {
            arCamera = Camera.main;
        }

        // Start spawning fish after a delay
        timer = spawnInterval;

        for (int i = 0; i <= 20; i++)
        {
            SpawnFish();
        }
    }

    void Update()
    {
        // Count down timer and spawn fish when the interval is up
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnFish();
            timer = spawnInterval;  // Reset the timer
        }
    }

    // Spawns a random fish prefab at a random position
    void SpawnFish()
    {
        // Select a random fish prefab
        int randomIndex = Random.Range(0, fishPrefabs.Length);
        GameObject fishToSpawn = fishPrefabs[randomIndex];

        // Generate a random position within the 2-meter radius of the camera
        Vector3 randomPosition = arCamera.transform.position + new Vector3(
            Random.Range(-maxSpawnDistance, maxSpawnDistance),
            Random.Range(-maxSpawnDistance, maxSpawnDistance),
            Random.Range(-maxSpawnDistance, maxSpawnDistance)
        );

        // Instantiate the selected fish prefab at the random position
        GameObject spawnedObject = Instantiate(fishToSpawn, randomPosition, Quaternion.identity);

        // Randomize rotation around the Y-axis (you can adjust the rotation if needed)
        float randomRotation = Random.Range(0f, 360f);
        spawnedObject.transform.rotation = Quaternion.Euler(0f, randomRotation, 0f);
    }
}
