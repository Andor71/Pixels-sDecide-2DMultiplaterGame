using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    public float jumpVelocity = 5f;
    float jumptimer = 2f;
    float jumptimerd = 0;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        jumptimerd -=Time.deltaTime;
    }
    
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space) && jumptimerd < 0)
        {
            rigidbody2D.velocity = Vector2.up * jumpVelocity;
            jumptimerd = jumptimer;
        }       
    }
}
