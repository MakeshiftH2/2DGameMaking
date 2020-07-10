using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;

    public GameObject UICanvas;
    public GameObject triggerDestroyer;

    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        UICanvas.SetActive(false);
        sentences = new Queue<string>();
    }


    public void StartDialogue(Dialogue dialogue)
    {
        UICanvas.SetActive(true);

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
        dialogueText.text = sentence;
    }

    public void EndDialogue()
    {
        Debug.Log("End of convo");
        FindObjectOfType<Player>().canMove = true;
        UICanvas.SetActive(false);
        Destroy(triggerDestroyer);

    }
}
