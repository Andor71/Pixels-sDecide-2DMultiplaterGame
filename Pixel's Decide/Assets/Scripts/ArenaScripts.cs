using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaScripts : MonoBehaviour
{
    public GameObject[] imagesPrefabs;
    float timer = 0; 
    float timer2 = 2;
    float timer3 = 4;
    public float timerR = 5f;
    public bool entered = false;
    int index = 0;
    int index2 = 1;
    int index3 = 2;


    List<Dictionary<int,GameObject>> activeImages = new List<Dictionary<int, GameObject>>();
    List<GameObject> images = new List<GameObject>();

    void Start()
    {
        foreach(GameObject obj in imagesPrefabs){
            GameObject temp = Instantiate(obj,new Vector3(0,0,0),Quaternion.identity);
            temp.SetActive(false);
            images.Add(temp);
        }
    }

    void Update()
    {
        if(entered){
            if(Time.time > timer){
                timer = Time.time + timerR;
                images[index].SetActive(false);
                index = RandomIndex();
                images[index].transform.position = CalculatePosition();
                images[index].SetActive(true);
            }
            if(Time.time > timer2){
                timer2 = Time.time + timerR;
                images[index2].SetActive(false);
                index2 = RandomIndex();
                images[index2].transform.position = CalculatePosition();
                images[index2].SetActive(true);
            }

            if(Time.time > timer3){
                timer3 = Time.time + timerR;
                images[index3].SetActive(false);
                index3 = RandomIndex();
                images[index3].transform.position = CalculatePosition();
                images[index3].SetActive(true);
            }

        }
    }

    Vector2 CalculatePosition(){
        int pos5 = Random.Range(0,4);
        float x,y;

        switch(pos5){
            case 0:
                x = Random.Range(25f,27f);
                y = Random.Range(6f,10f);
            break;
            case 1:
                x = Random.Range(30f,33f);
                y = Random.Range(5f,9f);
            break;
            case 2:
                x = Random.Range(37f,41f);
                y = Random.Range(7.0f,5.5f);
            break;

            case 3:
                x = Random.Range(45f,47f);
                y = Random.Range(6,9f);
            break;

            case 4:
                x = Random.Range(52f,53f);
                y = Random.Range(6f,10f);
            break;
            default:
            x = 0;
            y = 0;
            break;
        }
        return new Vector2(x,y+3);
    }

    int RandomIndex(){
        return Random.Range(0,images.Count);
    }
}
