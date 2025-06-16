using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    [Header("Main Menu Buttons")]
    [SerializeField] private GameObject _MainMenuButtonsBackground;

    [Header("Credits Text Reference")]
    [SerializeField] private GameObject _creditsTextBackground;

    [Header("Options Text Reference")]
    [SerializeField] private GameObject _optionsTextBackground;

    [Header("Scoreboard Text Reference")]
    [SerializeField] private GameObject _scoreboardTextBackground;

    public void PlayGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void DisplayCredits()
    {
        _MainMenuButtonsBackground.SetActive(false);
        _creditsTextBackground.SetActive(true);
    }

    public void ExitCredits()
    {
        _MainMenuButtonsBackground.SetActive(true);
        _creditsTextBackground.SetActive(false);
    }

    public void DisplayOptions()
    {
        _MainMenuButtonsBackground.SetActive(false);
        _optionsTextBackground.SetActive(true);
    }

    public void ExitOptions()
    {
        _MainMenuButtonsBackground.SetActive(true);
        _optionsTextBackground.SetActive(false);
    }

    public void DisplayScoreboard()
    {
        _MainMenuButtonsBackground.SetActive(false);
        _scoreboardTextBackground.SetActive(true);
    }

    public void ExitScoreboard()
    {
        _MainMenuButtonsBackground.SetActive(true);
        _scoreboardTextBackground.SetActive(false);
    }
}
