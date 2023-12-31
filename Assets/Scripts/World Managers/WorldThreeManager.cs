using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WorldThreeManager : MonoBehaviour
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

    int numRabbits = 0;
    int numPlants = 0;
    int numFoxes = 0;
    int numRats = 0;
    int numSnakes = 0;
    [SerializeField] Button continueButton;

    [SerializeField] Text plantCounter;
    [SerializeField] Text rabbitCounter;
    [SerializeField] Text foxCounter;
    [SerializeField] Text ratCounter;
    [SerializeField] Text snakeCounter;

    [SerializeField] Text stabilityText;

    [SerializeField] int timeToMakeStable = 20;
    [SerializeField] int timeToKeepStable = 60;

    private int timeSinceStable = 0;
    private bool keepStable = false;

    // Start is called before the first frame update
    void Start()
    {
        timePassedSinceLastDialogue = 0f;

        itemPlacer = FindObjectOfType<ItemPlacer>();
        dialogueManager = FindObjectOfType<DialogueManager>();

        itemPlacer.numFoxesAvailable = 10000;

        stabilityText.gameObject.SetActive(false);

        continueButton.onClick.AddListener(GoToNextLevel);
        continueButton.gameObject.SetActive(false);

        StartCoroutine(CountItemsInScene());

        plantCounter.text = "Plants: 0";
        rabbitCounter.text = "Rabbits: 0";
        foxCounter.text = "Foxes: " + numFoxes.ToString();

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

        //introduce rats
        if (!playedFact0)
        {
            playedFact0 = true;
            dialogueManager.StartDialogue(funFact0);
            // itemPlacer.EnableFoxButton();
            // itemPlacer.EnableRabbitButton();
            itemPlacer.EnablePlantButton();
            itemPlacer.EnableRatButton();
            timePassedSinceLastDialogue = 0f;
        }

        //Player hasn't reduced rabbits in time
        if (playedFact0 && !playedFact1 && timePassedSinceLastDialogue > 20)
        {
            playedFact1 = true;
            FindObjectOfType<DialogueManager>().StartDialogue(funFact1);
            timePassedSinceLastDialogue = 0f;
            itemPlacer.EnableFoxButton();
            itemPlacer.EnableRabbitButton();
            itemPlacer.EnableSnakeButton();
        }

        if (playedFact0 && playedFact1 && !playedWinDialogue && !keepStable)
        {
            StartCoroutine(KeepStableCounter());
            keepStable = true;
        }

        // if (playedFact0 && playedFact1 && isStable())
        // {
        //     if (timeSinceStable > timeToKeepStable)
        //     {
        //         playedWinDialogue = true;
        //         FindObjectOfType<DialogueManager>().StartDialogue(winDialogue);
        //         timePassedSinceLastDialogue = 0f;
        //         continueButton.gameObject.SetActive(true);
        //     }
        //     else
        //     {
        //         timeSinceStable += 1;
        //     }
        
        // }
           

        // //TODO: GIVE PLAYER BURROWS TO PLACE HERE
        // if (!playedFact1 && playedFact0 && numRabbits <= 3)
        // {
        //     playedFact1 = true;
        //     FindObjectOfType<DialogueManager>().StartDialogue(funFact1);
        //     timePassedSinceLastDialogue = 0f;

        //     StartCoroutine(MakeStableCounter());
        // }

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

            GameObject[] foxes = GameObject.FindGameObjectsWithTag("Fox");
            numFoxes = foxes.Length;
            foxCounter.text = "Foxes: " + numFoxes.ToString();

            GameObject[] rats = GameObject.FindGameObjectsWithTag("Rat");
            numRats = rats.Length;
            ratCounter.text = "Rats: " + numRats.ToString();

            GameObject[] snakes = GameObject.FindGameObjectsWithTag("Snake");
            numSnakes = snakes.Length;
            snakeCounter.text = "Snakes: " + numSnakes.ToString();

            yield return new WaitForSeconds(waitTime);
        }
    }

    bool isStable()
    {
        if (numRabbits >=1  && numFoxes >=1 && numRats >= 1 && numSnakes >=1 && numPlants >= 1)
        {
            return true;
        }
        else
        {
            return false;
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

    IEnumerator KeepStableCounter()
    {
        
        
        stabilityText.gameObject.SetActive(true);
        

        while (timeToKeepStable > 0)
        {
            if (!isStable())
            {
                timeToKeepStable = 60;
                // StartCoroutine(MakeStableCounter());
                stabilityText.text = "Time to keep stable: " + timeToKeepStable;
                yield return new WaitForSeconds(1);
            }

            stabilityText.text = "Time to keep stable: " + timeToKeepStable;
            timeToKeepStable -= 1;
            yield return new WaitForSeconds(1);
        }


        playedWinDialogue = true;
        FindObjectOfType<DialogueManager>().StartDialogue(winDialogue);
        timePassedSinceLastDialogue = 0f;
        continueButton.gameObject.SetActive(true);
    }

    // void CheckLoss()
    // {
    //     if (numRabbits <= 2 || numRabbits > 100 || numFoxes >= 20 || numFoxes <= 0)
    //     {
    //         StopAllCoroutines();
    //         playedLoseDialogue = true;
    //         FindObjectOfType<DialogueManager>().StartDialogue(loseDialogue);
    //         timePassedSinceLastDialogue = 0f;
    //         continueButton.gameObject.SetActive(true);

    //         continueButton.onClick.RemoveAllListeners();
    //         continueButton.onClick.AddListener(RestartLevel);

    //     }
    // }
}

