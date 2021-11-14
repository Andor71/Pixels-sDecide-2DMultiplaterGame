using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Collision_Detector : MonoBehaviourPunCallbacks ,IPunObservable
{

    PhotonView view;
    Character_Health character_Health_Script;
    Character_Attack character_Attack_Script;
    ArenaScripts arenaScripts;
    public Camera cameraPlayer;
    public Camera cameraArena;
    public bool globalDamage = false;

    public int damageofSpike = 5; 
    void Start()
    {
        character_Health_Script = GetComponent<Character_Health>();
        character_Attack_Script = GetComponent<Character_Attack>();
        cameraPlayer = GameObject.Find("PlayerCamera").GetComponent<Camera>();
        cameraArena = GameObject.Find("ArenaCamera").GetComponent<Camera>();
        view = GetComponent<PhotonView>();
        arenaScripts = GameObject.Find("Arena").GetComponent<ArenaScripts>();

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Spikes"))
        {
            character_Health_Script.gotHit(damageofSpike);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Entrance") && view.IsMine){
            view.RPC("SetGlobalDamage",RpcTarget.All);
            if(view.IsMine)
            {
                character_Attack_Script.enabled = true;
                arenaScripts.entered = true;
                cameraPlayer.enabled = false;
                cameraArena.enabled = true;
            }
        }
    }
    [PunRPC]
    void SetGlobalDamage()
    {
        globalDamage = true;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {   
        if (stream.IsWriting)
        {
            stream.SendNext(globalDamage);
        }
        else
        {
            globalDamage = (bool)stream.ReceiveNext();
        }
    }
}
