//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SceneManage : MonoBehaviour
{
    public bool isSwitchingScenes;
    public Animator animator;
    public GameObject fadeEffect;

    public static SceneManage instance;

    public int currentscene;

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
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        animator = GameObject.FindGameObjectWithTag("Fade").GetComponent<Animator>();
        fadeEffect = GameObject.FindGameObjectWithTag("Fade");
        Invoke("TurnOffRaycastForFadeEffect", 1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        currentscene = SceneManager.GetActiveScene().buildIndex;

        //TestSceneSwitch();
    }

    public IEnumerator SwitchScene()
    {

        isSwitchingScenes = true;
        animator = GameObject.FindGameObjectWithTag("Fade").GetComponent<Animator>();
        animator.SetTrigger("End");
        yield return new WaitForSeconds(3f);
        Debug.Log("Scene Switched");
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(1);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(2);
        }
        else if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            SceneManager.LoadScene(0);
        }
        
        isSwitchingScenes = false;
        animator.SetTrigger("Start");
    }

    public IEnumerator RestartScene()
    {
        animator = GameObject.FindGameObjectWithTag("Fade").GetComponent<Animator>();
        animator.SetTrigger("End");
        
        yield return new WaitForSeconds(3f);
        Debug.Log("Scene Switched");
        SceneManager.LoadScene(currentscene);
        animator.SetTrigger("Start");
    }

    public void PressStartGameButton()
    {

        if (SceneManager.GetActiveScene().buildIndex == 0 && isSwitchingScenes == false)
        {
            StartCoroutine(SwitchScene());
        }
    }

    public void PressQuitGameButton()
    {
        Application.Quit();
    }

    public void TurnOffRaycastForFadeEffect()
    {
        fadeEffect.GetComponent<Image>().raycastTarget = false;
    }

    public void TestSceneSwitch()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 && Input.GetKeyDown(KeyCode.Z) && isSwitchingScenes == false)
        {
            StartCoroutine(SwitchScene());
        }
    }
}
