using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Character_Health : MonoBehaviourPunCallbacks, IPunObservable
{
    
    PhotonView view;
    public int maxHealth = 0;
    public int globalDamageDamage = 1;
    public float timeGlobalDamage = 5;
    float timeRGlobalDamage = 0;
    float currentHealth;
    public bool stayingInArena = false;
    Transform healthBar;
    SpriteRenderer spriteRendererHP;
    Collision_Detector collision_Detector;

    void Start()
    {
        healthBar = transform.Find("HealthBar");
        currentHealth = maxHealth;
        spriteRendererHP = healthBar.GetComponent<SpriteRenderer>();
        spriteRendererHP.enabled = false;
        collision_Detector = GetComponent<Collision_Detector>();
        view = GetComponent<PhotonView>();
        healthBar.localScale = new Vector3(currentHealth,2,1);
    }

    void Update()
    {
        if(currentHealth < maxHealth){
            spriteRendererHP.enabled = true;
            spriteRendererHP.color = Color.green;
        }
        healthBar.localScale = new Vector3(currentHealth/10,1,1);
        
        if(currentHealth < maxHealth*50/100){
            spriteRendererHP.color = Color.yellow;
        }

        if(currentHealth < maxHealth*25/100){
            spriteRendererHP.color = Color.red;
        }
        if(currentHealth == maxHealth){
            spriteRendererHP.enabled = false;
        }


        if(view.IsMine){
            if(collision_Detector.getGlobalDamage() && !stayingInArena)
            {
                if(Time.time > timeRGlobalDamage){
                    timeRGlobalDamage = Time.time + timeGlobalDamage;
                    gotHit(5);
                }
            }
        }
    }

    public void gotHit(int valueOfDamage)
    {
        photonView.RPC("RPCgotHit",RpcTarget.AllBuffered,valueOfDamage);
    }

    [PunRPC]
    public void RPCgotHit(int valueOfDamage)
    {
        currentHealth -= valueOfDamage;
    }

    public void Heal()
    {
        photonView.RPC("RPCHeal",RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RPCHeal()
    {
        currentHealth = maxHealth;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentHealth);
        }
        else
        {
            currentHealth = (int)stream.ReceiveNext();
        }
    }


}
