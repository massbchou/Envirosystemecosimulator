using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    //This class is for managing the state of the world and the player's progress in it


    float timePassed = 0f;
    [SerializeField] Dialogue funFact1;
    bool played = false;

    // Start is called before the first frame update
    void Start()
    {
        timePassed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;

        if(timePassed > 5 && !played)
        {
            played = true;
            FindObjectOfType<DialogueManager>().StartDialogue(funFact1);
        }
    }
}
