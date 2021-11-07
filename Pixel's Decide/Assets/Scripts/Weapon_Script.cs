using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Weapon_Script : MonoBehaviourPunCallbacks
{

    Character_Attack character_Attack;

    public int damage;
    public bool isPickedUp = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(isPickedUp){

            if(other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetPhotonView().RPC("gotHit",RpcTarget.All,damage);
                //other.gameObject.GetComponent<Character_Health>().gotHit(damage);
            }
        }
    }
}
