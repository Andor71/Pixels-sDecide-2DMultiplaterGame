using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    public GameObject spawnPoint;

    void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab.name,spawnPoint.transform.position,Quaternion.identity);
    }
    
}
