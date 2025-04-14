using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarMovement : MonoBehaviour, IPossessable
{
    //public Vector2 point1;
    private NPC npc;
    private Rigidbody2D rb;
    public SpriteRenderer sr;
    public Vector2 movementDirection;
    public bool isPossessed = false;
    //private float pauseTime = 1f;
    //private float pauseTimer = 0f;
    //private bool isCooldown = false;
    //public float cooldownTime = 3f;
    private bool stopMovement = false;
    public LayerMask ignoreLayer;
    public BoxCollider2D parentBC;
    public bool isGameOver = false;
    private HeartManager heartManager;

    public bool applySineMovement;
    private float mag;
    private float freq;
    private AudioManager audioManager;
    public Material outlineMat;
    public Material defaultMat;

    void Start()
    {
        npc = GetComponent<NPC>();
        //isCooldown = false;

        rb = GetComponent<Rigidbody2D>();

        mag = Random.Range(2f, 11f);
        freq = Random.Range(2f, 13f);
        heartManager = FindObjectOfType<HeartManager>().GetComponent<HeartManager>();
        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
    }

    void Update()
    {
        if (!stopMovement)
        {

            Vector2 v = rb.velocity;

            if (applySineMovement)
            {
                v.y = mag * Mathf.Sin(freq * Time.time);
            }
            else
            {
                v.y = 0;
            }

            if (isPossessed)
            {
                v.x = 0;

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
                    v.x = -npc.carSpeed; // Move left
                    isPossessed = false;
                    //Debug.Log("Possessed, changed to left");
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    sr.flipX = false;
                    v.x = npc.carSpeed; // Move right
                    isPossessed = false;
                    //Debug.Log("Possessed, changed to right");
                }
            }
            else
            {
                isPossessed = false;
                v.x = sr.flipX ? -npc.carSpeed : npc.carSpeed;
            }

            rb.velocity = v;

            /*else if ((Vector2)transform.position != point1)
            {
                transform.position = Vector2.MoveTowards(transform.position, point1, npc.carSpeed * Time.deltaTime);
            }

            if ((Vector2)transform.position == point1)
            {
                Destroy(gameObject);
            }*/
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
            audioManager.PlaySFXByIndex(6); // oof sfx
            audioManager.PlaySFXByIndex(2); // car crash sfx
            print("health down");
            Destroy(collision.gameObject);
            NPCCounter.Instance.CarCrashed();
            HeartManager.health--;
            heartManager.LoseHeart();

        }

        if (collision.gameObject.CompareTag("ObjCollider") && parentBC.IsTouching(collision))
        {
            if (((1 << collision.gameObject.layer) & ignoreLayer.value) != 0) return;

            stopMovement = true;
            //Debug.Log("destroyed, collided with " + collision.gameObject.name);
            audioManager.PlaySFXByIndex(1); // car hit sfx
            NPCCounter.Instance.CarCrashed();
            StartCoroutine(DelayDestroy(0.5f));

        }

        if (collision.gameObject.CompareTag("ObjRadius"))
        {
            audioManager.PlaySFXByIndex(0); // car horn sfx
            stopMovement = true;
        }

        if (collision.gameObject.CompareTag("CarBorder"))
        {
            //Debug.Log("destroyed, collided with " + collision.gameObject.name);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("PlayerDetector"))
        {
            sr.material = outlineMat;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (((1 << collision.gameObject.layer) & ignoreLayer.value) != 0) return;
        if (collision.gameObject.CompareTag("ObjRadius") || collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DelayResume(0.5f));
        }

        if (collision.gameObject.CompareTag("PlayerDetector"))
        {
            sr.material = defaultMat;
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

