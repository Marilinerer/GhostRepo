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

    void Start()
    {
        generator = FindObjectOfType<PointsPopGenerator>().GetComponent<PointsPopGenerator>();
        pointsSystem = FindObjectOfType<PointsSystem>().GetComponent<PointsSystem>();
        if (pointsSystem == null)
        {
            Debug.Log("Cannot find points system");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            pointsSystem.AddPoints(pointsToAdd);
            generator.PointsPopUp(collision.transform.position, pointsToAdd.ToString());
        }
    }
}
