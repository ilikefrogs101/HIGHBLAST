using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.SceneManagement;

namespace a.parkour.fps
{
    public class customlauncher : MonoBehaviourPunCallbacks
    {
        public void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            Connect();
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("CONNECTED!");
            JoinRoom();

            base.OnConnectedToMaster();
        }

        public override void OnJoinedRoom()
        {
            StartGame();

            base.OnJoinedRoom();
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Create();

            base.OnJoinRoomFailed(returnCode, message);
        }
        
        public void Connect ()
        {
            Debug.Log("Trying to connect...");
           PhotonNetwork.GameVersion = "0.0.0";
           PhotonNetwork.ConnectUsingSettings();
        }

        public void JoinRoom ()
        {
            PhotonNetwork.JoinRoom(File.ReadAllText(Path.Combine(Application.dataPath, "..", "server.txt")));
        }

        public void Create ()
        {
            PhotonNetwork.CreateRoom(File.ReadAllText(Path.Combine(Application.dataPath, "..", "server.txt")));
        }

        public void StartGame ()
        {
            if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                PhotonNetwork.LoadLevel(13);
            }
        }
    }
}