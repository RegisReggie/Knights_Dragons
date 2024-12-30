using UnityEngine;

public class TutorialLevel : MonoBehaviour
{

    public static TutorialLevel instance;

    public GameObject bridge;
    public bool isBridgeActive = false;
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

    }

    // Update is called once per frame
    void Update()
    {

        if(bridge == null)
        {
            bridge = GameObject.FindGameObjectWithTag("Bridge");
        }

        bridge.SetActive(isBridgeActive);
    }
}
