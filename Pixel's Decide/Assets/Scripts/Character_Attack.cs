using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Character_Attack : MonoBehaviourPunCallbacks
{
    PhotonView view;

    private float AttackTimer = 1f;
    private float AttackTimerRemembered = 0;

    bool AttackEnableb;
    Transform Weapon;

    public int damage;
    public float attackRange = 0.5f;
    public LayerMask whoToAttack;

    void Start()
    {
        Weapon = transform.Find("Weapon");
        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if(view.IsMine){
            AttackTimerRemembered -= Time.deltaTime;

            if(AttackTimerRemembered < 0){
                AttackEnableb = true;
            }

            if(Input.GetKeyDown(KeyCode.Space)&&AttackEnableb){
                AttackTimerRemembered = AttackTimer;
            }

            if(AttackEnableb && AttackTimerRemembered > 0){
                AttackEnableb = false;

                Collider2D[] enemysCollider = Physics2D.OverlapCircleAll(Weapon.transform.position,attackRange,whoToAttack);

                foreach(Collider2D enemyCollider in enemysCollider){

                    if(enemyCollider.gameObject != this.gameObject){
                       // enemyCollider.GetComponent<Character_Health>().gotHit(damage);
                       enemyCollider.gameObject.GetPhotonView().RPC("gotHit",RpcTarget.All,damage);
                    }
                }
            }
        }
    }
}
