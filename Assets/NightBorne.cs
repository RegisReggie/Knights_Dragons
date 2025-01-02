using System.Collections;
using UnityEngine;

public class NightBorne : MonoBehaviour
{

    public Animator animator;
    public bool isExploding;

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

    IEnumerator Explode()
    {
        isExploding = false;
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(1.33f);
        Destroy(this.gameObject);
    }
}
