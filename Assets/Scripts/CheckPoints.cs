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
    private PointsPopGenerator generator;
    private AudioManager audioManager;
    public bool playDing = false;

    void Start()
    {
        generator = FindObjectOfType<PointsPopGenerator>().GetComponent<PointsPopGenerator>();
        pointsSystem = FindObjectOfType<PointsSystem>().GetComponent<PointsSystem>();
        if (pointsSystem == null)
        {
            Debug.Log("Cannot find points system");
        }
        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            pointsSystem.AddPoints(pointsToAdd);
            generator.PointsPopUp(collision.transform.position, pointsToAdd.ToString());
            if (playDing)
            {
                audioManager.PlaySFXByIndex(10); // ding sfx
            }
        }
    }
}
