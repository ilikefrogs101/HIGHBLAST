using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class multizoom : MonoBehaviourPunCallbacks
{
    public Animator animator;

    public GameObject scopeoverlay;

    private bool IsScoped = false;

    public GameObject Weaponcamera;
    public GameObject cursor;

    public Camera maincamera;

    public float scopedfov = 15;
    private float normalfov;

    void Update ()
    {
        if (!photonView.IsMine) return;

        if (Input.GetButtonDown("Fire2"))
        {
            IsScoped = !IsScoped;
            animator.SetBool("isScoped", IsScoped);

            if (IsScoped)
                StartCoroutine(OnScoped());
            else
                OnUnsoped();
        }
    }

    IEnumerator OnScoped ()
    {
        yield return new WaitForSeconds(.15f);

        scopeoverlay.SetActive(true);
        Weaponcamera.SetActive(false);
        cursor.SetActive(false);
        normalfov = maincamera.fieldOfView;
        maincamera.fieldOfView = scopedfov;

    }

    void OnUnsoped ()
    {
        Weaponcamera.SetActive(true);
        scopeoverlay.SetActive(false);
        cursor.SetActive(true);
        maincamera.fieldOfView = normalfov;
    }
}
