using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    public static CanvasScript instance;

    public GameManager gameManager;

    public GameObject healthBar;
    public GameObject manaBar;
    public GameObject gold;
    public GameObject hpPotionAmount;
    public GameObject mpPotionAmount;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        healthBar = GameObject.FindGameObjectWithTag("HealthBar");
        manaBar = GameObject.FindGameObjectWithTag("ManaBar");
        gold = GameObject.FindGameObjectWithTag("Gold");
        hpPotionAmount = GameObject.FindGameObjectWithTag("Hp");
        mpPotionAmount = GameObject.FindGameObjectWithTag("Mp");


    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CanvasUIController();
    }

    public void CanvasUIController()
    {
        if (!gameManager.isHealthBarActive)
        {
            healthBar.SetActive(false);
        }
        else
        {
            healthBar.SetActive(true);
        }

        if (!gameManager.isManaBarActive)
        {
            manaBar.SetActive(false);
        }
        else
        {
            manaBar.SetActive(true);
        }

        if (!gameManager.isGoldActive)
        {
            gold.SetActive(false);
        }
        else
        {
            gold.SetActive(true);
        }

        if (!gameManager.isHPAmountActive)
        {
            hpPotionAmount.SetActive(false);
        }
        else
        {
            hpPotionAmount.SetActive(true);
        }

        if (!gameManager.isMPAmountActive)
        {
            mpPotionAmount.SetActive(false);
        }
        else
        {
            mpPotionAmount.SetActive(true);
        }
    }
}
