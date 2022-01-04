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
    GameObject hurryUp;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        characterSelecterPanel = GameObject.Find("CharacterSelect");
        playerCounter = GameObject.Find("PlayerCounter").GetComponent<Text>();
        counter = GameObject.Find("Counter").GetComponent<Text>();
        hurryUp = GameObject.Find("Hurry Up");
        leaveBt = GameObject.Find("LeaveBt");
    }

    public void OnCLickLeaveRoom(){
  
        gameManager.LeaveRoom();
        characterSelecterPanel.SetActive(false);
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
        counter.gameObject.SetActive(true);

        for(int i = 3 ; i > -1 ; i--){
            yield return new WaitForSeconds(1.5f);
            counter.text = i.ToString();
            Debug.Log(i);
        }

        yield return new WaitForSeconds(2f);
        counter.gameObject.SetActive(false);
        //StartGame();
    }

}
