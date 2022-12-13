using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// AUTHOR: @Toni
/// Last modified: 13 Dec. 2022 by @Daniel K /  @Joona.
/// </summary>
/// 
public class PauseScript : MonoBehaviour
{
    /* EXPOSED FIELDS */
    [SerializeField]
    private GameObject pauseCanvas;
    [SerializeField]
    private GameObject optionsCanvas;
    [SerializeField] private GameObject gameWonCanvas;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject playerCharacter;
    [SerializeField] private GameObject playerGun; // @Daniel K. - 13.Dec.2022

    void Update()
    {
        PauseGame();
    }

    private void PauseGame() // modified. by @Daniel K / @Joona - 12 Dec 2022.
    {
        if (Input.GetKeyUp(KeyCode.Escape) && gameWonCanvas.activeSelf == false && gameOverCanvas.activeSelf == false)
        {
            if (pauseCanvas.activeInHierarchy)
                continueGame();
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
        if(playerGun.activeSelf) // @Daniel K. 13.Dec.2023
            playerCharacter.GetComponent<PlayerShooting>().enabled = true;

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
