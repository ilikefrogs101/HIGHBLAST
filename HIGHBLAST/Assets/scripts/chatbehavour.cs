using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using System.Text.RegularExpressions;

public class chatbehavour : MonoBehaviourPunCallbacks
{
    public TMP_InputField ChatInput;
    public TextMeshProUGUI ChatContent;
    private PhotonView _photon;
    private List<string> _messages = new List<string>();
    private float _buildDelay = 0f;
    private int _maximumMessages;

    void Awake()
    {
        _photon = GetComponent<PhotonView>();
    }

    [PunRPC]
    void RPC_AddNewMessage(string msg)
    {
        _messages.Add(msg);
    }

    public void SendChat(string msg)
    {
        string NewMessage = PhotonNetwork.NickName +": " + msg;
        if(_photon != null)
        {
            _photon.RPC("RPC_AddNewMessage", RpcTarget.All,NewMessage);
        }
    }

    public void SubmitChat()
    {
        string blankCheck = ChatInput.text;
        blankCheck = Regex.Replace(blankCheck, @"\s", "");
        if (blankCheck == "")
        {
            ChatInput.ActivateInputField();
            ChatInput.text = "";
            return;
        }
    
        SendChat(ChatInput.text);
        ChatInput.DeactivateInputField();
        ChatInput.text = "";
    }

    void BuildChatContents()
    {
        string NewContents = "";
        foreach (string s in _messages)
        {
            NewContents += s + "\n";
        }
        ChatContent.text = NewContents;
    }

    void Update()
    {
        if (!photonView.IsMine) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SubmitChat();
        }

        if (Input.GetKeyDown(KeyCode.Slash))
        {
            ChatInput.ActivateInputField();
        }

        if (PhotonNetwork.InRoom)
        {
            ChatContent.maxVisibleLines = _maximumMessages;

            if (_messages.Count> _maximumMessages)
            {
                _messages.RemoveAt(0);
            }
            if (_buildDelay < Time.time)
            {
                BuildChatContents();
                _buildDelay = Time.time + 0.25f;
            }
        }
        else if (_messages.Count>0)
        {
            _messages.Clear();
            ChatContent.text = "";
        }
    }
}
