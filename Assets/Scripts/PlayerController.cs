using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;          // Movement speed
    public float acceleration = 5f;  // Speed of acceleration
    public float deceleration = 5f;  // Speed of deceleration

    private Rigidbody2D rb;
    private Vector2 currentVelocity; // Smooth velocity to apply

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Get target velocity from input
        Vector2 targetVelocity = GetInput().normalized * speed;

        // Gradually move toward the target velocity
        currentVelocity = Vector2.MoveTowards(
            currentVelocity,
            targetVelocity,
            (targetVelocity.magnitude > 0 ? acceleration : deceleration) * Time.deltaTime
        );

        // Apply the smoothed velocity to the Rigidbody
        rb.velocity = currentVelocity;
    }

    private Vector2 GetInput()
    {
        float x = 0f;
        float y = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) y = 1f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) y = -1f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) x = -1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) x = 1f;

        return new Vector2(x, y);
    }
}

