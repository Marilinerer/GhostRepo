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
        float targetMoveX = 0f;
        float targetMoveY = 0f;

        // Check for input and set target velocity
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) targetMoveY = 1f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) targetMoveY = -1f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) targetMoveX = -1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) targetMoveX = 1f;

        Vector2 targetVelocity = new Vector2(targetMoveX, targetMoveY).normalized * speed;

        // Gradually move toward the target velocity for smooth acceleration and deceleration
        currentVelocity = Vector2.MoveTowards(currentVelocity, targetVelocity, (targetVelocity.magnitude > 0 ? acceleration : deceleration) * Time.deltaTime);

        // Apply the smoothed velocity to the Rigidbody
        rb.velocity = currentVelocity;
    }
}
