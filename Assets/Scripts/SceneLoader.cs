using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// AUTHOR: @Toni
/// Last modified: 05 Dec. 2022 by @Daniel K.
/// </summary>
/// 
public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject creditCanvas;
    [SerializeField]
    private GameObject optionCanvas;

    [SerializeField]
    private Button newGameButton;
    [SerializeField]
    private Button optionsButton;
    [SerializeField]
    private Button creditsButton;
    [SerializeField]
    private Button continueButton;

    [SerializeField]
    private AudioClip gameStartSound;

    public void NewGame()
    {
        SoundManager.instance.PlaySingle(gameStartSound);
        SceneManager.LoadScene("Demo Level");
        Time.timeScale = 1;
    }

    public void Options()
    {
        optionCanvas.SetActive(true);
        newGameButton.interactable = false;
        optionsButton.interactable = false;
        creditsButton.interactable = false;
        continueButton.interactable = false;
    }

    public void CloseOptions()
    {
        optionCanvas.SetActive(false);
        newGameButton.interactable = true;
        optionsButton.interactable = true;
        creditsButton.interactable = true;
        continueButton.interactable = true;
    }

    public void Credits()
    {
        creditCanvas.SetActive(true);
        newGameButton.interactable = false;
        optionsButton.interactable = false;
        creditsButton.interactable = false;
        continueButton.interactable = false;
    }

    public void CloseCredits()
    {
        creditCanvas.SetActive(false);
        newGameButton.interactable = true;
        optionsButton.interactable = true;
        creditsButton.interactable = true;
        continueButton.interactable = true;
    }

}
