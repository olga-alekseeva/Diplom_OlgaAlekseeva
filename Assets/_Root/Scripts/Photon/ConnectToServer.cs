using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField _usernameInput;
    [SerializeField] private TMP_Text _buttonLabel;
    [SerializeField] private Button _connectButton;

    private void Start()
    {
        _connectButton.onClick.AddListener(OnClickConnect);
    }

    private void OnClickConnect()
    {
        if(_usernameInput.text.Length >= 1)
        {
            PhotonNetwork.NickName = _usernameInput.text;
            _buttonLabel.text = "Connecting...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Lobby");
    }
}
