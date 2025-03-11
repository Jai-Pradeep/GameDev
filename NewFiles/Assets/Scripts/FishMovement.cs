using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class FishMovement : MonoBehaviour
{
    public float speedMin = 1f; // Minimum speed
    public float speedMax = 3f; // Maximum speed
    public float turnIntervalMin = 2f; // Minimum interval for random turns (in seconds)
    public float turnIntervalMax = 5f; // Maximum interval for random turns (in seconds)
    public float maxDistance = 100000f;

    private Vector3 fishDirection;  // Direction the fish is facing
    private float speed;            // Speed of the fish
    private float turnInterval;     // Random interval for turning
    private float lastTurnTime;     // Time of the last turn
    private float lastSpeedChangeTime; // Time of the last speed change
    public Camera aRCamera;

    void Start()
    {
        // Initialize fish direction and speed
        RandomlyTurn();
        speed = Random.Range(speedMin, speedMax);
        turnInterval = Random.Range(turnIntervalMin, turnIntervalMax);
        lastTurnTime = Time.time;
        lastSpeedChangeTime = Time.time;

        aRCamera = Camera.main;

    }

    void Update()
    {
        // Move the fish
        transform.position += fishDirection * speed * Time.deltaTime;

        // Check if it's time to change the direction
        if (Time.time - lastTurnTime >= turnInterval)
        {
            RandomlyTurn();
            lastTurnTime = Time.time;
            turnInterval = Random.Range(turnIntervalMin, turnIntervalMax);
        }

        // Check if it's time to change speed
        if (Time.time - lastSpeedChangeTime >= 2f)  // change speed every 2 seconds, for example
        {
            ChangeSpeed();
            lastSpeedChangeTime = Time.time;
        }

 // Check distance from AR camera
        if (Vector3.Distance(transform.position, aRCamera.transform.position) > maxDistance)
        {
            //Destroy(gameObject); // Destroy the fish if it exceeds the max distance

            //Debug.Log("Fish deleted");
        }
    }

    // Randomly change the fish's direction
    void RandomlyTurn()
    {
        // Choose a random angle around the Y axis (in degrees)
        float randomAngleY = Random.Range(-60f, 60f);
        // Choose a random angle for tilt on X and Z axis
        float randomAngleX = Random.Range(-10f, 10f);
        float randomAngleZ = Random.Range(-30f, 30f);

        // Rotate the fish around the Y axis
        transform.Rotate(new Vector3(randomAngleX, randomAngleY, randomAngleZ), Space.Self);
        // Update the fish's direction (forward direction)
        fishDirection = -transform.right;
    }

    // Change the fish's speed randomly
    void ChangeSpeed()
    {
        speed = Random.Range(speedMin, speedMax);
    }
}
