using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;

    /*public float acceleration = 5f;
    public float deceleration = 5f;
    
    private Vector2 currentVelocity; // Smooth velocity to apply*/

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontal, vertical, 0);

        // Normalize the movement vector if it's not zero
        if (movement.magnitude > 0)
        {
            movement = movement.normalized;
        }

        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
        }

        animator.SetFloat("xVelocity", Mathf.Abs(movement.x));
        /*float targetMoveX = 0f;
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
        rb.velocity = currentVelocity;*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerWall"))
        {
            LeanTween.scale(gameObject, new Vector3(2.2f, 2.2f, 2.2f), 0.2f).setEaseInOutExpo().setOnComplete(() =>
            {
                LeanTween.scale(gameObject, new Vector3(2f, 2f, 2f), 0.1f).setEaseInOutExpo();
            });
        }
    }

}
