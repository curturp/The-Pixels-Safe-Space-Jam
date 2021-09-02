using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public DialogueTrigger[] dialogueTriggers;
    private MoveableObject moveableObject;
    private GameManager gameManager;
    public Transform circleMask;

    private bool firstConvo = true;
    public bool noLight;
    public bool hasLight;

    private void Start()
    {
        moveableObject = GetComponent<MoveableObject>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (hasLight == true)
        {
            circleMask.localScale += circleMask.localScale * Time.deltaTime;
        }
    }

    public void RedDialouge()
    {
        if (firstConvo)
        {
            if (dialogueTriggers[0].dialogueManager.animator.GetBool("IsOpen") == true)
            {
                if (dialogueTriggers[0].dialogue.name == "You")
                {
                    dialogueTriggers[0].dialogue.name = "Red";
                    dialogueTriggers[0].dialogueManager.nameText.text = "Red";
                }
                else if (dialogueTriggers[0].dialogue.name == "Red")
                {
                    dialogueTriggers[0].dialogue.name = "You";
                    dialogueTriggers[0].dialogueManager.nameText.text = "You";
                }
                dialogueTriggers[0].ContinueDialogue();
            }                
            else
            {
                dialogueTriggers[0].dialogue.name = "You";
                dialogueTriggers[0].TriggerDialogue();
            }

            if (dialogueTriggers[0].dialogueManager.sentences.Count == 0)
            {
                firstConvo = false;
                noLight = true;
            }            
        }
        else if (noLight)
        {
            if (dialogueTriggers[1].dialogueManager.animator.GetBool("IsOpen") == true)
            {
                if (dialogueTriggers[1].dialogue.name == "You")
                {
                    dialogueTriggers[1].dialogue.name = "Red";
                    dialogueTriggers[1].dialogueManager.nameText.text = "Red";
                }
                else if (dialogueTriggers[1].dialogue.name == "Red")
                {
                    dialogueTriggers[1].dialogue.name = "You";
                    dialogueTriggers[1].dialogueManager.nameText.text = "You";
                }
                dialogueTriggers[1].ContinueDialogue();
            }
            else
            {
                dialogueTriggers[1].dialogue.name = "You";
                dialogueTriggers[1].TriggerDialogue();
            }
        }
    }

    public void RecieveLight()
    {
        noLight = false;
        hasLight = true;

        if (dialogueTriggers[2].dialogueManager.animator.GetBool("IsOpen") == true)
        {
            if (dialogueTriggers[2].dialogue.name == "You")
            {
                dialogueTriggers[2].dialogue.name = "Red";
                dialogueTriggers[2].dialogueManager.nameText.text = "Red";
            }
            else if (dialogueTriggers[2].dialogue.name == "Red")
            {
                dialogueTriggers[2].dialogue.name = "You";
                dialogueTriggers[2].dialogueManager.nameText.text = "You";
            }
            dialogueTriggers[2].ContinueDialogue();
        }
        else
        {
            dialogueTriggers[2].dialogue.name = "You";
            dialogueTriggers[2].TriggerDialogue();
        }

        if (dialogueTriggers[2].dialogueManager.sentences.Count == 0)
        {
            gameManager.LoadScene("Credits");
        }
    }
}