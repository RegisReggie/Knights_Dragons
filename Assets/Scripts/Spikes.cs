using UnityEngine;
using UnityEngine.UI;



public class Spikes : MonoBehaviour
{

    public HealthBar health;


    private void Awake()
    {
        health = GameObject.FindGameObjectWithTag("Canvas").GetComponent<HealthBar>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

        }
    }
}
