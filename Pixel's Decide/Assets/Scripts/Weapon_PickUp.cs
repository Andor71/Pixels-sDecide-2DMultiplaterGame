using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Weapon_PickUp : MonoBehaviourPunCallbacks
{
    public GameObject[] weaponsPrefabs;
    Collider2D collider2DCharacter;
    Character_Attack character_Attack;
    PhotonView view;
    
    public float droppedTimer = 2f;
    float droppedTimerR = 0f;
    public bool alreadyHasWeapon = false;

    float timerAxe;
    float timerSpear;
    float timerSword;

    int index;

    void Start()
    {

        view = GetComponent<PhotonView>();
        collider2DCharacter = GetComponent<Collider2D>();
        character_Attack = GetComponent<Character_Attack>();

    }

    void Update()
    {

        pickUptimer();

        ///Dropp
        if(view.IsMine){
            if(Input.GetKeyDown(KeyCode.X))
            {
                if(alreadyHasWeapon)
                {
                    character_Attack.updateDamage("None");

                    //If Weapon dropped set its struct values acording to that.
                    weaponsPrefabs[index].SetActive(false);
                    GameObject temp = Instantiate(weaponsPrefabs[index],collider2DCharacter.transform.localPosition,Quaternion.identity);
                    temp.transform.localScale = new Vector3(1,1,1);
                    temp.transform.position += new Vector3(0,.5f,0); 
                    temp.SetActive(true);

                    alreadyHasWeapon = false;
            
                    switch(temp.name)
                    {
                        case "axe(Clone)":
                            timerAxe = 1f;
                        break;

                        case "sword(Clone)":
                            timerSword = 1f;
                        break;

                        case "spear(Clone)":
                            timerSpear = 1f;
                        break;
                        default:
                        break;
                    }
                }
            }
        }
        
    }
    void pickUptimer()
    {
        timerSpear -=Time.deltaTime;
        timerAxe -=Time.deltaTime;
        timerSword -=Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(view.IsMine){
            if(other.gameObject.CompareTag("Weapon"))
            {
                if(!alreadyHasWeapon)
                {
                    index = getIndexOfTriggeredWeapon(other.gameObject.name);

                    switch(weaponsPrefabs[index].name)
                    {
                        case "axe":
                            if(timerAxe < 0){
                                pickUpWeapon(other.gameObject);
                            }
                        break;

                        case "sword":
                            if(timerSword < 0){
                                pickUpWeapon(other.gameObject);
                            }
                        break;

                        case "spear":
                            if(timerSpear < 0){
                                pickUpWeapon(other.gameObject);
                            }
                        break;
                    }
                
                }
            }
        }
    }

    void pickUpWeapon(GameObject weaponToPickUp)
    {
        Destroy(weaponToPickUp.gameObject);

        weaponsPrefabs[index].SetActive(true);

        alreadyHasWeapon = true;

        character_Attack.updateDamage(weaponToPickUp.gameObject.name);

    }

    int getIndexOfTriggeredWeapon(string triggeredWeaponname)
    {
        for(int i = 0 ; i < weaponsPrefabs.Length ; i++)
        {
            if(weaponsPrefabs[i].name == triggeredWeaponname || weaponsPrefabs[i].name+"(Clone)" == triggeredWeaponname)
            {
                return i;
            }
        }
        return -1;
    }
}
