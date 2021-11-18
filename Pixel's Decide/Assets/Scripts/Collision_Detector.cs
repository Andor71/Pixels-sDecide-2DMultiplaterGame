using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Collision_Detector : MonoBehaviourPunCallbacks
{
    Rigidbody2D rb2D;
    PhotonView view;
    Character_Health character_Health_Script;
    Character_Attack character_Attack_Script;
    ArenaScripts arenaScripts;
    Collider2D entrance;
    Camera cameraPlayer;
    Camera cameraArena;
    bool globalDamage;

    GameObject cheeseCake;

    public int damageofSpike = 5; 
    public int spikeForce = 5;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        character_Health_Script = GetComponent<Character_Health>();
        character_Attack_Script = GetComponent<Character_Attack>();
        cameraPlayer = GameObject.Find("PlayerCamera").GetComponent<Camera>();
        cameraArena = GameObject.Find("ArenaCamera").GetComponent<Camera>();
        view = GetComponent<PhotonView>();
        arenaScripts = GameObject.Find("Arena").GetComponent<ArenaScripts>();
        entrance = GameObject.Find("Left").GetComponent<Collider2D>();
        cheeseCake = GameObject.Find("CheeseCake");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Obstacle"))
        {
            character_Health_Script.gotHit(damageofSpike);

            rb2D.velocity = new Vector2(rb2D.velocity.x,spikeForce);
        }
        if(other.gameObject.CompareTag("Heal"))
        {
            GetComponent<Character_Health>().Heal();

            if(cheeseCake.GetPhotonView().Owner != view.Owner){
                cheeseCake.GetPhotonView().TransferOwnership(view.Owner);
            }
            PhotonNetwork.Destroy(other.gameObject);
            
        }
        if(other.gameObject.CompareTag("Entrance")){
        
            if(view.IsMine)
            {
                //photonView.RPC("SetGlobalDamage",RpcTarget.AllBuffered);  
                //globalDamage = true;

                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                {   
                    player.GetPhotonView().RPC("SetGlobalDamage",RpcTarget.AllBuffered);
                }
                character_Health_Script.stayingInArena = true;
                character_Attack_Script.enabled = true;
                arenaScripts.entered = true;
                cameraPlayer.enabled = false;
                cameraArena.enabled = true;
            }
        }
    }

    [PunRPC]
    public void SetGlobalDamage()
    {
        globalDamage = true;
    }
    public bool getGlobalDamage(){
        return globalDamage;
    }
}
