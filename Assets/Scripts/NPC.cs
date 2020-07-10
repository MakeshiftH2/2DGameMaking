using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Dialogue dialogue;
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            TriggerDialogue();
        }
    }

    private void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().UICanvas.SetActive(true);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        FindObjectOfType<Player>().canMove = false;
    }


}

