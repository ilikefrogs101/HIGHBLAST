using UnityEngine;
using System.Collections;

public class singlesword : MonoBehaviour
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

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * ImpactForce);
            }
        }
    }
}