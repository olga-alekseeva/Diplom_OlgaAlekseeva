using UnityEngine;
using Photon.Pun;
using UnityEditor.PackageManager;

public class Launcher : MonoBehaviourPunCallbacks
{ 
    /// <summary> /// This client's version number.
    /// Users are separated from each other by gameVersion (which allows you to make breaking changes).
    /// </summary> 
     string gameVersion = "1"; 
        /// <summary> 
        /// MonoBehaviour method called on GameObject by Unity during early initialization phase. 
        /// </summary>
    void Awake() 
    {
        // #Critical // this makes sure we can use PhotonNetwork.LoadLevel() on the master client
        // and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    ///<summary> 
    //////MonoBehaviour method called on GameObject byUnity during initialization phase. 
    //////</summary> 
    void Start() 
    {
        Connect(); 
    }
    ///<summary> 
    ///Start the connection process. 
    ///- If already connected, we attempt joining arandom room 
    ///- if not yet connected, Connect this applicationinstance to Photon Cloud Network 
    ///</summary> 
    public void Connect() 
    {
        // we check if we are connected or not, we joinif we are , else we initiate the connection to the server.
        if(PhotonNetwork.IsConnected) 
        {
            // #Critical we need at this point to attemptjoining a Random Room.
            // If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
            PhotonNetwork.JoinRandomRoom(); 
        }
        else 
        {
            // #Critical, we must first and foremostconnect to Photon Online Server.
            PhotonNetwork.ConnectUsingSettings(); 
            PhotonNetwork.GameVersion = gameVersion; 
        }

    }
    public override void OnConnectedToMaster() 
    {
        Debug.Log("OnConnectedToMaster() was called byPUN"); 
    }
}