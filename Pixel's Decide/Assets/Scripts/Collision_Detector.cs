using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_Detector : MonoBehaviour
{

    Character_Health character_Health_Script;

    public float damageofSpike = 5f; 
    void Start()
    {
        character_Health_Script = GetComponent<Character_Health>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Spikes"))
        {
            character_Health_Script.gotHit(damageofSpike);
        }
    }
}
