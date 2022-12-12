using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// AUTHOR: @Toni
/// Last modified: 05 Dec. 2022 by @Daniel K.
/// </summary>
/// 
public class PauseScript : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseCanvas;
    [SerializeField]
    private GameObject optionsCanvas;
    [SerializeField] private GameObject gameWonCanvas;
    [SerializeField] private GameObject gameOverCanvas;
    private GameObject playerCharacter;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && gameWonCanvas.activeSelf == false && gameOverCanvas.activeSelf == false)
        {
           if (pauseCanvas.activeInHierarchy)
            {
                continueGame();
                playerCharacter.GetComponent<PlayerShooting>().enabled = true;

            } 
            else
            {
                pauseCanvas.SetActive(true);
                Time.timeScale = 0f;
                playerCharacter.GetComponent<PlayerShooting>().enabled = false;
            }
        }
    }

    public void continueGame()
    {
        optionsCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    public void optionsMenu()
    {
        optionsCanvas.SetActive(true);
    }

    public void closeOptionsMenu()
    {
        optionsCanvas.SetActive(false);
    }

    public void quitGame()
    {
        SceneManager.LoadScene("MainMenu");
        SceneManager.UnloadSceneAsync("Demo Level");
        CustomCursor.SetDefaultCursor();
    }

    public void restartLevel()
    {
        SceneManager.LoadScene("Demo Level");
        Time.timeScale = 1;
    }
}
