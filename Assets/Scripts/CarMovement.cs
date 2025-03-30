using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public bool moveRight = true; // Set in Inspector (true = right, false = left)
    public float speed = 5f; // Fixed speed

    private bool isPossessed = false; // Track possession

    void Update()
    {
        if (!isPossessed) // Move only if NOT possessed
        {
            float direction = moveRight ? 1f : -1f;
            transform.position += Vector3.right * direction * speed * Time.deltaTime;
        }
    }

    // Called when the ghost possesses the car
    public void Possess()
    {
        if (!isPossessed)
        {
            isPossessed = true;
            speed = 0; // Stop car movement
            Debug.Log("CAR STOPS!!");
        }
    }

    // Called when the ghost leaves the car
    public void Release()
    {
        if (isPossessed)
        {
            isPossessed = false;
            speed = 5f; // Resume movement
            Debug.Log("CAR MOVES AGAIN!!");
        }
    }

    // Called when a collision occurs with the car
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the car collides with the NPC (this will destroy both car and NPC)
        if (collision.gameObject.CompareTag("NPC"))
        {
            Destroy(gameObject); // Destroy car
            Destroy(collision.gameObject); // Destroy NPC
            Debug.Log("DIEEEEE - NPC collision");
        }

        // Optional: Handle collisions with other objects here
        else if (collision.gameObject.CompareTag("OtherObject"))
        {
            // Handle collision with other objects, e.g., apply damage or some effect
            Debug.Log("Collided with OtherObject");
        }
    }
}
