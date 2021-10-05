using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class mainmenu : MonoBehaviourPunCallbacks
{
public void playgame()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame ()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void multiplayerconnect ()
    {
        SceneManager.LoadScene(1);
    }

    public void menuscreen()
    {
        SceneManager.LoadScene(0);
    }

    public void firstlevel()
    {
        SceneManager.LoadScene(3);
    }

    public void cancel()
    {
        StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;
        SceneManager.LoadScene(0);
    }

    public void multiselectpage()
    {
        SceneManager.LoadScene(4);
    }

    public void privatejoin()
    {
        SceneManager.LoadScene(5);
    }

    IEnumerator multiselect()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;
        SceneManager.LoadScene(0);
    }

    public void joinback()
    {
        StartCoroutine(multiselect());
    }

    public void tutorialrestart()
    {
        SceneManager.LoadScene(6);
        Time.timeScale = 1f;
    }

    public void controlscreen()
    {
        SceneManager.LoadScene(7);
    }

    public void bouncy()
    {
        SceneManager.LoadScene(8);
        Time.timeScale = 1f;
    }

    public void slippery()
    {
        SceneManager.LoadScene(9);
        Time.timeScale = 1f;
    }

    public void boxed()
    {
        SceneManager.LoadScene(10);
        Time.timeScale = 1f;
    }

    public void bossbattle()
    {
        SceneManager.LoadScene(12);
        Time.timeScale = 1f;
    }

    public void fortattack()
    {
        SceneManager.LoadScene(11);
        Time.timeScale = 1f;
    }

    public void singlemode()
    {
        SceneManager.LoadScene(14);
        Time.timeScale = 1f;
    }

    public void nothinghereyet()
    {
        SceneManager.LoadScene(15);
        Time.timeScale = 1f;
    }

    public void waverestart()
    {
        SceneManager.LoadScene(16);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }
}
