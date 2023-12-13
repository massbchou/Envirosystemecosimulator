using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public AudioSource audioSource;
    [SerializeField] Animator animator;

    public Queue<string> sentences; 

    public Queue<AudioClip> audioClips;
    private AudioClip audioClip;
    public bool IsInDialogue()
    {
        return animator.GetBool("isOpen");
    }

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        audioClips = new Queue<AudioClip>();
        audioClip = null;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();


        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        foreach(AudioClip audioClip in dialogue.audioClips)
        {
            audioClips.Enqueue(audioClip);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        if (audioClips.Count == 0)
            audioClip = null;
        else
            audioClip = audioClips.Dequeue();
        
        if (audioClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    private void EndDialogue()
    {
        animator.SetBool("isOpen", false);
    }
}
