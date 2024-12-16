using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueStarter : MonoBehaviour
{
    public DialogueRunner dialogueRunner;

    public string dialogueNodeName;

    public void OnActivate()
    {
        if (dialogueRunner != null)
        {
            if (dialogueRunner.IsDialogueRunning)
            {
                dialogueRunner.Stop();
            }
            dialogueRunner.StartDialogue(dialogueNodeName);
        }
        else
        {
            Debug.LogWarning("DialogueRunner component not assigned!");
        }

    }
}
