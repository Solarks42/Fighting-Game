using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jump;
    public bool isJumpingState;

    public Rigidbody2D rb;
    public Animator Anim;
    public SpriteRenderer SpriteRender; 

    private float Move;
    private float currentHeight; 
    private float previousHeight;
    
    void Start()
    {
    }

    void Update()
    {
        //movement & jumping 
        float Move = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2(speed * Move, rb.linearVelocity.y);

        if (Input.GetButtonDown("Jump") && isJumpingState == false)
        {
            rb.AddForce(new Vector2(rb.linearVelocity.x, jump));

        }


        // running anim
        if (Move != 0)
        {
            Anim.SetBool("IsRunning", true);
        }
        else
        {
            Anim.SetBool("IsRunning", false);
        }
        //character flipping 
        if (Move < 0)
        {
            SpriteRender.flipX = true;
        }
        if (Move > 0)
        {
            SpriteRender.flipX = false;
        }


        //fall anim
        float currentHeight = rb.linearVelocity.y;

        // Check if the object reached max jump height and started falling
        if (previousHeight > 0 && currentHeight <= 0) {
            Debug.Log("Reached max jump height!");
         
            
            Anim.SetBool("IsJumping", false);
             
            Anim.SetBool("IsFalling", true);
            

        }

        previousHeight = currentHeight;


    }
    // floor collision
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            Anim.SetBool("IsFalling", false);
            isJumpingState = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isJumpingState = true;
            Anim.SetBool("IsJumping", true);
        }
    }


    

}




