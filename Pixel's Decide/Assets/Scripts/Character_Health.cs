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
    GameManager gameManager;
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
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
        if(currentHealth <= 0){
           photonView.RPC("Die",RpcTarget.AllBuffered);
        }


        if(view.IsMine){
            
            if(stayingInArena){
                gameManager.StopArrowRain();
            }

            if(collision_Detector.getGlobalDamage() && !stayingInArena)
            {
                if(gameManager.particlesystem.isStopped){
                    gameManager.StartArrowRain();
                }
                if(gameManager.particlesystem.isPlaying && stayingInArena){
                    gameManager.StopArrowRain();
                }
                if(Time.time > timeRGlobalDamage){
                    timeRGlobalDamage = Time.time + timeGlobalDamage;
                    gotHit(5);
                }
            }
        }
    }
    [PunRPC]
    public void Die()
    {
        gameManager.SetLose();
        gameManager.SomeoneDied();
        //Turn of Scripts
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Character_Movement>().enabled = false;
        GetComponent<Character_Attack>().enabled = false;
        GetComponent<Character_Health>().enabled = false;
        
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
