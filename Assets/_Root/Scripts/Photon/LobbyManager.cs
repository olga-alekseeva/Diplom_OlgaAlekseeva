using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField _roomInputField;
    [SerializeField] private GameObject _lobbyPanel;
    [SerializeField] private GameObject _roomPanel;
    [SerializeField] private TMP_Text _roomName;
    [SerializeField] private Button _createRoomButton;
    [SerializeField] private RoomItem _roomItem;
    [SerializeField] private Transform _contentObject;
    List<RoomItem> roomItemsList = new List<RoomItem>();

    private void Start()
    {
        _createRoomButton.onClick.AddListener(OnClickCreate);
        PhotonNetwork.JoinLobby();
    }

    private void OnClickCreate()
    {
        if (_roomInputField.text.Length >= 1)
        {
            PhotonNetwork.CreateRoom(_roomInputField.text, new RoomOptions() { MaxPlayers = 5 });
        }
    }
    public override void OnJoinedRoom()
    {
        _lobbyPanel.SetActive(false);
        _roomPanel.SetActive(true);
        _roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateRoomList(roomList);
    }

    private void UpdateRoomList(List<RoomInfo> roomList)
    {
        foreach(RoomItem item in roomItemsList)
        {
            Destroy(item.gameObject);
        }
        roomItemsList.Clear();

        foreach(RoomInfo room in roomList)
        {
            RoomItem newRoom = Instantiate(_roomItem, _contentObject);
            newRoom.SetRoomName(room.Name);
            roomItemsList.Add(newRoom);
        }
    }
    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }
}
