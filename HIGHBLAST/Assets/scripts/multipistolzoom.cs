using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class multipistolzoom : MonoBehaviourPunCallbacks
{
    public Animator animator;

    private bool IsScoped = false;

    void Update ()
    {
        if (!photonView.IsMine) return;

        if (Input.GetButtonDown("Fire2"))
        {
            IsScoped = !IsScoped;
            animator.SetBool("isScoped", IsScoped);

            if (IsScoped)
                scope();
            else
                OnUnsoped();
        }
    }

    void scope()
    {
        animator.SetBool("isScoped", IsScoped);
    }

    void OnUnsoped()
    {
        animator.SetBool("isScoped", IsScoped);
    }
}
