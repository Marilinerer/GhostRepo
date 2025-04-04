using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarMovement : MonoBehaviour, IPossessable
{
    public Vector2 point1;
    private NPC npc;
    private bool isPaused = false;
    private float pauseTime = 3f;
    private float pauseTimer = 0f;
    private bool isCooldown = false;
    public float cooldownTime = 3f;
    private bool stopMovement = false;
    public LayerMask ignoreLayer;

    void Start()
    {
        npc = GetComponent<NPC>();
        isCooldown = false;
    }

    void Update()
    {
        if (!stopMovement)
        {
            if (isPaused)
            {
                pauseTimer += Time.deltaTime;
                if (pauseTimer >= pauseTime)
                {
                    isPaused = false;
                    pauseTimer = 0f;
                    StartCooldown();
                }
            }
            else if ((Vector2)transform.position != point1)
            {
                transform.position = Vector2.MoveTowards(transform.position, point1, npc.carSpeed * Time.deltaTime);
            }

            if ((Vector2)transform.position == point1)
            {
                Destroy(gameObject);
            }
        }
    }

    public void OnPossess()
    {
        if (isCooldown) return; // Prevent possession if in cooldown

        isPaused = true;
        pauseTimer = 0f;
    }

    private void StartCooldown()
    {
        isCooldown = true;
        Invoke(nameof(ResetCooldown), cooldownTime); // Automatically reset cooldown after `cooldownTime`
    }

    private void ResetCooldown()
    {
        isCooldown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            print("health down");
            Destroy(collision.gameObject);
            HeartManager.health--;

            if (HeartManager.health <= 0)
            {
                // Handle game over logic here
                Debug.Log("GAME OVER");
            }
        }

        if (((1 << collision.gameObject.layer) & ignoreLayer.value) != 0) return;

        if (collision.gameObject.CompareTag("Possessable"))
        {

            stopMovement = true; // Stop movement when colliding with a possessable object
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & ignoreLayer.value) != 0) return;

        stopMovement = false; // Resume movement when exiting the trigger
    }
}

