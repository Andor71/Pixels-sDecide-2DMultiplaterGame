using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Health : MonoBehaviour
{
    public float maxHealth = 0f;
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
      //  transform.localScale = healthBar.localScale;
    }

    public void gotHit(float valueOfDamage)
    {
        currentHealth -= valueOfDamage;
        Debug.Log("Got Hitted, current Health: "+currentHealth);
    }

}
