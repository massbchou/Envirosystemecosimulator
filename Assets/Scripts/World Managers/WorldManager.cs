using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    //This class is for managing the state of the world and the player's progress in it

    [SerializeField] Dialogue funFact0;
    bool playedFact0 = false;

    float timePassedSinceLastDialogue = 0f;
    bool counting = false;

    [SerializeField] Dialogue funFact1;
    bool playedFact1 = false;

    [SerializeField] Dialogue movementTutorial;
    bool playedMovementTutorial = false;

    [SerializeField] Dialogue funFact2;
    bool playedFact2 = false;

    [SerializeField] Dialogue funFact3;
    bool playedFact3 = false;

    ItemPlacer itemPlacer;
    DialogueManager dialogueManager;

    int numRabbits;
    int numPlants;
    int numFoxes;

    [SerializeField] Button continueButton;

    [SerializeField] Text plantCounter;
    [SerializeField] Text rabbitCounter;
    [SerializeField] Text foxCounter;

    GrowPlants plantGrower;


    // Start is called before the first frame update
    void Start()
    {
        timePassedSinceLastDialogue = 0f;

        itemPlacer = FindObjectOfType<ItemPlacer>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        plantGrower = FindObjectOfType<GrowPlants>();

        plantGrower.shouldGrowPlants = false;

        continueButton.onClick.AddListener(GoToNextLevel);
        continueButton.gameObject.SetActive(false);

        StartCoroutine(CountItemsInScene());

        plantCounter.text = "Plants: 0";
        rabbitCounter.text = "Rabbits: 0";
        foxCounter.text = "Foxes: 0";

    }

    // Update is called once per frame
    void Update()
    {
        if (!dialogueManager.IsInDialogue() && !counting)
        {
            timePassedSinceLastDialogue = 0f;
            counting = true;
            StartCoroutine(CountTimeNotInDialogue());
        }
        if (dialogueManager.IsInDialogue() && counting)
        {
            counting = false;
            StopCoroutine(CountTimeNotInDialogue());
        }

        //introduce world and give player plants
        if (!playedFact0)
        {
            playedFact0 = true;
            dialogueManager.StartDialogue(funFact0);
            itemPlacer.EnablePlantButton();
            timePassedSinceLastDialogue = 0f;
        }

        //give player rabbits
        if (timePassedSinceLastDialogue > 20 && !playedFact1)
        {
            playedFact1 = true;
            FindObjectOfType<DialogueManager>().StartDialogue(funFact1);
            itemPlacer.EnableRabbitButton();
            timePassedSinceLastDialogue = 0f;
        }

        //movement tutorial
        if (timePassedSinceLastDialogue > 20 && !playedMovementTutorial)
        {
            playedMovementTutorial = true;
            FindObjectOfType<DialogueManager>().StartDialogue(movementTutorial);
            timePassedSinceLastDialogue = 0f;
        }

        //give player foxes
        if (timePassedSinceLastDialogue > 20 && !playedFact2)
        {
            playedFact2 = true;
            FindObjectOfType<DialogueManager>().StartDialogue(funFact2);
            itemPlacer.EnableFoxButton();
            timePassedSinceLastDialogue = 0f;
        }

        //END TUTORIAL
        if (timePassedSinceLastDialogue > 30 && !playedFact3)
        {
            playedFact3 = true;
            FindObjectOfType<DialogueManager>().StartDialogue(funFact3);
            continueButton.gameObject.SetActive(true);
            timePassedSinceLastDialogue = 0f;
        }
    }

    IEnumerator CountItemsInScene()
    {
        float waitTime = 0.20f;
        while (true)
        {
            GameObject[] rabbits = GameObject.FindGameObjectsWithTag("Rabbit");
            numRabbits = rabbits.Length;
            rabbitCounter.text = "Rabbits: " + numRabbits.ToString();

            GameObject[] plants = GameObject.FindGameObjectsWithTag("Plant");
            numPlants = plants.Length;
            plantCounter.text = "Plants: " + numPlants.ToString();

            if(numPlants >= 2)
            {
                plantGrower.shouldGrowPlants = true;
            }
            else
            {
                plantGrower.shouldGrowPlants = false;
            }

            GameObject[] foxes = GameObject.FindGameObjectsWithTag("Fox");
            numFoxes = foxes.Length;
            foxCounter.text = "Foxes: " + numFoxes.ToString();

            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator CountTimeNotInDialogue()
    {
        float waitTime = 0.1f;
        while (counting)
        {
            yield return new WaitForSeconds(waitTime);
            timePassedSinceLastDialogue += waitTime;
        }
    }

    void GoToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
