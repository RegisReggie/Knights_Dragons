using System.Collections;
using UnityEngine;

public class Goblin : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator animator;



    public float health = 100f;

    public float speed;
    public float fallSpeed = 10f;

    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer;
    private bool isGrounded;

    public bool canMove;

    public bool isDead;

    public Transform groundCheck;
    public bool moveRight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
    bool CheckGrounded()
    {
        // Cast a ray downwards from the character's position to check if it hits the ground
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

        // Return true if the raycast hits something that belongs to the ground layer
        return hit.collider != null;
    }

    public void Flip()
    {
        if (!isDead)
        {
            transform.Rotate(0f, 180f, 0f);
        }
        
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
}
