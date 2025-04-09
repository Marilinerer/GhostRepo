using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarRandomMovement : MonoBehaviour, IPossessable
{
    public Vector2 point1;
    private NPC npc;
    private Rigidbody2D rb;
    public SpriteRenderer sr;
    public Vector2 movementDirection;
    public Vector2 movementDirection2;
    private bool isPossessed = false;
    //private float pauseTime = 1f;
    //private float pauseTimer = 0f;
    //private bool isCooldown = false;
    //public float cooldownTime = 3f;
    private bool stopMovement = false;
    public LayerMask ignoreLayer;
    public BoxCollider2D parentBC;

    void Start()
    {
        npc = GetComponent<NPC>();
        //isCooldown = false;

        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        if (!stopMovement)
        {
            if (isPossessed)
            {
                rb.velocity = Vector2.zero;

                /*pauseTimer += Time.deltaTime;
                if (pauseTimer >= pauseTime)
                {
                    isPaused = false;
                    pauseTimer = 0f;
                    Debug.Log("Unpaused" + gameObject.name);
                    //StartCooldown();
                }*/

                if (Input.GetKeyDown(KeyCode.A))
                {
                    sr.flipX = true;
                    rb.velocity = movementDirection; // Move left
                    isPossessed = false;
                    //Debug.Log("Possessed, changed to left");
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    sr.flipX = false;
                    rb.velocity = movementDirection2; // Move right
                    isPossessed = false;
                    //Debug.Log("Possessed, changed to right");
                }
            }
            /*else
            {
                isPossessed = false;
                rb.velocity = sr.flipX ? new Vector2(-npc.carSpeed, 0) : new Vector2(npc.carSpeed, 0);
            }*/
            else if ((Vector2)transform.position != point1)
            {
                transform.position = Vector2.MoveTowards(transform.position, point1, npc.carSpeed * Time.deltaTime);
            }

            if ((Vector2)transform.position == point1)
            {
                //Debug.Log("destroyed, reached waypoint");
                Destroy(gameObject);
            }
        }
        if (stopMovement)
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void OnPossess()
    {
        //if (isCooldown) return; // Prevent possession if in cooldown
        //Debug.Log("Possessed" + gameObject.name);

        isPossessed = true;
        //pauseTimer = 0f;
    }

    /*private void StartCooldown()
    {
        isCooldown = true;
        Invoke(nameof(ResetCooldown), cooldownTime); // Automatically reset cooldown after `cooldownTime`
    }

    private void ResetCooldown()
    {
        isCooldown = false;
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC") && parentBC.IsTouching(collision))
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

        if (collision.gameObject.CompareTag("ObjCollider"))
        {
            if (((1 << collision.gameObject.layer) & ignoreLayer.value) != 0) return;

            stopMovement = true;
            //Debug.Log("destroyed, collided with " + collision.gameObject.name);
            StartCoroutine(DelayDestroy(0.5f));

        }

        if (collision.gameObject.CompareTag("ObjRadius"))
        {
            stopMovement = true;
        }

        if (collision.gameObject.CompareTag("CarBorder"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & ignoreLayer.value) != 0) return;
        if (collision.gameObject.CompareTag("ObjRadius"))
        {
            StartCoroutine(DelayResume(1));
        }
    }

    private IEnumerator DelayDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private IEnumerator DelayResume(float delay)
    {
        yield return new WaitForSeconds(delay);
        stopMovement = false;
    }
}

