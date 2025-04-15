using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCCounter : MonoBehaviour
{
    public static NPCCounter Instance { get; private set; }
    private int npcCounter = 0;
    private int carCrashCounter = 0;

    public TextMeshProUGUI npcCountText;
    public TextMeshProUGUI carCrashCountText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        npcCounter = 0;
        carCrashCounter = 0;

        npcCountText.text = npcCounter.ToString();
        carCrashCountText.text = carCrashCounter.ToString();
    }

    public void NPCReachedPoint2()
    {
        npcCounter++; // Increase counter
        Debug.Log("NPCs reached point2: " + npcCounter);

        // Update UI text
        npcCountText.text = npcCounter.ToString();
    }

    public void CarCrashed()
    {
        carCrashCounter++; // Increase counter
        Debug.Log("CarCrashed: " + carCrashCounter);

        // Update UI text
        npcCountText.text = carCrashCounter.ToString();
    }
}
