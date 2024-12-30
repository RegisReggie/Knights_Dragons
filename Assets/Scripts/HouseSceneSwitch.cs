using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseSceneSwitch : MonoBehaviour
{

    public SceneManage sceneManage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneManage = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(sceneManage.SwitchScene());
        }
    }
}
