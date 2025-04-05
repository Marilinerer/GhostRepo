using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float npcSpeed;
    public float carSpeed;

    private void Start()
    {
        npcSpeed = Random.Range(2, 8);
        carSpeed = Random.Range(3, 8);
    }
}
