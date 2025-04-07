using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsAnim : MonoBehaviour
{
    public float scaleUpSize;
    public float scaleDuration;
    public float fadeDuration;
    public float moveDuration;
    private Vector3 spawnPosition;
    public float horizontalDistance;
    public float baseVerticalHeight;
    public float randomHeightRange;

    private Vector3 startPosition;

    void Start()
    {
        //Debug.Log("PointsAnim started: " + gameObject.name);

        startPosition = transform.position;

        Animation();
    }
    public void Animation()
    {
        Debug.Log("Animation started: " + gameObject.name);
        MoveAlongCurveWithRandomHeight();
        // LeanTween.alphaVertex(gameObject, 1, fadeDuration).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.scale(gameObject, new Vector3(scaleUpSize, scaleUpSize, 0.003f), scaleDuration).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.scale(gameObject, new Vector3(0f, 0f, 0.003f), scaleDuration).setDelay(0.3f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.alpha(gameObject, 0, fadeDuration).setDelay(scaleDuration).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    void MoveAlongCurveWithRandomHeight()
    {
        // Randomize the vertical height within the specified range
        float verticalHeight = baseVerticalHeight + Random.Range(-randomHeightRange, randomHeightRange);
        int direction = Random.Range(0, 2) * 2 - 1;

        // Move along a curve
        LeanTween.value(gameObject, 0f, 1f, moveDuration)
            .setOnUpdate((float t) =>
            {
                // Calculate the X position (linear interpolation)
                float newX = Mathf.Lerp(startPosition.x, startPosition.x + direction * horizontalDistance, t);

                // Calculate the Y position (parabolic curve: up then down)
                float newY = startPosition.y + verticalHeight * (1 - Mathf.Abs(2 * t - 1)); // Parabola formula

                // Update the object's position
                transform.position = new Vector3(newX, newY, startPosition.z);
            })
            .setEase(LeanTweenType.easeInOutQuad) // Smooth easing for natural feel
            .setOnComplete(() =>
            {
                Debug.Log("Finished the curve movement!");
                // Add additional behavior after completion if needed
            });
    }
}
