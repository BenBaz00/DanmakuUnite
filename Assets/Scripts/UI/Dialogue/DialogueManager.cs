using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Animator animator;
    public GameObject InitialSelectedBtn;

    private Queue<string> sentenceQueue;

    // Start is called before the first frame update
    void Start()
    {
        sentenceQueue = new Queue<string>();
    }

    // BeginDialogue()
    // -- Freeze time and open dialogue box with name of speaker and text
    public void BeginDialogue(Dialogue dialogue)
    {
        Debug.Log("DialogueManager: BeginDialogue with: " + dialogue.name);

        Time.timeScale = 0f;
        PauseMenu.allowPause = false;

        // Parameter within animator, IsOpen is a boolean that determines when dialogue box is shown
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;

        sentenceQueue.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentenceQueue.Enqueue(sentence);
        }
        DisplayNextSentence();

        // Clear Selected Button and set to initial.
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(InitialSelectedBtn);
    }

    // DisplayNextSentence()
    // --Continue to next string in dialogue's sentence queue
    public void DisplayNextSentence()
    {
        if (sentenceQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentenceQueue.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    // TypeSentence()
    // --Way to animate text being placed character-by-character on the dialogue box
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    // EndDialogue()
    // --Unfreeze time and close dialogue box
    void EndDialogue()
    {
        EventSystem.current.SetSelectedGameObject(null);
        animator.SetBool("IsOpen", false);

        Time.timeScale = 1f;
        PauseMenu.allowPause = true;
    }
}
