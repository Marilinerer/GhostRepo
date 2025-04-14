using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointsSystem : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    public GameObject pointsHolder;

    private void Update()
    {
        pointsText.text = ScoreManager.totalPoints.ToString();
    }

    public void AddPoints(int points)
    {
        LeanTween.scale(pointsHolder, new Vector3(1.2f, 1.2f, 1.2f), 0.3f).setEaseOutElastic().setOnComplete(() =>
        {
            LeanTween.scale(pointsHolder, new Vector3(1f, 1f, 1f), 0.2f).setEaseOutCubic();
        });

        ScoreManager.totalPoints += points;
    }
}
