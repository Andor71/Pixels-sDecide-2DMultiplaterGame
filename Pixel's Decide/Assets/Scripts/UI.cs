using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class UI : MonoBehaviourPunCallbacks
{
    GameManager gameManager;
    GameObject characterSelecterPanel;
    public Text playerCounter;


    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        characterSelecterPanel = GameObject.Find("CharacterSelect");

    }

    public void OnCLickLeaveRoom(){
  
        gameManager.LeaveRoom();
        characterSelecterPanel.SetActive(false);
    }

    public void UpdatePlayerCounter()
    {
        playerCounter.text = "players" + PhotonNetwork.CurrentRoom.PlayerCount + "-" + PhotonNetwork.CurrentRoom.MaxPlayers;
    }
}
