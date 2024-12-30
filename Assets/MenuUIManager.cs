using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{

    public SceneManage sceneManage;


    private void Awake()
    {
        sceneManage = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManage>();
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
