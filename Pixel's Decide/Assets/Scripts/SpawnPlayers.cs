using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public GameObject spawnPoint;

    Camera_Script camera_Script;
    string cName;
    GameObject characterToSpawn;
    void Start()
    {
        cName = PlayerPrefs.GetString("CharacterTypeIndex","Normal");
        camera_Script = GameObject.Find("PlayerCamera").GetComponent<Camera_Script>();
        GameObject player = PhotonNetwork.Instantiate(getPrefabByName(cName).name,spawnPoint.transform.position,Quaternion.identity) as GameObject;

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
