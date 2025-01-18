using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageFloating : MonoBehaviour
{
    // Movement parameters
    public float moveSpeed = 1f; // How fast the garbage moves
    public float bobbingSpeed = 1f; // Speed of the bobbing motion (up and down)
    public float bobbingHeight = 0.2f; // Height of the bobbing motion

    // Random movement range (within an area around the garbage's starting position)
    public float moveRangeX = 1f;
    public float moveRangeY = 1f;
    public float moveRangeZ = 1f;

    private Vector3 startPosition; // Initial position of the garbage
    private Vector3 targetPosition; // The target position for movement
    private float startYPosition;  // Original Y position to maintain float level

    void Start()
    {
        // Record the starting position of the garbage
        startPosition = transform.position;
        startYPosition = startPosition.y;

        // Set the initial target position within the movement range
        SetRandomTargetPosition();
    }

    void Update()
    {
        // Move the garbage towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // If the garbage reaches the target position, pick a new random target
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }

        // Bobbing motion - making the garbage move up and down (float effect)
        float bobbingOffset = Mathf.Sin(Time.time * bobbingSpeed) * bobbingHeight;

        // Apply bobbing offset to the Y position
        transform.position = new Vector3(transform.position.x, startYPosition + bobbingOffset, transform.position.z);
    }

    // Sets a new random target position within the defined movement range
    void SetRandomTargetPosition()
    {
        float randomX = Random.Range(-moveRangeX, moveRangeX);
        float randomY = startYPosition; // Maintain original Y position for floating
        float randomZ = Random.Range(-moveRangeZ, moveRangeZ);

        targetPosition = new Vector3(startPosition.x + randomX, randomY, startPosition.z + randomZ);
    }
}
