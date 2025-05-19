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
    public void PlayGame()
    {
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
        _creditsTextBackground.SetActive(false);
        _MainMenuButtonsBackground.SetActive(true);
    }

    public void DisplayOptions()
    {

    }
}
