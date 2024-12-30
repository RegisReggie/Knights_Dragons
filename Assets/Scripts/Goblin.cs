using System.Collections;
using UnityEngine;

public class Goblin : MonoBehaviour
{

    public PlayerController playerController;

    private Rigidbody2D rb;
    private Animator animator;

    public GameObject attackPosition;
    public float attackRange = 1f;
    public float attackTimer;
    public float attackCooldown = .3f;

    public float health = 100f;

    public float speed;
    public float fallSpeed = 10f;

    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer;
    public bool isGrounded;

    public bool canMove;

    public bool canAttack;
    public bool isAttacking;

    public bool isDead;

    public Transform groundCheck;
    public bool moveRight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canAttack = false;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        isDead = false;
        canMove = true;
        rb = GetComponent<Rigidbody2D>();
        moveRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = CheckGrounded();

        animator.SetBool("isDead", isDead);

        attackTimer -= Time.deltaTime;  // Decrease cooldown timer over time.

        if (!isGrounded)
        {
            moveRight = !moveRight;
            Flip();
        }

        if (!canMove)
        {
            rb.linearVelocity = Vector2.down * fallSpeed;
        }

        if (health <= 0)
        {
            canMove = false;
            if (!isDead)
            {
                StartCoroutine(Dead());
            }

        }
        if (!isDead)
        {
            if (canAttack)
            {
                if (isGrounded)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Goblin_Attack"))
                    {
                        isAttacking = true;
                        StopMovement();
                        return;
                    }
                    else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Goblin_Attack"))
                    {
                        isAttacking = false;

                        if (attackTimer <= 0f)
                        {
                            animator.SetTrigger("Goblin_Attack");
                            //Attack();
                        }
                    }

                }

            }
            else
            {
                canMove = true;
                isAttacking = false;
            }
        }
       
       
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            if (moveRight)
            {
                rb.linearVelocity = Vector3.right * speed;
            }
            else
            {
                rb.linearVelocity = Vector2.left * speed;
            }
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if(collision.gameObject.tag == "Player")
        {

            if (collision.transform.position.x < transform.position.x)
            {
                if (moveRight)
                {
                    moveRight = !moveRight;
                    Flip();
                }
            }
            else if (collision.transform.position.x > transform.position.x)
            {
                if (!moveRight)
                {
                    moveRight = !moveRight;
                    Flip();
                }
            }

            canAttack = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            canAttack = false;
        }

    }

    public void Attack()
    {
        isAttacking = true;
        //animator.SetTrigger("Goblin_Attack");


        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPosition.transform.position, attackRange);

        foreach (Collider2D player in hitPlayer)
        {
            if (player.CompareTag("Player"))
            {
                // Call a method to damage the enemy.
                player.GetComponent<PlayerController>().TakeDamage(40f);
            }

        }


        // Reset attack cooldown.
        attackTimer = attackCooldown;
    }
    public void Flip()
    {
        if (!isDead)
        {
            transform.Rotate(0f, 180f, 0f);
        }

    }

    public void StopMovement()
    {
        canMove = false;
        rb.linearVelocity = Vector2.down * speed;
    }

    public void TakeDamage()
    {
        if (health > 0)
        {
            StartCoroutine(Hurt());
        }

    }

    public IEnumerator Hurt()
    {
        canMove = false;
        health -= 50f;
        animator.SetTrigger("Hurt");
        yield return new WaitForSeconds(.3f);
        canMove = true;
    }

    public IEnumerator Dead()
    {
        isDead = true;
        animator.SetTrigger("Dead");
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }



    bool CheckGrounded()
    {
        // Cast a ray downwards from the character's position to check if it hits the ground
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

        // Return true if the raycast hits something that belongs to the ground layer
        return hit.collider != null;
    }

    private void OnDrawGizmosSelected()

    {

        Gizmos.color = Color.red; // Set gizmo color

        Gizmos.DrawWireSphere(attackPosition.transform.position, attackRange); // Draw circle at attack origin

    }
}
