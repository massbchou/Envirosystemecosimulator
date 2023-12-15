using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTwoManager : MonoBehaviour
{
    //This class is for managing the state of the WORLD TWO and the player's progress in it

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

    int numRabbits;
    int numPlants;
    int numFoxes;

    // Start is called before the first frame update
    void Start()
    {
        timePassed = 0f;
        itemPlacer = FindObjectOfType<ItemPlacer>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        StartCoroutine(CountItemsInScene());
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


        if (timePassed > 10 && !playedFact1)
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

    IEnumerator CountItemsInScene()
    {
        float waitTime = 0.25f;
        while (true)
        {
            GameObject[] rabbits = GameObject.FindGameObjectsWithTag("Rabbit");
            numRabbits = rabbits.Length;
            GameObject[] plants = GameObject.FindGameObjectsWithTag("Plant");
            numPlants = plants.Length;
            GameObject[] foxes = GameObject.FindGameObjectsWithTag("Fox");
            numFoxes = foxes.Length;
            yield return new WaitForSeconds(waitTime);
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
