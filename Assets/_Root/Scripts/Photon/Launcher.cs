using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Photon
{

    public class Launcher : MonoBehaviourPunCallbacks
    {
        [SerializeField] public Button photonLogInOutButton;
        [SerializeField] public Text answer;
        string gameVersion = "1";
        void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        void Start()
        {
            DisconnectedSign();
            photonLogInOutButton.onClick.AddListener(() => Connect());
            if (PhotonNetwork.IsConnected)
            {
                photonLogInOutButton.onClick.AddListener(() => Disconnect());
            }
        }
        public void Connect()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }

        }
        public void Disconnect()
        {
            DisconnectedSign();
            PhotonNetwork.Disconnect();


        }
        public override void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaster() was called byPUN");
            ConnectedSign();
        }
        public void ConnectedSign()
        {
            answer.text = "connected";
            answer.color = Color.green;
        }
        public void DisconnectedSign()
        {
            answer.text = "Disconnect";
            answer.color = Color.red;
        }
    }
}