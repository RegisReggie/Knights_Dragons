using UnityEngine;

public class CaveEntrance : MonoBehaviour
{
    public bool canEnterCave;
    public SceneManage sceneManage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneManage = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManage>();
        canEnterCave = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canEnterCave) 
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                StartCoroutine(sceneManage.SwitchScene());
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canEnterCave = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canEnterCave = false;
    }
}
