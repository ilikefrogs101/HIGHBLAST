using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadethrower : MonoBehaviour
{
    public float delay;
    private float delayT;

    public float throwforce = 50f;
    public GameObject Grenade;

    // Update is called once per frame
    void Update()
    {
        if (delayT >= delay)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                delayT = 0;
                ThrowGrenade();
            }
        }
        else
        {
            delayT += Time.deltaTime;
        }
    }

    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(Grenade, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwforce, ForceMode.VelocityChange);
    }
}
