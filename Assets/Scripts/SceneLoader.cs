using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// AUTHOR: @Toni
/// Last modified: 11 Dec. 2022 by @Toni N
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

    [SerializeField]
    private GameObject spaceShip;
    private Animator spaceShipAnimation;
    [SerializeField]
    private Image black;

    private void Start()
    {
        spaceShipAnimation = spaceShip.GetComponent<Animator>();
        black.enabled = false;
    }

    public void NewGameButton()
    {
        Time.timeScale = 1;
        newGameButton.interactable = false;
        optionsButton.interactable = false;
        creditsButton.interactable = false;
        black.enabled = true;
        spaceShipAnimation.Play("NewGame_Animation");
        SoundManager.instance.PlaySingle(gameStartSound);
        StartCoroutine(NewGameWait());
    }

    //Fade out effect
    IEnumerator NewGameWait()
    {
        Color curColor = black.color;
        while (Mathf.Abs(curColor.a - 1f) > 0.0001f)
        {
            curColor.a = Mathf.Lerp(curColor.a, 1f, Time.deltaTime);
            black.color = curColor;
            yield return null;
        }
        curColor.a = 1f; 
        black.color = curColor;
        SceneManager.LoadScene("Demo Level");
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
