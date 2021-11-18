using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Character_Attack : MonoBehaviourPunCallbacks
{
    PhotonView view;
    Weapon_PickUp weapon_PickUp;
    Animator animator;
    private float AttackTimer = 1f;
    private float AttackTimerRemembered = 0;

    bool AttackEnableb;
    Collider2D weaponCollider;

    //Test
    GameObject weapon;

    public float attackRange = 0.5f;
    public LayerMask whoToAttack;
    public int damageAxe;
    public int damageSpire;
    public int damageSword;

    void Start()
    {
        animator = GetComponent<Animator>();
        view = GetComponent<PhotonView>();
        weapon_PickUp = GetComponent<Weapon_PickUp>();
        weapon = GameObject.Find("WeaponSlot");
    }


    public void updateWeaponCollider(Collider2D weaponColliderf)
    {
        weaponCollider = weaponColliderf;
    }


    void Update()
    {


        if(view.IsMine)
        {
            AttackTimerRemembered -= Time.deltaTime;

            if(AttackTimerRemembered < 0){
                AttackEnableb = true;
            }

            if(Input.GetKeyDown(KeyCode.Space)&&AttackEnableb){
                AttackTimerRemembered = AttackTimer;
            }

            if(AttackEnableb && AttackTimerRemembered > 0){
                AttackEnableb = false;

                Collider2D[] targets =Physics2D.OverlapCircleAll(weapon.transform.position,attackRange,whoToAttack);
                foreach (Collider2D target in targets)
                {
                    //view.RPC("gotHit",target,damageAxe);
                    target.gameObject.GetComponent<Character_Health>().gotHit(5);
                    //enemyCollider.gameObject.GetPhotonView().RPC("gotHit",RpcTarget.All,damage);
                }
               // animator.SetTrigger("SpearAttack");
                // foreach(Collider2D enemyCollider in enemysCollider){

                //     if(enemyCollider.gameObject != this.gameObject){
                //        enemyCollider.gameObject.GetPhotonView().RPC("gotHit",RpcTarget.All,damage);
                //     }
                // }
            }
        }
    }
}
