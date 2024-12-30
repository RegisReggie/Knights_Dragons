using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{

    public SceneManage sceneManage;

    public GameObject gameManager;
    public GameObject levelManager;
    public GameObject canvas;

    public bool isGameManagerDestroyed;
    public bool isLevelManagerDestroyed;
    public bool isCanvasDestroyed;


    private void Awake()
    {
        sceneManage = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManage>();
        isGameManagerDestroyed = false;
        isLevelManagerDestroyed = false;
        isCanvasDestroyed = false;
    }


    public void Update()
    {
        if(gameManager == null && !isGameManagerDestroyed)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager");
        }
        else if(gameManager != null && !isGameManagerDestroyed)
        {
            Destroy(gameManager);
            isGameManagerDestroyed = true;
        }

        if(levelManager == null && !isLevelManagerDestroyed)
        {
            levelManager = GameObject.FindGameObjectWithTag("LevelManager");
        }else if(levelManager != null && !isLevelManagerDestroyed)
        {
            Destroy(levelManager);
            isLevelManagerDestroyed = true;
        }

        if (canvas == null && !isCanvasDestroyed)
        {
            canvas = GameObject.FindGameObjectWithTag("Canvas");
        }else if (canvas != null && !isCanvasDestroyed)
        {
            Destroy(canvas);
            isCanvasDestroyed = true;
        }
    }

    public void StartGameButton()
    {

        sceneManage = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManage>();
        sceneManage.PressStartGameButton();
        
    }

    public void QuitGameButton()
    {

        sceneManage = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManage>();
        sceneManage.PressQuitGameButton();

    }


}
