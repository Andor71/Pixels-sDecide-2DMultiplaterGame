using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public GameObject spawnPoint;
    Camera_Script camera_Script;
    GameObject characterToSpawn;
    
    void Start()
    {
        camera_Script = GameObject.Find("PlayerCamera").GetComponent<Camera_Script>();   
    }

    public void SpawnPlayerPrefab(string prefabName)
    {
        GameObject player = PhotonNetwork.Instantiate(getPrefabByName(prefabName).name,spawnPoint.transform.position,Quaternion.identity) as GameObject;
        //Mock
        if(player == null){
            player = GameObject.Find("Normal");
        }
        camera_Script.getPLayer(player.transform);
    }

    GameObject getPrefabByName(string cName){
        foreach(GameObject character in characterPrefabs){
            if(character.name == cName){
                return character;
            }
        }
        return characterPrefabs[0];
    }
    
}
