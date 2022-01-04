using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RoomItem : MonoBehaviour
{
    public Text roomName;
    string roomNameInfo;
    LobbyManager manager;

    void Start()
    {
        manager = FindObjectOfType<LobbyManager>();
    }

    public void SetRoom(string _roomName, int playerCount , int MaxPlayers)
    {
        roomNameInfo = _roomName;
        roomName.text = _roomName+":"+playerCount+"-"+ MaxPlayers;
    }

    public void OnCLickItem(){
        manager.JoinRoom(roomNameInfo);
    }
}
