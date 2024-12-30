using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public SceneManage sceneManage;
    public HealthBar healthBar;

    private Rigidbody2D rigidbody2;
    public Animator anim;
    private SpriteRenderer spriteRenderer;

    public GameObject attackPosition;
    public Transform groundCheck;

    public RuntimeAnimatorController detachSword;
    public RuntimeAnimatorController equipSword;

    public float speed;
    public float dashSpeed;
    public float moveH;

    public float jumpHeight;
    public float doubleJumpHeight;
    private int jumpCount;
    public int maxJumpCount;
    public bool isJumping;
    public float jumpTimeCounter;
    public float jumpTime;

    public float fallSpeed = 10f;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    private bool isGrounded;

    public bool facingRight;

    public bool isCrouching;
    public bool canMove;
    public bool isSwordAttached;

    public bool canDrink;
    public bool canCrouch;
    public bool canSwitchWeapon;
    public bool canJump;
    public bool canAttack;
    public bool canFlipSprite;
    public bool isDead;
    public bool canDie;

    public float playerHealth = 100;

    public float attackRange = 1f;  // Attack range in units.
    public float attackCooldown = 0.5f;  // Time between attacks.
    public int attackDamage = 10;  // Damage dealt by the attack.

    private float attackTimer = 0f;  // Timer to manage attack cooldown.


    public bool isInvulnerable = false;
    public float invulnerabilityDuration = 2f;
    public float flashDuration = .01f;
    public float timeElapsed = 0f;
    private void Awake()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (GameObject.FindGameObjectWithTag("SceneManager"))
        {
            sceneManage = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManage>();
        }

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canDie = true;
        canDrink = true;
        isDead = false;
        canFlipSprite = true;
        canSwitchWeapon = true;
        canAttack = true;
        canJump = true;
        canMove = true;
        canCrouch = true;
        isCrouching = false;
        facingRight = true;
        maxJumpCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        moveH = Input.GetAxisRaw("Horizontal");

        isGrounded = CheckGrounded();

        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isJumping", isJumping);


        PlayerStats();



        if (isGrounded)
        {
            anim.SetFloat("Speed", Mathf.Abs(moveH));

        }

        attackTimer -= Time.deltaTime;  // Decrease cooldown timer over time.


        if (Input.GetKeyDown(KeyCode.X) && attackTimer <= 0f)
        {
            if (canAttack)
            {
                if (!isCrouching)
                {
                    Attack();
                }
                
            }
           
        }

        if (canCrouch)
        {
            Crouch();
        }
        
        Dash(dashSpeed);

        if (!isDead)
        {
            //Dead();
        }
       
        
        if (canFlipSprite)
        {
            FlipSprite();
        }

        if (canDrink)
        {
            DrinkHealthPotion();
            DrinkManaPotion();
        }
        
        if (canSwitchWeapon)
        {
            SwitchWeapon();
        }

        if (!isDead)
        {
            TakeDamage(10f);
        }
       

        if (canJump)
        {
            if (!isCrouching)
            {
                Jump();
            }
        }


        //Stop player movement when attacking
        if (isGrounded && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            speed = 0.0f;
        }
        else
        {
            speed = 10f;
        }
    }

    private void FixedUpdate()
    {

        MovePlayer();
    }

   
    void MovePlayer()
    {
        if(canMove)
        {
            float moveDirection = moveH * speed;
            rigidbody2.linearVelocity = new Vector2(moveDirection, rigidbody2.linearVelocity.y);
        }
    }

    public void FlipSprite()
    {
        if (moveH > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveH < 0 && facingRight)
        {
            Flip();
        }
    }

    public void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void Attack()
    {
            anim.SetTrigger("Attack");


        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition.transform.position, attackRange);

            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.CompareTag("Dummy"))
                {
                    // Call a method to damage the enemy.
                    enemy.GetComponent<Dummy>().TakeDamage();
                }

                if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<Goblin>().TakeDamage();
            }

            }

            // Reset attack cooldown.
            attackTimer = attackCooldown;
    }

    public void Crouch()
    {
        if (Input.GetKey(KeyCode.DownArrow) && isGrounded)
        {
            canMove = false;
            isCrouching = true;
            anim.SetBool("isCrouching", isCrouching);
        }else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            canMove = true;
            isCrouching = false;
            anim.SetBool("isCrouching", isCrouching);
        }

        if (isCrouching)
        {
            rigidbody2.linearVelocity = Vector2.down * fallSpeed;
        }
    }

    public void Dash(float dash)
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = dash; 
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 10;
        }
    }

    public void Dead()
    {
        if (!isDead)
        {
            canDrink = false;
            canJump = false;
            canCrouch = false;
            canSwitchWeapon = false;
            canFlipSprite = false;
            isDead = true;
            canDie = false;
            canMove = false;
            canAttack = false;
            anim.SetTrigger("Dead");
        }
        
        if (!canDie)
        {
            canDie = true; ;
            rigidbody2.linearVelocity = Vector2.down * fallSpeed;
            healthBar.ResetStats();
            StartCoroutine(sceneManage.RestartScene());
            
        }
    }

    public void DrinkHealthPotion()
    {
        if (Input.GetKeyDown(KeyCode.V) && isGrounded)
        {
            if (healthBar.healPotionCount > 0)
            {
                anim.SetTrigger("Drink");
                healthBar.Heal(40f);
            }
            
        }
    }

    public void DrinkManaPotion()
    {
        if (Input.GetKeyDown(KeyCode.B) && isGrounded)
        {
            if (healthBar.manaPotionCount > 0)
            {
                anim.SetTrigger("Drink");
                healthBar.AddMana(40f);
            }

        }
    }

    public void Jump()
    {

        // Makes sure jump count doesnt go past 1 for double jump
        if (jumpCount >= maxJumpCount)
        {
            jumpCount = maxJumpCount;
        }


        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
                isJumping = true;
                jumpTimeCounter = jumpTime;
                anim.SetTrigger("Jump");

                rigidbody2.linearVelocity = Vector2.up * jumpHeight;
                jumpCount++;
            Debug.Log("Character is grounded.");
        }

        if(Input.GetKey(KeyCode.Space) && isJumping)
        {
            if(jumpTimeCounter > 0)
            {
                rigidbody2.linearVelocity = Vector2.up * jumpHeight;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
            
        // Do something if the character is not grounded (e.g., disable jumping)
            
       
        if (!isGrounded) {

            Debug.Log("Character is not grounded.");

            if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
            {
                rigidbody2.linearVelocity = Vector2.up * doubleJumpHeight;

                jumpCount--;
            }

        }
        
    }

    


    public void PlayerStats()
    {

        if (healthBar == null)
        {
            healthBar = GameObject.FindGameObjectWithTag("Canvas").GetComponent<HealthBar>();
        }

        playerHealth = healthBar.currentHealth;
    }

    public void SwitchWeapon()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            isSwordAttached = !isSwordAttached;
        }

        if (isSwordAttached)
        {
            anim.runtimeAnimatorController = equipSword;
        }
        else
        {
            anim.runtimeAnimatorController = detachSword;
        }
        
    }

    public void TakeDamage(float damage)
    {

        if (isInvulnerable)
        {
            return;
        }
        //Take out of update later
        if (Input.GetKeyDown(KeyCode.H) && playerHealth > 0)
        {
            anim.SetTrigger("Hurt");

            healthBar.currentHealth -= damage;

            // Ensure current health doesn't go below 0
            if (healthBar.currentHealth <= 0)
            {
                healthBar.currentHealth = 0;
                Dead();
            }
            else
            {
                StartCoroutine(Invunerability());
            }
        }
    }

    public IEnumerator Invunerability()
    {
        isInvulnerable = true;
        timeElapsed = 0f;

        while (timeElapsed < invulnerabilityDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(flashDuration);
            timeElapsed += flashDuration;
        }

        spriteRenderer.enabled = true;
        isInvulnerable = false;

    }

    bool CheckGrounded()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Optionally, you can visualize the circle to help debugging
        Debug.DrawLine(groundCheck.position - Vector3.right * groundCheckRadius, groundCheck.position + Vector3.right * groundCheckRadius, Color.green);

        return isGrounded;
    }


    private void OnDrawGizmosSelected()

    {

        Gizmos.color = Color.red; // Set gizmo color

        Gizmos.DrawWireSphere(attackPosition.transform.position, attackRange); // Draw circle at attack origin

    }

    /* change the value to hit max speed over time when running and when wall jump or when not pressing anything reset the value */
}