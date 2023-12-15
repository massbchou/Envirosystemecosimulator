using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject playButton;
    public GameObject exitButton;

   
    private void Start()
    {
        isPaused = false;
        playButton.SetActive(false);
        exitButton.SetActive(false);
        playButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { TogglePause(); });
        exitButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { HandleExit(); });
    }

    void Update()
    {
        //Input is P for pause
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Pause the game by setting time scale to zero    
            exitButton.SetActive(true);
            playButton.SetActive(true);
        }
        else
        {
            playButton.SetActive(false);
            exitButton.SetActive(false);
            Time.timeScale = 1f; 
            
        }
    }

    void HandleExit(){
        playButton.SetActive(false);
        exitButton.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}