using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace a.parkour.fps
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        public void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            Connect();
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("CONNECTED!");
            Join();

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

        public void Join ()
        {
            PhotonNetwork.JoinRoom("1dc7b949-667c-4ff1-aadd-91e6b5b59ef6");
        }

        public void Create ()
        {
            PhotonNetwork.CreateRoom("1dc7b949-667c-4ff1-aadd-91e6b5b59ef6");
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
