using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WorldTwoManager : MonoBehaviour
{
    //This class is for managing the state of the world and the player's progress in it


    [SerializeField] Dialogue funFact0;
    bool playedFact0 = false;

    float timePassedSinceLastDialogue = 0f;
    bool counting = false;

    [SerializeField] Dialogue funFact1;
    bool playedFact1 = false;


    [SerializeField] Dialogue winDialogue;
    bool playedWinDialogue = false;

    [SerializeField] Dialogue loseDialogue;
    bool playedLoseDialogue = false;


    ItemPlacer itemPlacer;
    DialogueManager dialogueManager;
    GrowPlants plantGrower;


    int numRabbits = 0;
    int numPlants = 0;
    int numFoxes = 0;
    int numSnakes = 0;

    [SerializeField] Button continueButton;

    [SerializeField] Text plantCounter;
    [SerializeField] Text rabbitCounter;
    [SerializeField] Text foxCounter;
    [SerializeField] Text snakeCounter;

    [SerializeField] Text stabilityText;

    [SerializeField] int timeToMakeStable = 20;
    [SerializeField] int timeToKeepStable = 60;
    [SerializeField] int timeToKillRabbits = 30;

    // Start is called before the first frame update
    void Start()
    {
        timePassedSinceLastDialogue = 0f;
        plantGrower = FindObjectOfType<GrowPlants>();

        itemPlacer = FindObjectOfType<ItemPlacer>();
        dialogueManager = FindObjectOfType<DialogueManager>();

        stabilityText.gameObject.SetActive(false);

        continueButton.onClick.AddListener(GoToNextLevel);
        continueButton.gameObject.SetActive(false);

        StartCoroutine(CountItemsInScene());

        plantCounter.text = "Plants: 0";
        rabbitCounter.text = "Rabbits: 0";
        foxCounter.text = "";
        snakeCounter.text = "Snakes: 0";

    }

    // Update is called once per frame
    void Update()
    {
        //start or stop counter
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

        //introduce sneks
        if (!playedFact0)
        {
           // Debug.Log("here");
            playedFact0 = true;
            dialogueManager.StartDialogue(funFact0);
           // itemPlacer.EnableFoxButton();
            itemPlacer.EnablePlantButton();
            itemPlacer.EnableSnakeButton();

            timePassedSinceLastDialogue = 0f;
            StartCoroutine(KillRabbitsCounter());

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

           /* GameObject[] foxes = GameObject.FindGameObjectsWithTag("Fox");
            numFoxes = foxes.Length;*/
          //  foxCounter.text = "Foxes: " + numFoxes.ToString() + " (Available: " + itemPlacer.numFoxesAvailable + ")";
            
            GameObject[] snakes = GameObject.FindGameObjectsWithTag("Snake");
            numSnakes = snakes.Length;
            snakeCounter.text = "Snakes: " + numSnakes.ToString();

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

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    void GoToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    IEnumerator KillRabbitsCounter()
    {
        while (dialogueManager.IsInDialogue())
        {
            yield return null;
        }

        stabilityText.gameObject.SetActive(true);

        while (timeToKillRabbits > 0)
        {
            stabilityText.text = "Time to reduce rabbits: " + timeToKillRabbits;
            timeToKillRabbits -= 1;

            if (numRabbits < 3)
            {
                break;
            }

            yield return new WaitForSeconds(1);
        }

        if (numRabbits < 3)
        {
            playedFact1 = true;
            FindObjectOfType<DialogueManager>().StartDialogue(funFact1);
            itemPlacer.EnableRabbitButton();

            timePassedSinceLastDialogue = 0f;
            StartCoroutine(MakeStableCounter());

        }
        else
        {
            playedLoseDialogue = true;
            FindObjectOfType<DialogueManager>().StartDialogue(loseDialogue);
            timePassedSinceLastDialogue = 0f;
            continueButton.gameObject.SetActive(true);


            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(RestartLevel);
        }
    }

    IEnumerator MakeStableCounter()
    {
        plantGrower.growDelay = 4;
        while (dialogueManager.IsInDialogue())
        {
            yield return null;
        }

        while(timeToMakeStable > 0)
        {
            stabilityText.text = "Time to make stable: " + timeToMakeStable;
            timeToMakeStable -= 1;
            yield return new WaitForSeconds(1);
        }

        StartCoroutine(KeepStableCounter());
    }

    IEnumerator KeepStableCounter()
    {
        while (dialogueManager.IsInDialogue())
        {
            yield return null;
        }

        while (timeToKeepStable > 0)
        {
            CheckLoss();

            stabilityText.text = "Time to keep stable: " + timeToKeepStable;
            timeToKeepStable -= 1;
            yield return new WaitForSeconds(1);
        }


        playedWinDialogue = true;
        FindObjectOfType<DialogueManager>().StartDialogue(winDialogue);
        timePassedSinceLastDialogue = 0f;
        continueButton.gameObject.SetActive(true);
    }

    void CheckLoss()
    {
        if (numRabbits <= 2 || numRabbits > 100 || numSnakes >= 20 || numSnakes <= 0)
        {
            StopAllCoroutines();
            playedLoseDialogue = true;
            FindObjectOfType<DialogueManager>().StartDialogue(loseDialogue);
            timePassedSinceLastDialogue = 0f;
            continueButton.gameObject.SetActive(true);

            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(RestartLevel);

        }
    }
}
