using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public ParticleSystem particlesystem;
    UI uI;
    GameObject gate;
    GameObject lose; 
    GameObject win;
    int activePlayers;
    void Start()
    {
        uI = GameObject.Find("UI").GetComponent<UI>();
        particlesystem = GameObject.Find("ArrowRain").GetComponent<ParticleSystem>();
        particlesystem.Stop();
        gate = GameObject.Find("Gates");
        lose = GameObject.Find("Lose");
        win = GameObject.Find("Win");
    }
    public void SetLose()
    {
        lose.SetActive(true);
    }
    public void SetWin()
    {
        win.SetActive(true);
    }

    public void StartArrowRain()
    {
        particlesystem.Play();
    }
    public void StopArrowRain()
    {
        particlesystem.Stop();
    }  

    public void StartLevel()
    {
        gate.SetActive(false);
        activePlayers = PhotonNetwork.CurrentRoom.PlayerCount;
    }
    
    public void SomeoneDied()
    {
        activePlayers--;
        if(activePlayers == 1){
            SomeoneWon();
        }
    }

    public void SomeoneWon()
    {
        if(lose.active != true){
            SetWin();
        }
    }

    public void LeaveRoom()
    {
        SceneManager.LoadScene("Lobby");
        PhotonNetwork.LeaveRoom();
        uI.UpdatePlayerCounter();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

}
