using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointsSystem : MonoBehaviour
{
    private static int totalPoints;
    public TextMeshProUGUI pointsText;
    public GameObject pointsHolder;

    private void Start()
    {
        totalPoints = 0;
    }

    private void Update()
    {
        pointsText.text = totalPoints.ToString();
    }

    public void AddPoints(int points)
    {
        LeanTween.scale(pointsHolder, new Vector3(1.2f, 1.2f, 1.2f), 0.3f).setEaseOutElastic().setOnComplete(() =>
        {
            LeanTween.scale(pointsHolder, new Vector3(1f, 1f, 1f), 0.2f).setEaseOutCubic();
        });
        totalPoints += points;
        Update();
    }
}
