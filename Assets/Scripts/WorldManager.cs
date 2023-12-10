using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    //This class is for managing the state of the world and the player's progress in it

    [SerializeField] Dialogue introDialogue;
    bool playedIntro = false;

    float timePassed = 0f;
    bool counting = false;

    [SerializeField] Dialogue funFact1;
    bool playedFact1 = false;

    [SerializeField] Dialogue funFact2;
    bool playedFact2 = false;


    ItemPlacer itemPlacer;
    DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        timePassed = 0f;
        itemPlacer = FindObjectOfType<ItemPlacer>();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dialogueManager.IsInDialogue() && !counting)
        {
            counting = true;
            StartCoroutine(CountTimeNotInDialogue());
        }
        if (dialogueManager.IsInDialogue() && counting)
        {
            counting = false;
            StopCoroutine(CountTimeNotInDialogue());
        }

        if (!playedIntro)
        {
            playedIntro = true;
            dialogueManager.StartDialogue(introDialogue);
            itemPlacer.EnablePlantButton();
        }


        if(timePassed > 10 && !playedFact1)
        {
            playedFact1 = true;
            FindObjectOfType<DialogueManager>().StartDialogue(funFact1);
            itemPlacer.EnableRabbitButton();
        }

        if (timePassed > 20 && !playedFact2)
        {
            playedFact2 = true;
            FindObjectOfType<DialogueManager>().StartDialogue(funFact2);
            itemPlacer.EnableFoxButton();
        }
    }

    IEnumerator CountTimeNotInDialogue()
    {
        float waitTime = 0.1f;
        while (counting)
        {
            yield return new WaitForSeconds(waitTime);
            timePassed += waitTime;
        }
        
    }
}
