using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Selecter : MonoBehaviour
{
    public GameObject[] chracterPrefabs;
    int index = 0;

    void Start()
    {
        PlayerPrefs.SetString("CharacterTypeIndex","Normal");
    }
    
    public void NextCharacter(int arrowIndex)
    {
        Debug.Log(arrowIndex);
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
        PlayerPrefs.SetString("CharacterTypeIndex",chracterPrefabs[index].name);
    }
}
