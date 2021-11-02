using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Character_Health : MonoBehaviourPunCallbacks, IPunObservable
{
    
    public int maxHealth = 0;
    float currentHealth;
    Transform healthBar;
    SpriteRenderer spriteRendererHP;
  
    void Start()
    {
        healthBar = transform.Find("HealthBar");
        currentHealth = maxHealth;
        spriteRendererHP = healthBar.GetComponent<SpriteRenderer>();
        spriteRendererHP.enabled = false;
        healthBar.localScale = new Vector3(currentHealth,2,1);
    }

    void Update()
    {
        if(currentHealth < maxHealth){
            spriteRendererHP.enabled = true;
        }
        healthBar.localScale = new Vector3(currentHealth,1,1);
    }
    [PunRPC]
    public void gotHit(int valueOfDamage)
    {
        currentHealth -= valueOfDamage;
        Debug.Log(this);
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
