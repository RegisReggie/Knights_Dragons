using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public bool isCanvasActive;
    public bool isHealthBarActive;
    public bool isManaBarActive;
    public bool isGoldActive;
    public bool isHPAmountActive;
    public bool isMPAmountActive;
    
 

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
        isCanvasActive = false;
        isHealthBarActive = false;
        isManaBarActive = false;
        isGoldActive = false;
        isHPAmountActive = false;
        isMPAmountActive = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
