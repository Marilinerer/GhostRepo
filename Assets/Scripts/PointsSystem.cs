using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointsSystem : MonoBehaviour
{
    private static int totalPoints;
    public TextMeshProUGUI pointsText;

    private void Update()
    {
        pointsText.text = "Points: " + totalPoints.ToString();
    }

    public void AddPoints(int points)
    {
        totalPoints += points;
        Update();
    }
}
