using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessDetect : MonoBehaviour
{
    private IPossessable nearbyPossessable;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object has IPossessable
        IPossessable possessable = collision.GetComponent<IPossessable>();
        if (possessable != null)
        {
            nearbyPossessable = possessable; // Store reference
        }

        SpriteRenderer sr = collision.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Material mat = sr.material;
            mat.SetFloat("_OutlineThickness", 0.05f);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // Remove reference when player exits range
        IPossessable possessable = collision.GetComponent<IPossessable>();
        if (possessable != null && nearbyPossessable == possessable)
        {
            nearbyPossessable = null;
        }
        SpriteRenderer sr = collision.GetComponent<SpriteRenderer>();
        Material mat = sr.material;
        mat.SetFloat("_OutlineThickness", 0);
    }

    void Update()
    {
        // Allow possession trigger at any time while in range
        if (nearbyPossessable != null && Input.GetKeyDown(KeyCode.E))
        {
            nearbyPossessable.OnPossess();
        }
    }
}
