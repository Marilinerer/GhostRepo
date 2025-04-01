using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*ublic class PlayerPossession : MonoBehaviour
{
    private GameObject possessedObject;
    private CarMovement possessedCar;
    private bool isPossessing;
    private SpriteRenderer ghostRenderer; // For ghost visibility

    void Start()
    {
        ghostRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            if (isPossessing)
            {
                ReleasePossession();
            }
            else
            {
                TryToPossess();
            }
        }
    }

    private void TryToPossess()
    {
        // Find nearby objects with "Possessable" tag
        Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D obj in nearbyObjects)
        {
            if (obj.CompareTag("Possessable"))
            {
                possessedObject = obj.gameObject;
                possessedCar = possessedObject.GetComponent<CarMovement>(); // Get CarMovement script

                if (possessedCar != null)
                {
                    possessedCar.Possess(); // Stop the car when possessed
                }

                isPossessing = true;
                ghostRenderer.enabled = false; // Hide the ghost

                // Move ghost inside the object
                transform.position = possessedObject.transform.position;

                // Disable ghost movement
                GetComponent<PlayerController>().enabled = false;
                break;
            }
        }
    }

    private void ReleasePossession()
    {
        if (possessedObject != null)
        {
            // Set ghost position to the possessed object's position
            transform.position = possessedObject.transform.position;

            // Stop possessing car
            if (possessedCar != null)
            {
                possessedCar.Release(); // Resume car movement
                possessedCar = null;
            }

            // Detach from the object
            possessedObject = null;
            isPossessing = false;

            // Make the ghost visible again
            ghostRenderer.enabled = true;

            // Re-enable ghost movement
            GetComponent<PlayerController>().enabled = true;
        }
    }
}*/
