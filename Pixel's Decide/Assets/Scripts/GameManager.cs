using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public ParticleSystem particlesystem;
    Character_Health character_Health;
    UI uI;
    GameObject gate;
    GameObject lose; 
    GameObject win;
    int activePlayers;
    PhotonView view;


    void Start()
    {
        uI = GameObject.Find("UI").GetComponent<UI>();
        particlesystem = GameObject.Find("ArrowRain").GetComponent<ParticleSystem>();
        particlesystem.Stop();
        gate = GameObject.Find("Gates");
        lose = GameObject.Find("Lose");
        lose.SetActive(false);
        win = GameObject.Find("Win");
        win.SetActive(false);
        view =  gate.GetComponent<PhotonView>();
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
        activePlayers = PhotonNetwork.CurrentRoom.PlayerCount;
        if(view.Owner.IsMasterClient){
            PhotonNetwork.Destroy(gate);
        }
    }
    public void SomeoneDied()
    {
        activePlayers--;
        if(activePlayers == 1){
            SomeoneWon();
        }else{
            StartCoroutine(LeaveOnDie());
        }
        
    }
    public IEnumerator LeaveOnDie(){
        yield return new WaitForSeconds(5f);
        LeaveRoom();
    }
    public void SomeoneWon()
    {
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<PhotonView>().IsMine){
            if(character_Health == null){
                character_Health = GameObject.FindGameObjectWithTag("Player").GetComponent<Character_Health>();
            }
            if(character_Health.stilAlive){
                SetWin();
            }else{
                SetLose();
            }
        }
        StartCoroutine(LeaveOnDie());
    }

    public void LeaveRoom()
    {
        SceneManager.LoadScene("Lobby");
        PhotonNetwork.LeaveRoom();
        uI.UpdatePlayerCounter();
        uI.OnLeaveRoomSetActiveCharacterPanel();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

}
