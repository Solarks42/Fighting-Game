using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jump;
    private float Move;
    public bool isJumping;

    public Rigidbody2D rb;
    public Animator Anim;
    public SpriteRenderer SpriteRender;  

    void Start()
    {
    }

    void Update()
    {
        float Move = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2(speed * Move, rb.linearVelocity.y);

        if (Input.GetButtonDown("Jump") && isJumping == false)
        {
            rb.AddForce(new Vector2(rb.linearVelocity.x, jump));
        }

        if (Move != 0)
        {
            Anim.SetBool("IsRunning", true);
        }
        else
        {
            Anim.SetBool("IsRunning", false);
        }

        if (Move < 0)
        {
            SpriteRender.flipX = true;
        }
        if (Move > 0)
        {
            SpriteRender.flipX = false;
        }

    }

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




