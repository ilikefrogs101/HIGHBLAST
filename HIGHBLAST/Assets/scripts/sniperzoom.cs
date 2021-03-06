using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sniperzoom : MonoBehaviour
{
    public Animator animator;

    public GameObject scopeoverlay;

    private bool IsScoped = false;

    public GameObject Weaponcamera;
    public GameObject cursor;

    public Camera maincamera;

    public float scopedfov = 15f;
    private float normalfov = 60f;

    void Update ()
    {
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

    IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(.15f);

        scopeoverlay.SetActive(true);
        Weaponcamera.SetActive(false);
        cursor.SetActive(false);
        normalfov = maincamera.fieldOfView;
        maincamera.fieldOfView = scopedfov;
    }

    void OnUnsoped()
    {
        Debug.Log("unzoomed");
        Weaponcamera.SetActive(true);
        scopeoverlay.SetActive(false);
        cursor.SetActive(true);
        maincamera.fieldOfView = normalfov;
    }
}
