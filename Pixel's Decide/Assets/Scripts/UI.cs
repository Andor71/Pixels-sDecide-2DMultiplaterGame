using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class UI : MonoBehaviourPunCallbacks
{
    GameManager gameManager;

    public Button leaveButton;
    public Text playerCounter;


    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();


    }

    public void OnCLickLeaveRoom(){
        gameManager.LeaveRoom();
        
    }

    public void UpdatePlayerCounter()
    {
        
    }
}
