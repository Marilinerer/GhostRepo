using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab; // Assign the car prefab in the Inspector
    public float minSpawnInterval = 0.25f; // Minimum time between spawns
    public float maxSpawnInterval = 3f; // Maximum time between spawns
    public bool spawnRightDirection = true; // Set direction in Inspector

    private Vector3 spawnPosition;

    void Start()
    {
        // Get screen boundary
        float screenBoundsX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

        // Set spawn position just outside the screen
        spawnPosition = new Vector3(spawnRightDirection ? -screenBoundsX - 2f : screenBoundsX + 2f,
                                    transform.position.y,
                                    transform.position.z);

        // Move spawner to correct side
        transform.position = spawnPosition;

        // Start spawning with a random interval
        Invoke(nameof(SpawnCar), Random.Range(minSpawnInterval, maxSpawnInterval));
    }

    void SpawnCar()
    {
        GameObject newCar = Instantiate(carPrefab, transform.position, Quaternion.identity);

        // Set the car's direction
        CarMovement carScript = newCar.GetComponent<CarMovement>();
        if (carScript != null)
        {
            carScript.moveRight = spawnRightDirection;
        }

        // Call the next spawn with a random interval
        Invoke(nameof(SpawnCar), Random.Range(minSpawnInterval, maxSpawnInterval));
    }
}


