// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Drawing;
using System.Numerics;

public class FighterController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    [Header("Combat")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 10;
    public LayerMask enemyLayers;

    [Header("Input")]
    public string horizontalInput = "Horizontal";
    public KeyCode jumpKey = KeyCode.W;
    public KeyCode attackKey = KeyCode.Space;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private bool isAttacking;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        HandleMovement();
        HandleJump();
        HandleAttack();
        FlipTowardsOpponent();
    }

    void HandleMovement()
    {
        if (isAttacking) return;

        float move = Input.GetAxisRaw(horizontalInput);
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        animator.SetBool("isWalking", move != 0);
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("Jump");
        }
    }

    void HandleAttack()
    {
        if (Input.GetKeyDown(attackKey) && !isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
            Invoke(nameof(PerformAttack), 0.2f); // small delay to match animation
            Invoke(nameof(ResetAttack), 0.5f);
        }
    }

    void PerformAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<FighterController>().TakeDamage(attackDamage);
        }
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    public void TakeDamage(int damage)
    {
        // TODO: Apply damage logic here
        Debug.Log($"{gameObject.name} took {damage} damage!");
    }

    void FlipTowardsOpponent()
    {
        GameObject opponent = GameObject.FindGameObjectsWithTag("Fighter")[0] == gameObject
            ? GameObject.FindGameObjectsWithTag("Fighter")[1]
            : GameObject.FindGameObjectsWithTag("Fighter")[0];

        if (opponent == null) return;

        if (transform.position.x > opponent.transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

