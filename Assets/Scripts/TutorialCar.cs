using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialCar : MonoBehaviour, IPossessable
{
    public GameObject tryAgain;
    public bool partOneDone = false;
    public bool partTwoDone = false;
    public bool partThreeDone = false;
    public GameObject person;

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
    private AudioManager audioManager;
    public Material outlineMat;
    public Material defaultMat;

    void Start()
    {
        npc = GetComponent<NPC>();
        //isCooldown = false;

        rb = GetComponent<Rigidbody2D>();

        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        tryAgain.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerDetector"))
        {
            sr.material = outlineMat;
        }

        if (!partOneDone)
        {
            if (collision.gameObject.CompareTag("NPC") && parentBC.IsTouching(collision))
            {

                tryAgain.SetActive(true);
                audioManager.PlaySFXByIndex(6); // oof sfx
                audioManager.PlaySFXByIndex(2); // car crash sfx
                person.SetActive(false);
                sr.enabled = false;

                StartCoroutine(Delay2(0.5f));
            }

            if (collision.gameObject.CompareTag("ObjCollider"))
            {
                if (((1 << collision.gameObject.layer) & ignoreLayer.value) != 0) return;

                audioManager.PlaySFXByIndex(1); // car hit sfx
                tryAgain.SetActive(false);
                sr.enabled = false;

                StartCoroutine(Delay1(0.5f));
            }
        }

        if (partOneDone && !partTwoDone)
        {
            if (collision.gameObject.CompareTag("NPC") && parentBC.IsTouching(collision))
            {
                tryAgain.SetActive(true);
                audioManager.PlaySFXByIndex(6); // oof sfx
                audioManager.PlaySFXByIndex(2); // car crash sfx
                person.SetActive(false);
                sr.enabled = false;

                StartCoroutine(Delay3(0.5f));
            }

            if (collision.gameObject.CompareTag("ObjRadius"))
            {
                stopMovement = true;
                rb.velocity = Vector2.zero;
                Debug.Log("Velocity after stop: " + rb.velocity);
                audioManager.PlaySFXByIndex(0); // car horn sfx
                tryAgain.SetActive(false);
                sr.enabled = true;

                StartCoroutine(Delay4(4));
            }
        }

        if (partTwoDone && !partThreeDone)
        {
            if (collision.gameObject.CompareTag("NPC") && parentBC.IsTouching(collision))
            {
                tryAgain.SetActive(true);
                audioManager.PlaySFXByIndex(6); // oof sfx
                audioManager.PlaySFXByIndex(2); // car crash sfx
                person.SetActive(false);
                sr.enabled = false;

                StartCoroutine(Delay3(0.5f));
            }

            if (sr.flipX == true)
            {
                tryAgain.SetActive(false);

                StartCoroutine(Delay5(0.5f));
            }
        }

    }

    void Update()
    {
        if (!stopMovement)
        {
            Vector2 v = rb.velocity;

            if (isPossessed)
            {
                v.x = 0;

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

        }

        if (stopMovement)
        {
            sr.enabled = true;
            rb.velocity = Vector2.zero;
        }

        if (gameObject.transform.position == new Vector3(-11, -3.4f, transform.position.z))
        {
            sr.flipX = false;
            rb.velocity = new Vector2(npc.carSpeed, 0);
        }
    }

    public void OnPossess()
    {
        isPossessed = true;
    }

    private IEnumerator Delay1(float times)
    {
        yield return new WaitForSeconds(times);

        partOneDone = true;
        audioManager.PlaySFXByIndex(10);
        sr.enabled = true;
        transform.position = new Vector2(-11, -3.4f);
        sr.flipX = false;
        movementDirection = new Vector2(1, 0);
        person.transform.position = new Vector2(0, -3.4f);

    }
    private IEnumerator Delay2(float times)
    {
        yield return new WaitForSeconds(times);

        person.SetActive(true);
        sr.enabled = true;
        transform.position = new Vector2(11, -0.5f);
    }
    private IEnumerator Delay3(float times)
    {
        yield return new WaitForSeconds(times);

        person.SetActive(true);
        sr.enabled = true;
        transform.position = new Vector2(-11, -3.4f);

    }

    private IEnumerator Delay4(float times)
    {
        yield return new WaitForSeconds(times);

        partTwoDone = true;
        audioManager.PlaySFXByIndex(10);
        transform.position = new Vector2(-11, -3.4f);
        rb.velocity = new Vector2(npc.carSpeed, 0);
        stopMovement = false;
    }

    private IEnumerator Delay5(float times)
    {
        yield return new WaitForSeconds(times);

        partThreeDone = true;
        audioManager.PlaySFXByIndex(10);
        stopMovement = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerDetector"))
        {
            sr.material = defaultMat;
        }
    }

    public void RespawnCar()
    {
        if (!partOneDone)
        {
            transform.position = new Vector3(11, -0.5f, transform.position.z);
        }

        else if (partOneDone || partTwoDone)
        {
            transform.position = new Vector3(-11, -3.4f, transform.position.z);
        }
    }
}