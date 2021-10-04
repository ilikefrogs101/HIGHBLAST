
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

namespace a.parkour.fps
{
    public class Manager : MonoBehaviourPunCallbacks
    {
        public string player_prefab;

        private void Start()
        {
            Spawn();
        }

        public void Spawn()
        {
            Transform spawnpoint = SpawnManager.Instance.GetSpawnpoint();
            PhotonNetwork.Instantiate(player_prefab, spawnpoint.position, spawnpoint.rotation);
        }

        public void DisconnectPlayer()
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
    }
}