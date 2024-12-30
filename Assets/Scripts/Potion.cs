using UnityEngine;
using UnityEngine.UI;

public class Potion : MonoBehaviour
{

    public HealthBar health;
    public GameObject healthBar;
    public GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        health = GameObject.FindGameObjectWithTag("Canvas").GetComponent<HealthBar>();
        healthBar = GameObject.FindGameObjectWithTag("HealthBar");
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            if(this.gameObject.tag == "Health")
            {
                AddHealthUI();
                health.healPotionCount++;
                Destroy(this.gameObject);
            }
            else if (this.gameObject.tag == "Mana")
            {
                AddManaUI();
                health.manaPotionCount++;
                Destroy(this.gameObject);
            }
            

        }
    }

    public void AddHealthUI()
    {
        if(gameManager.isHealthBarActive == true)
        {
            return;
        }
        else
        {
            gameManager.isHealthBarActive = true;
        }

        if(gameManager.isHPAmountActive == true)
        {
            return;
        }
        else
        {
            gameManager.isHPAmountActive = true;
        }
    }

    public void AddManaUI()
    {
        if (gameManager.isManaBarActive == true)
        {
            return;
        }
        else
        {
            gameManager.isManaBarActive = true;
        }

        if(gameManager.isMPAmountActive == true)
        {
            return;
        }
        else
        {
            gameManager.isMPAmountActive = true;
        }
    }
}
