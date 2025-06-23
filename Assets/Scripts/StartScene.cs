using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    [Header("Main Menu Buttons")]
    [SerializeField] private GameObject _MainMenuButtonsBackground;

    [Header("Credits Text Reference")]
    [SerializeField] private GameObject _creditsTextBackground;

    [Header("Options Text Reference")]
    [SerializeField] private GameObject _optionsTextBackground;

    [Header("Main Camera Zoom Variables")]

    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothTime;
    private Vector3 _offset;

    private Vector3 _currentVelocity = Vector3.zero;
    
    //public Transform GetTarget { get { return _target; } }

    private void Awake()
    {
        //_offset = transform.position - _target.position;
    }

    private void FixedUpdate()
    {
        //Vector3 targetPosition = _target.position + _offset;
        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, _smoothTime);
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        //StartCoroutine(nameof(Intro));
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

    //IEnumerator Intro()
    //{
    //    //yield return new WaitForSeconds(3.0f);
    //}
}
