using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jump;
    public bool isJumping;

    public Rigidbody2D rb;
    public Animator Anim;
    public SpriteRenderer SpriteRender; 

    private float Move;

    void Start()
    {
    }

    void Update()
    {
        float Move = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2(speed * Move, rb.linearVelocity.y) ;

        if (Input.GetButtonDown("Jump") && isJumping == false)
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

    }
    // floor collision
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
            Anim.SetBool("IsJumping", false);

        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isJumping = true;
            Anim.SetBool("IsJumping", true);
        }
    }


    

}




