using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class singleplayergun : MonoBehaviour
{
    public int damage = 10;

    public Text ammodisplay;
    
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
        if (isRealoading)
            return;

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
            ammodisplay.GetComponent<Text>().text = currentAmmo.ToString ();
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

        RaycastHit Hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out Hit, range))
        {
            Debug.Log(Hit.transform.name);
            
            if( Hit.collider.CompareTag("boss") )
            {
                bosshealth attack = Hit.transform.GetComponent<bosshealth>();
                if (attack != null)
                {
                    attack.TakeDamage(damage);
                }
            }
        
            
            if( Hit.collider.CompareTag("Enemy") )
            {
                {
                    enemyhealth attack = Hit.transform.GetComponent<enemyhealth>();
                    if (attack != null)
                    {
                        attack.TakeDamage(damage);
                    }
                }
            }
        }

        if (Hit.rigidbody != null)
        {
            Hit.rigidbody.AddForce(-Hit.normal * ImpactForce);
        }

        {
            player.AddForce(-fpsCam.transform.forward * recoil);
        }

        GameObject impactGO = Instantiate(ImpactEffect, Hit.point, Quaternion.LookRotation(Hit.normal));

        GameObject BulletholeGO = Instantiate(Bullethole, Hit.point, Quaternion.LookRotation(Hit.normal));
    }
}