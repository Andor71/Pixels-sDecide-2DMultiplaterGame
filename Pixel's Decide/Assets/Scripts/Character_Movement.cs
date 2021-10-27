using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Movement : MonoBehaviour
{
    
    private float playerScale;
    private float distanceToGround;
    private float XCord ; 
    bool IsGrounded;
    bool canJump;
    public Collider2D collider;
    public Rigidbody2D rigidbody;
    public Transform feetPos;
    public LayerMask Ground;
  


    [Range(1,100)]
    public int speed = 1;
    public float jumpSpeed;

    void Start()
    {
        playerScale = transform.localScale.x;
    }

    void Update()
    {

        //By hitting A or D in keyboard we can move our player.
        //Checking if Input is greater than 0 or not and flipping the player model by that value.
        XCord = Input.GetAxisRaw("Horizontal") * speed;

        if ( Input.GetKeyDown(KeyCode.W)){
            canJump = true;
        }

   }
   void FixedUpdate()
   {
       
        Vector3 move = new Vector3(1,0,0) * XCord;
        rigidbody.velocity = move;

        Vector3 charecterScale = transform.localScale;
        if (XCord < 0)
        {
            charecterScale.x = -playerScale;
        } 
        if (XCord > 0)
        {
            charecterScale.x = playerScale;
        }
        transform.localScale = charecterScale;

        IsGrounded = Physics2D.OverlapCircle(feetPos.position, 0.1f, Ground);
        if (canJump && IsGrounded){
            canJump = false;
            Debug.Log("Space");
            rigidbody.velocity = Vector2.up * jumpSpeed ;
        }
   }
}
