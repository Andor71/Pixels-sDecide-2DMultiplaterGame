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
    GameObject leaveBt;
    Text playerCounter;
    Text counter;
    GameObject StartGametext;
    GameObject hurryUp;
    PhotonView view;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        characterSelecterPanel = GameObject.Find("CharacterSelect");
        playerCounter = GameObject.Find("PlayerCounter").GetComponent<Text>();
        counter = GameObject.Find("Counter").GetComponent<Text>();
        hurryUp = GameObject.Find("Hurry Up");
        hurryUp.SetActive(false);
        leaveBt = GameObject.Find("LeaveBt");
        StartGametext = GameObject.Find("StartGameText");
        StartGametext.SetActive(false);
    }

    public void OnCLickLeaveRoom()
    {
        gameManager.LeaveRoom();
        characterSelecterPanel.SetActive(true);
    }

    public void OnLeaveRoomSetActiveCharacterPanel(){
        characterSelecterPanel.SetActive(true);
    }

    public void UpdatePlayerCounter()
    {
        playerCounter.text = "players " + PhotonNetwork.CurrentRoom.PlayerCount + "-" + PhotonNetwork.CurrentRoom.MaxPlayers;
    }

    public void CheckIfRoomIsFull()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers){
            leaveBt.SetActive(false);
            StartCoroutine(Counter());
        }
    }

    public void ActivateHurryUpMessage()
    {
        hurryUp.SetActive(true);
    }

    public void DeActivateHurryUpMessage()
    {
        hurryUp.SetActive(false);
    }

    public IEnumerator Counter()
    {
        StartGametext.SetActive(true);
        yield return new WaitForSeconds(5f);
        StartGametext.SetActive(false);
        counter.gameObject.SetActive(true);
           
        for(int i = 3 ; i > -1 ; i--)
        {
            yield return new WaitForSeconds(1.5f);
            counter.text = i.ToString();
        }
        counter.text = "GO!";
        yield return new WaitForSeconds(2f);
        counter.gameObject.SetActive(false);
        gameManager.StartLevel();
    }

}
