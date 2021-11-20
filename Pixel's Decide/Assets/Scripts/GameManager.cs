using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public ParticleSystem particlesystem;

    void Start()
    {
        particlesystem = GameObject.Find("ArrowRain").GetComponent<ParticleSystem>();
        particlesystem.Stop();
    }

    public void StartArrowRain()
    {
        particlesystem.Play();
    }
    public void StopArrowRain()
    {
        particlesystem.Stop();
    }  
    
    public void LeaveRoom()
    {
        SceneManager.LoadScene("Lobby");
        PhotonNetwork.LeaveRoom();
      
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

}
