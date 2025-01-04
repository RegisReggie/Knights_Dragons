using UnityEngine;

public class Chat : MonoBehaviour
{

    public SpriteRenderer sr;
    public DialogueTrigger dialogueTrigger;

    public bool canChat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
        sr = GetComponent<SpriteRenderer>();
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
                dialogueTrigger.TriggerDialogue();
            }

        }
        else
        {
            sr.enabled = false;
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
