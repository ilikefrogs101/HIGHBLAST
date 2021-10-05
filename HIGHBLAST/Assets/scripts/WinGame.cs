using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    public GameObject WinMenu;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Target")
        {
            WinMenu.SetActive(true);
            Debug.Log("you win!");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }
}

