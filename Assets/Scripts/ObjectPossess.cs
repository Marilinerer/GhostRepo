using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPossess : MonoBehaviour, IPossessable
{
    private Animator animator;
    public bool isPossessing;
    public bool isCooldown = false;
    public Collider2D radiusCol;
    public Material outlineMat;
    public Material defaultMat;
    private SpriteRenderer sr;
    private AudioManager audioManager;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.material = outlineMat;
        isPossessing = false;
        animator = GetComponent<Animator>();
        // radiusCol = GetComponent<Collider2D>();
        radiusCol.enabled = false;
        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
    }

    public void OnPossess()
    {
        if (isCooldown) return;

        audioManager.PlaySFXByIndex(7); // ghost possess sfx
        sr.material = defaultMat;
        isPossessing = true;
        animator.SetBool("isPossessed", isPossessing);
        radiusCol.enabled = true;

        Debug.Log("Possessed animation triggered.");

    }

    public void Unpossess() // called after end of possession animation
    {
        isCooldown = true;

        isPossessing = false;
        animator.SetBool("isPossessed", isPossessing);
        radiusCol.enabled = false;

        Debug.Log("cooldown animation triggered.");
    }

    private void ResetCooldown() // called by animation event at end of cooldown animation
    {
        isCooldown = false;

        isPossessing = false;
        animator.SetBool("isPossessed", isPossessing);
        sr.material = outlineMat;

        Debug.Log("Cooldown reset and possessed animation stopped.");
    }

    private void PlayHydrantSFX() // Played in animation event
    {
        audioManager.PlaySFXByIndex(9); // Hydrant sfx
    }

    private void PlayLampSFX() // Played in animation event
    {
        audioManager.PlaySFXByIndex(5); // Lamp post sfx
    }
}
