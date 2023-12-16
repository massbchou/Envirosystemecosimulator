using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public Button startButton;
    void Start()
    {
        //On click of start button, load the scene names Tutorial
        startButton.onClick.AddListener(delegate { SceneManager.LoadScene("Tutorial"); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
