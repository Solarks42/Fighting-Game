using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jump;
    private float Move;
    public bool isJumping;

    public Rigidbody2D rb;
    private Animator Anim;
    
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move = Input.GetAxisRaw("Horizontal");

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
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isJumping = true;
        }
    }
        
   
}




