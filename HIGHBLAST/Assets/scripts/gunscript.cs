using UnityEngine;
using System.Collections;
using Photon.Pun;
using UnityEngine.UI;

public class gunscript : MonoBehaviourPunCallbacks
{
    public int damage = 10;
    public float range = 100f;
    public float fireRate = 15;
    public float ImpactForce = 30f;
    public float recoil = 30f;

    public int maxAmmo = 10;
    public int currentAmmo;
    public float reloadTime = 1f;
    private bool isRealoading = false;

    public Rigidbody player;

    private float timeStamp;

    public Text ammodisplay;

    public Camera fpsCam;
    public ParticleSystem MuzzleFlash;
    public GameObject ImpactEffect; 
    public GameObject Bullethole;

    private float nextTimetoFire = 0f;

    public Animator animator;
    
    void Start ()
    {
        currentAmmo = maxAmmo;
    }

    void OnEnable ()
    {
        isRealoading = false;
        animator.SetBool("Reloading", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;

        if (isRealoading)
            return;

        ammodisplay.GetComponent<Text>().text = currentAmmo.ToString();

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetKey(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimetoFire)
        {
            nextTimetoFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    IEnumerator Reload ()
    {
        isRealoading = true;
        Debug.Log ("Reloading...");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        isRealoading = false;
    }

    void Shoot()
    {
        MuzzleFlash.Play();

        currentAmmo--;

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

            {
                player.AddForce(-fpsCam.transform.forward * recoil);
            }

            GameObject impactGO = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));

            GameObject BulletholeGO = Instantiate(Bullethole, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}