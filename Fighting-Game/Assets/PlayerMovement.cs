using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jump;
    public bool isJumpingState;
    public bool isAttackingState = false;

    public Rigidbody2D rb;
    public Animator Anim;
    public SpriteRenderer SpriteRender;

    
    private float MoveX;
    private float MoveY;
    private float currentHeight;
    private float previousHeight;


    void Start()
    {
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

    void Update()
    {

        float MoveX = Input.GetAxisRaw("HorizontalA");
        float MoveY = Input.GetAxisRaw("VerticalA");

         //movement & jumping 
        rb.linearVelocity = new Vector2(speed * MoveX, rb.linearVelocity.y);

        if (Input.GetButtonDown("JumpA") && isJumpingState == false)
        {
            rb.AddForce(new Vector2(rb.linearVelocity.x, jump));

        }


        // running anim
        if (MoveX != 0)
        {
            Anim.SetBool("IsRunning", true);
        }
        else
        {
            Anim.SetBool("IsRunning", false);
        }
        //character flipping 
        if (MoveX < 0)
        {
            SpriteRender.flipX = true;
        }
        if (MoveX > 0)
        {
            SpriteRender.flipX = false;
        }


        //fall anim
        float currentHeight = rb.linearVelocity.y;

        // Check if the object reached max jump height and started falling
        if (previousHeight > 0 && currentHeight <= 0)
        {
            Debug.Log("Reached max jump height!");

            Anim.SetBool("IsJumping", false);

            Anim.SetBool("IsFalling", true);
        }

        previousHeight = currentHeight;

        if (Input.GetButtonDown("BasicAttackA") && isJumpingState == false && isAttackingState == false)
        {
            Anim.SetBool("BasicAttackA", true);
            isAttackingState = true;
        }
        else if (Input.GetButtonDown("SPAttackA") && isJumpingState == false && isAttackingState == false)
        {
            Anim.SetBool("SPAttackA", true);
            isAttackingState = true;
        }
        else if (Input.GetButtonDown("BlockingA") && isJumpingState == false && isAttackingState == false)
        {
            Anim.SetBool("IsBlockingA", true);
            isAttackingState = true;
        }
        
        

    }
    public void EndAttack()
    {
        Anim.SetBool("IsAttacking1A", false);
        Anim.SetBool("IsAttacking2A", false);
        Anim.SetBool("IsAttacking3A", false);
        isAttackingState = false;
    }
        
       
 
    



}




