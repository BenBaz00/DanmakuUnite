using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
 * DialogueTrigger determines when the DialogueManager should open a text window based on waves
 * Dialogue includes a wave, name and sentences. DialogueTrigger opens a json with an array of dialogues
 * and triggers a dialogue when the corresponding wave is met
 */

public class DialogueTrigger : MonoBehaviour
{
    public TextAsset jsonFile;
    private DialoguePage page;

    private int nextDialogue = 0;

    private void Start()
    {
        page = JsonUtility.FromJson<DialoguePage>(jsonFile.text);
    }

    private void Update()
    {

        // Checks if the next dialogue corresponds to the wave number
        if (nextDialogue < page.Dialogues.Count)
        {
            var currDialogue = page.Dialogues[nextDialogue];
            if (currDialogue.wave <= LevelEditor.waveNumber)
            {
                TriggerDialogue();
            }
        }
    }

    // TriggerDialogue()
    // --Sends the dialogue to the manager to handle dialogueBox and sentences
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().BeginDialogue(page.Dialogues[nextDialogue]);
        nextDialogue++;
    }
}
