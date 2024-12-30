using UnityEngine;

public class Chest : MonoBehaviour
{

    public Animator anim;
    public HealthBar goldAmount;
    public GameManager manager;
    public bool ableToOpen;
    public bool isOpen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
        goldAmount = GameObject.FindGameObjectWithTag("Canvas").GetComponent<HealthBar>();
        isOpen = false;
        ableToOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!isOpen && ableToOpen)
            {
                anim.SetTrigger("Open");
                AddGold(10);
                isOpen = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ableToOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ableToOpen = false;
        }
    }

    public void AddGold(int gold)
    {
        AddGoldUI();
        goldAmount.goldAmount += gold;
    }

    public void AddGoldUI()
    {
        if (manager.isGoldActive == true)
        {
            return;
        }
        else
        {
            manager.isGoldActive = true;
        }
    }
}
