using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_PickUp : MonoBehaviour
{
    public GameObject[] weaponsPrefabs;
    Collider2D collider2DCharacter;

    public float droppedTimer = 2f;
    float droppedTimerR = 0f;
    public bool alreadyHasWeapon = false;

    int index;

    public class Weapon{
        public GameObject weapon;
        public bool delayed;
        public float timer;

        public Weapon(GameObject weapon,bool delayed, float timer)
        {
            this.weapon = weapon;
            this.delayed = delayed;
            this.timer = timer;
        }


        public void setTimer(float timer)
        {
            this.timer = timer;
        }
        public void setDealayed(bool delayed)
        {
            this.delayed = delayed;

        }
        public void CountDownTimer()
        {
            timer -= Time.deltaTime;
        }
        public bool getDelay()
        {
            return delayed;
        }
        public float getTimer()
        {
            return timer;
        }
    }

    public List<Weapon> weapons = new List<Weapon>();
    public List<Weapon> delayedWeapons = new List<Weapon>();

    void Start()
    {
        collider2DCharacter = GetComponent<Collider2D>();

        Debug.Log(weaponsPrefabs.Length);
        for(int i = 0 ; i < weaponsPrefabs.Length ; i++){
            weapons.Add(new Weapon(weaponsPrefabs[i],false,0));
        }
    }

    void Update()
    {
        ///Dropp
        if(Input.GetKeyDown(KeyCode.X))
        {
            if(alreadyHasWeapon)
            {
                //If Weapon dropped set its struct values acording to that.
                weapons[index].weapon.SetActive(false);
                GameObject temp = Instantiate(weapons[index].weapon,collider2DCharacter.transform.localPosition,Quaternion.identity);
                alreadyHasWeapon = false;
                weapons[index].setDealayed(true);
                weapons[index].setTimer(droppedTimer);

                delayedWeapons.Add(weapons[index]);
            }
        }
        
    }

    void delayedTimer(){
        foreach(Weapon weapon in delayedWeapons)
        {
            weapon.CountDownTimer();
            if(weapon.getTimer() <= 0){
                weapon.setDealayed(false);
                delayedWeapons.Remove(weapon);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Weapon"))
        {
            if(!alreadyHasWeapon)
            {
                index = getIndexOfTriggeredWeapon(other.gameObject.name);

                if(!delayedWeapons.Contains(weapons[index])){
                    Destroy(other.gameObject);

                    weapons[index].weapon.SetActive(true);

                    alreadyHasWeapon = true;
                }
            }
        }
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
