using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckPoints : MonoBehaviour
{
    private PointsSystem pointsSystem;
    public int pointsToAdd;
    public Canvas pointsPop;

    void Start()
    {
        pointsSystem = FindObjectOfType<PointsSystem>().GetComponent<PointsSystem>();
        if (pointsSystem == null)
        {
            Debug.Log("Cannot find points system");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            pointsSystem.AddPoints(pointsToAdd);
            Instantiate(pointsPop, collision.transform.position, Quaternion.identity);
        }
    }
}
