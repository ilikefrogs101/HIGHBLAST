using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    public Transform dest;

    public Camera fpsCam;

    public float range = 5;

    private void Update()
    {   
        RaycastHit Hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out Hit, range))
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                GetComponent<Rigidbody>().useGravity = false;
                this.transform.position = dest.position;
                this.transform.parent = GameObject.Find("holding").transform;
            }
        }
    
        if(Input.GetKeyUp(KeyCode.Q))
        {
            GetComponent<Rigidbody>().useGravity = true;
            this.transform.parent = null;
        }
    }
}
