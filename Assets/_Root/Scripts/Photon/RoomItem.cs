using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;


public class RoomItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _roomName;
    [SerializeField] private Button _joinRoomButton;
    [SerializeField] private LobbyManager _lobbyManager;
    private void Start()
    {
        _lobbyManager = FindObjectOfType<LobbyManager>();
        _joinRoomButton.onClick.AddListener(OnClickItem);
    }

    public void SetRoomName(string roomName)
    {
        _roomName.text = roomName;
    }

    public void OnClickItem()
    {
        _lobbyManager.JoinRoom(_roomName.text);
    }
   }
