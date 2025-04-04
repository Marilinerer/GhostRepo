using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPossess : MonoBehaviour, IPossessable
{
    private Animator animator;
    public bool isPossessing;
    public bool isCooldown = false;

    void Start()
    {
        isPossessing = false;
        animator = GetComponent<Animator>();
    }

    public void OnPossess()
    {
        if (isCooldown) return;

        isPossessing = true;
        animator.SetBool("isPossessed", isPossessing);
        Debug.Log("Possessed animation triggered.");

    }

    public void Unpossess() // called after end of possession animation
    {
        isCooldown = true;

        isPossessing = false;
        animator.SetBool("isPossessed", isPossessing);
        Debug.Log("cooldown animation triggered.");
    }

    private void ResetCooldown() // called by animation event at end of cooldown animation
    {
        isCooldown = false;

        isPossessing = false;
        animator.SetBool("isPossessed", isPossessing);
        Debug.Log("Cooldown reset and possessed animation stopped.");

    }
}
