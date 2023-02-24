using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Photon
{

    public class Launcher : MonoBehaviourPunCallbacks
    {
        [SerializeField] public Button photonLogInButton;
        [SerializeField] public Button photonLogOutButton;
        [SerializeField] public Text logInAnswer;
        [SerializeField] public Text logOutAnswer;
        string gameVersion = "1";
        void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        void Start()
        {
            
           
                photonLogInButton.onClick.AddListener(() => Connect());
          
                photonLogOutButton.onClick.AddListener(() => Disconnect());
            
        }
        public void Connect()
        {
            if(PhotonNetwork.IsConnected == false)
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
                ConnectedSign();
            }
            else
            {
                Debug.Log("You have already been connected");
            }

        }
        public void Disconnect()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.Disconnect();
                Debug.Log("out");
                DisconnectedSign();
            }
            else
            {
                Debug.Log("try to connect first");
            }

        }
        public override void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaster() was called byPUN");
        }
        public void ConnectedSign()
        {
            if (PhotonNetwork.IsConnected)
            {
                logInAnswer.text = "connected";
                logInAnswer.color = Color.green;
            }
        }
        public void DisconnectedSign()
        {
            
                logOutAnswer.text = "Disconnect";
                logOutAnswer.color = Color.red;
            
        }
    }
}