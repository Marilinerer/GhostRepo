using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPossession : MonoBehaviour
{
    private GameObject possessedObject;
    private bool isPossessing;
    private SpriteRenderer ghostRenderer; // For ghost visibility

    void Start()
    {
        ghostRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
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
                isPossessing = true;

                // Hide the ghost
                ghostRenderer.enabled = false;

                // Position the ghost inside the possessed object
                transform.position = possessedObject.transform.position;

                // Optional: Disable ghost movement
                GetComponent<PlayerController>().enabled = false;

                break;
            }
        }
    }

    private void ReleasePossession()
    {
        if (possessedObject != null)
        {
            // Set ghost's position to the possessed object's position
            transform.position = possessedObject.transform.position;

            // Detach from the possessed object
            possessedObject = null;
            isPossessing = false;

            // Make the ghost visible again
            ghostRenderer.enabled = true;

            // Optional: Re-enable ghost movement
            GetComponent<PlayerController>().enabled = true;
        }
    }



}
