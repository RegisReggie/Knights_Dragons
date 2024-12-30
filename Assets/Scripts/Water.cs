using UnityEngine;
using UnityEngine.SceneManagement;

public class Water : MonoBehaviour
{

    public SceneManage SceneManage;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SceneManage = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(SceneManage.RestartScene());
            Destroy(collision.gameObject);
           
        }
    }
}
