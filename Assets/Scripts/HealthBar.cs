using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
public class HealthBar : MonoBehaviour
{
    public Image healthBarFill;   // The fill image (foreground of the health bar)
    public Image manaBarFill; // The fill image (foreground of the health bar)
    
    public float maxHealth = 100f; // Max health of the player
    public float maxMana = 100f;
    public float currentHealth;  // Current health of the player
    public float currentMana;
    
    public TMP_Text healthText;
    public TMP_Text manaText;
    public TMP_Text goldAmountText;

    public float healPotionCount;
    public float manaPotionCount;

    public TMP_Text healthPotionAmount;
    public TMP_Text manaPotionAmount;

    public int goldAmount;


    public PlayerController playerController;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        healthText = GameObject.FindGameObjectWithTag("HealthBar").GetComponentInChildren<TMP_Text>();
        manaText = GameObject.FindGameObjectWithTag("ManaBar").GetComponentInChildren<TMP_Text>();
        goldAmountText = GameObject.FindGameObjectWithTag("Gold").GetComponent<TMP_Text>();
        healthPotionAmount = GameObject.FindGameObjectWithTag("Hp").GetComponentInChildren<TMP_Text>();
        manaPotionAmount = GameObject.FindGameObjectWithTag("Mp").GetComponentInChildren<TMP_Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        // Initialize the current health to max health
        currentHealth = maxHealth;
        currentMana = maxMana;
    }

    // Update is called once per frame
    void Update()
    {

        goldAmountText.text = " : " + goldAmount.ToString();
        healthPotionAmount.text = " : " + healPotionCount.ToString();
        manaPotionAmount.text = " : " + manaPotionCount.ToString() ;

        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeMana(25f);
        }

        if (Input.GetKeyDown(KeyCode.V) && healPotionCount > 0 && currentHealth < maxHealth) 
        {
            Debug.Log("Health Added");
            Heal(40f);
            healPotionCount--;
        }

        if(Input.GetKeyDown(KeyCode.B) && manaPotionCount > 0 && currentMana < maxMana)
        {
            Debug.Log("Mana Added");
            AddMana(40f);
            manaPotionCount--;
        }

        // Update the health bar fill amount based on current health
        healthBarFill.fillAmount = currentHealth / maxHealth;
        manaBarFill.fillAmount = currentMana / maxMana;

        healthText.text = currentHealth.ToString();
        manaText.text = currentMana.ToString();
    }

    public void TakeMana(float manaLost)
    {
        currentMana -= manaLost;

        // Ensure current mana doesn't go below 0
        if (currentMana < 0)
        {
            currentMana = 0;
        }
    }

    // Function to heal the player
    public void Heal(float amount)
    {
        currentHealth += amount;

        // Ensure current health doesn't exceed max health
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void AddMana(float amount)
    {
        currentMana += amount;

        // Ensure current mana doesn't exceed max mana
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
    }

    public void ResetStats()
    {
        currentHealth = maxHealth;

    }
}
