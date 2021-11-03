using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class ConnectToServer : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    //This function called when we connected to the Photon server
    public override void OnConnectedToMaster()
    {
        //Create an join rooms
        PhotonNetwork.JoinLobby();
    }

    //Called if we joined to the lobby;
    public override void OnJoinedLobby()
    {
       SceneManager.LoadScene("Lobby");
    }
}
