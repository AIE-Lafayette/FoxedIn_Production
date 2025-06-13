using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _player;

    private void Start()
    {
        _pauseMenu.SetActive(false);
    }

    private void Update()
    {
        // Whenever the escape key is pressed, display the pause menu and freeze the game.
        if (Input.GetKey(KeyCode.Escape))
        {
            _pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    // Whenever the resume button is pressed, disable the pause menu and unfreeze the game.
    public void Resume()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Retry()
    {

        SceneManager.LoadScene("SampleScene");
    }
}
