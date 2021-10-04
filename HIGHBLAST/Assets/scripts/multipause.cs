using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class multipause : MonoBehaviourPunCallbacks
{
    public string mainMenuScene;
    public GameObject pauseMenu;
    public bool isPaused;
    public GameObject crosshair;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
                crosshair.SetActive(true);
            }
            else
            {
                isPaused = true;
                pauseMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 1;
                crosshair.SetActive(false);
            }
        }
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        Debug.Log("Resuming");
    }

    public void Main()
    {
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1;
        Debug.Log("main");
    }

    public void Respawn()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        photonView.RPC("Die", RpcTarget.All);
        crosshair.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1;
        Debug.Log("quiting");
    }

    public void DisconnectPlayer()
    {
        StartCoroutine(DisconnectAndLoad());
        Debug.Log("leaving...");
    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;
        SceneManager.LoadScene(0);
        Debug.Log("left");
    }
}