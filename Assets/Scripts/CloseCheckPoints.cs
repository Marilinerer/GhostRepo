using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CloseCheckPoints : MonoBehaviour
{
    private PointsSystem pointsSystem;
    public int pointsToAdd;
    public Canvas pointsPop;
    private PointsPopGenerator generator;
    private bool hasTriggered = false;

    private float timeInsideTrigger = 0f;
    public float requiredStayTime = 1f;

    void Start()
    {
        generator = FindObjectOfType<PointsPopGenerator>().GetComponent<PointsPopGenerator>();
        pointsSystem = FindObjectOfType<PointsSystem>().GetComponent<PointsSystem>();
        if (pointsSystem == null)
        {
            Debug.Log("Cannot find points system");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC") && !hasTriggered)
        {
            timeInsideTrigger += Time.deltaTime; // Increment time

            if (timeInsideTrigger >= requiredStayTime)
            {
                pointsSystem.AddPoints(pointsToAdd);
                generator.PointsPopUpClose(collision.transform.position, pointsToAdd.ToString());
                hasTriggered = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            timeInsideTrigger = 0f;
            hasTriggered = false;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            timeInsideTrigger = 0f;
        }

    }
}
