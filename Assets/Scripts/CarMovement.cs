using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public Vector2 point1;
    private NPC npc;
    private bool isPaused = false;
    private float pauseTime = 3f;
    private float pauseTimer = 0f;

    void Start()
    {
        npc = GetComponent<NPC>();
    }

    void Update()
    {
        if (isPaused)
        {
            pauseTimer += Time.deltaTime;
            if (pauseTimer >= pauseTime)
            {
                isPaused = false;
                pauseTimer = 0f;
            }
        }
        else if ((Vector2)transform.position != point1)
        {
            transform.position = Vector2.MoveTowards(transform.position, point1, npc.NPCspeed * Time.deltaTime);
        }

        if ((Vector2)transform.position == point1)
        {
            Destroy(gameObject);
        }
    }

    public void OnPossess()
    {
        isPaused = true;
        pauseTimer = 0f; // Reset the timer
    }

}

