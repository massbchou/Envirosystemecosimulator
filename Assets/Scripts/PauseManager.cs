using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject playButton;

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
            playButton.SetActive(true);
            //on click of play button, set isPaused to false and timeScale to 1
            playButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { TogglePause(); });
            

        }
        else
        {
            playButton.SetActive(false);
            Time.timeScale = 1f; 
            
        }
    }
}