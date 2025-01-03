using System.Collections;
using UnityEngine;

public class NightBorne : MonoBehaviour
{

    public Animator animator;
    public bool isExploding;

    public GameObject attackPosition;
    public float attackRange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        isExploding = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isExploding)
        {
            StartCoroutine(Explode());
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isExploding = true;
        }
    }

    public void ExplodeAttack()
    {


        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPosition.transform.position, attackRange);

        foreach (Collider2D player in hitPlayer)
        {
            if (player.CompareTag("Player"))
            {
                // Call a method to damage the enemy.
                player.GetComponent<PlayerController>().TakeDamage(40f);
                Debug.Log("Hit");
            }

        }
    }


        IEnumerator Explode()
    {
        isExploding = false;
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(1.33f);
        Destroy(this.gameObject);
    }


    private void OnDrawGizmosSelected()

    {

        Gizmos.color = Color.blue; // Set gizmo color

        Gizmos.DrawWireSphere(attackPosition.transform.position, attackRange); // Draw circle at attack origin

    }

}
