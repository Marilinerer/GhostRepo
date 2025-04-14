using UnityEngine;

public class GhostFloat : MonoBehaviour
{
    public float floatSpeed = 1f;         // How fast it floats up and down
    public float floatHeight = 0.5f;      // Max height from original position
    public float driftSpeed = 0.2f;       // Sideways drift speed
    public float driftDistance = 0.3f;    // How far it drifts sideways

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Up and down float motion
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        // Side to side drift motion
        float xOffset = Mathf.Cos(Time.time * driftSpeed) * driftDistance;

        transform.position = startPos + new Vector3(xOffset, yOffset, 0f);
    }
}
