using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dooropen : MonoBehaviour
{
    [SerializeField]
    GameObject door;

    bool IsOpen = false;

    public Animator Door;

    void OnTriggerEnter(Collider col)
    {
        if (!IsOpen)
        {
            IsOpen = true;
            Door.SetBool("isOpen", IsOpen);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (IsOpen)
        {
            {
                IsOpen = false;
                Door.SetBool("isOpen", IsOpen);
            }
        }
    }
}
