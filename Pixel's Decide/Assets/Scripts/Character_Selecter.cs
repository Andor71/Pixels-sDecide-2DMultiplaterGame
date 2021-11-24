using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Selecter : MonoBehaviour
{
    public GameObject[] chracterPrefabs;
    GameObject characterSelecterPanel;
    SpawnPlayers spawnPlayers;
    int index = 0;

    void Start()
    {
        characterSelecterPanel = GameObject.Find("CharacterSelect");
        spawnPlayers = GameObject.Find("SpawnPlayers").GetComponent<SpawnPlayers>();
    }

    public void NextCharacter(int arrowIndex)
    {
        chracterPrefabs[index].SetActive(false);
        //0 Left Arrow , 1 Right Arrow
        if(arrowIndex == 0)
        {
            index --;
        }
        if(arrowIndex == 1){
            index ++;
        }
        if(index < 0)
        {
            index = chracterPrefabs.Length-1;
        }   
        if(index > chracterPrefabs.Length-1)
        {   
            index = 0;
        }
        chracterPrefabs[index].SetActive(true);
    }

    public void OnClickReady()
    {
        characterSelecterPanel.SetActive(false);
        spawnPlayers.SpawnPlayerPrefab(chracterPrefabs[index].name);

    }
}
