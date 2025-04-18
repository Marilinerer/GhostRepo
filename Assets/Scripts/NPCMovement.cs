using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Vector2 point1;
    public Vector2 point2;
    private NPC npc;
    private Animator animator;

    void Start()
    {
        StartCoroutine(CrossRoad());
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    IEnumerator CrossRoad()
    {
        npc = FindObjectOfType<NPC>().GetComponent<NPC>();

        while (true)
        {
            // Move to point1
            while ((Vector2)transform.position != point1)
            {
                transform.position = Vector2.MoveTowards(transform.position, point1, npc.npcSpeed * Time.deltaTime);
                yield return null;
            }

            animator.SetBool("isStill", true);
            yield return new WaitForSeconds(Random.Range(0.5f, 3f));

            // Move to point2
            while ((Vector2)transform.position != point2)
            {
                transform.position = Vector2.MoveTowards(transform.position, point2, npc.npcSpeed * Time.deltaTime);
                animator.SetBool("isDowning", true);

                yield return null;
            }
            NPCCounter.Instance.NPCReachedPoint2();
            Destroy(gameObject);
        }
    }
}
