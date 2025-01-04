using UnityEngine;

public class Chat : MonoBehaviour
{

    public SpriteRenderer sr;
    public DialogueTrigger dialogueTrigger;
    public DialougeManager dialougeManager;

    public bool canChat;
    public bool isChatStart;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        dialougeManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialougeManager>();
        dialogueTrigger = GetComponent<DialogueTrigger>();
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
        canChat = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (canChat)
        {
            sr.enabled = true;

            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (dialougeManager.isChatStarted == false)
                {
                    dialougeManager.isChatStarted = true;
                    dialogueTrigger.TriggerDialogue();
                }
                else
                {
                    dialougeManager.DisplayNextSentence();
                }
                
            }
        }
        else
        {
            sr.enabled = false;
            dialougeManager.EndDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canChat = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canChat = false;
        }
    }


}
