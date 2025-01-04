using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.UI;
using TMPro;

public class DialougeManager : MonoBehaviour
{

    public bool isChatStarted;

    public Queue<string> sentences;

    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isChatStarted = false;
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

        
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        //dialogueText.text = sentence;

        //Stops corroutine from typing before starting a new one
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        isChatStarted = false;
    }
}
 