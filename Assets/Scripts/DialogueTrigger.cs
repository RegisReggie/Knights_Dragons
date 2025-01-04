using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DialogueTrigger : MonoBehaviour
{


    public Dialogue dialogue;
    
    public DialougeManager dialougeManager;

    public void TriggerDialogue()
    {
        dialougeManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialougeManager>();
        dialougeManager.StartDialogue(dialogue);
    }
}
