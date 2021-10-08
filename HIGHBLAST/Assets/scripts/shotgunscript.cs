using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class shotgunscript : MonoBehaviour
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

    private float timeStamp;

    public Rigidbody player;

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

        int amountOfProjectiles = 8;
        if (Input.GetButton("Fire1") && Time.time >= nextTimetoFire)
        {
            currentAmmo--;

            for (int i = 0; i < amountOfProjectiles; i++)
            {
                nextTimetoFire = Time.time + 1f / fireRate;
                ShotgunRay();
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
    }

    void ShotgunRay()
    {
        MuzzleFlash.Play();

        {
            player.AddForce(-fpsCam.transform.forward * recoil);
        }

        RaycastHit hit;
        Vector3 direction = fpsCam.transform.forward;
        Vector3 spread = Vector3.zero;
        spread += fpsCam.transform.up * Random.Range(-1f, 1f);
        spread += fpsCam.transform.right * Random.Range(-1f, 1f);

        direction += spread.normalized * Random.Range(0f, 0.2f); 

        if (Physics.Raycast(fpsCam.transform.position, direction, out hit, range))
        {
            Debug.Log(hit.transform.name);

            if( hit.collider.CompareTag("Enemy") )
            {
                enemyhealth attack = hit.transform.GetComponent<enemyhealth>();
                if (attack != null)
                {
                    attack.TakeDamage(damage);
                }
            }
            
            if( hit.collider.CompareTag("boss") )
            {
                bosshealth attack = hit.transform.GetComponent<bosshealth>();
                if (attack != null)
                {
                    attack.TakeDamage(damage);
                }
            }
        }
    
        if (hit.rigidbody != null)
        {
            hit.rigidbody.AddForce(-hit.normal * ImpactForce);
        }

        GameObject impactGO = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));

        GameObject BulletholeGO = Instantiate(Bullethole, hit.point, Quaternion.LookRotation(hit.normal));
    }
}