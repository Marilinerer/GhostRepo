using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Vector2 point1;
    public Vector2 point2;
    private NPC npc;

    void Start()
    {
        StartCoroutine(CrossRoad());
        //npc = FindObjectOfType<NPC>().GetComponent<NPC>();
    }

    // Update is called once per frame
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
                transform.position = Vector2.MoveTowards(transform.position, point1, npc.NPCspeed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(1.5f);

            // Move to point2
            while ((Vector2)transform.position != point2)
            {
                transform.position = Vector2.MoveTowards(transform.position, point2, npc.NPCspeed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(1.5f);

            Destroy(gameObject);
        }
    }
}
