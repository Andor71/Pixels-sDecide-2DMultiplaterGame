using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public InputField roomInputField;
    Text roomName;
    public GameObject createPanel;
    public GameObject characterSelecterPanel;

    public RoomItem roomItemPrefab;

    List<RoomItem> roomItemList = new List<RoomItem>();
    public Transform contentObject;

    public float timeBetweenUpdates = 3f;
    float nextUpdateTime = 0f;

    void Start()
    {
        PhotonNetwork.JoinLobby();
    }

    public void OnClickCreate()
    {
        if(roomInputField.text.Length >= 1 && roomInputField.text.Length <= 10)
        {
            PhotonNetwork.CreateRoom(roomInputField.text, new RoomOptions(){MaxPlayers = 4});
        }
    }

    public override void OnJoinedRoom()
    {
        createPanel.SetActive(false);
        characterSelecterPanel.SetActive(true);
    }

    public void OnClickJoinGameScene()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        
        if(Time.time >= nextUpdateTime){
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timeBetweenUpdates;
        }
    }

    void UpdateRoomList(List<RoomInfo> list)
    {
        foreach (RoomItem item in roomItemList)
        {
            Destroy(item.gameObject);
        }

        roomItemList.Clear();

        foreach (RoomInfo room in list)
        {
            if(room.PlayerCount != 0){

                RoomItem newRoom = Instantiate(roomItemPrefab , contentObject);

                if(room.PlayerCount < room.MaxPlayers && room.PlayerCount > 0)
                {
                    newRoom.SetRoom(room.Name,room.PlayerCount,room.MaxPlayers);
                    roomItemList.Add(newRoom);
                }
            }
        }
    }

    public void JoinRoom(string roomName){
        if(roomName.Length <1 ){
            return;
        }
        PhotonNetwork.JoinRoom(roomName);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        createPanel.SetActive(true);
        characterSelecterPanel.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
}
