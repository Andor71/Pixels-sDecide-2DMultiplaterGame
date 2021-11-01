using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject spawnPoint;

    Camera_Script camera_Script;

    void Start()
    {
        Debug.Log("dwd");
        camera_Script = GameObject.Find("PlayerCamera").GetComponent<Camera_Script>();
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name,spawnPoint.transform.position,Quaternion.identity) as GameObject;
        camera_Script.getPLayer(player.transform);
    }
    
}
