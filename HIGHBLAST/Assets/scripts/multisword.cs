using UnityEngine;
using System.Collections;
using Photon.Pun;

public class multisword : MonoBehaviourPunCallbacks
{
    public int damage = 10;
    public Animator animator;
    public float range = 100f;
    public float fireRate = 15;
    public float ImpactForce = 30f;

    private float timeStamp;
    private bool attacking = false;

    public Camera fpsCam;

    private float nextTimetoFire = 0f;

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimetoFire)
        {
            nextTimetoFire = Time.time + 1f / fireRate;
            attacking = !attacking;
            animator.SetBool("swordattacking", attacking);
            swing();
        }
    }

    void swing()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target attack = hit.transform.GetComponent<Target>();
            if (attack != null)
            {
                attack.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * ImpactForce);
            }
        }
    }
}