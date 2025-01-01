using UnityEngine;

public class Dummy : MonoBehaviour
{
    private Animator animator;
    public TutorialLevel level;

    public float health;
    private void Awake()
    {
        level = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<TutorialLevel>();
        animator = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            health = 100;
        }
    }

    public void TakeDamage()
    {
        animator.SetTrigger("Hit");
        health -= 10;
        if(this.gameObject.name == "ManaDummy")
        {
            if (level.isBridgeActive == false)
            {
                level.isBridgeActive = true;
            }
        }
        
    }
}
