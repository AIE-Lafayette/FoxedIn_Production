using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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

    bool _hellModeActive;
    int _hellResetCheck;
    int _hellChecker;

    public bool HellModeActive { get { return _hellModeActive; } }
    //public Transform GetTarget { get { return _target; } }

    private void Awake()
    {
        _hellModeActive = false;
        //_offset = transform.position - _target.position;
    }

    private void FixedUpdate()
    {
        Debug.Log("Hell Mode: " + _hellModeActive);
        //Vector3 targetPosition = _target.position + _offset;
        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, _smoothTime);
    }

    private void Update()
    {
        HellModeCheck();
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

    private void HellModeCheck()
    {

        if(_creditsTextBackground.activeInHierarchy)
        {
        }
        else
        {
            HellModeCheckReset();
            return;
        }

        //ENTER 11
        if (Input.GetKey(KeyCode.Return) && _hellChecker == 10)
        {
            Debug.Log("ENTER");
            _hellModeActive = true;
            _hellChecker = 11;
            _hellResetCheck = _hellChecker;
        }
        //A 10
        else if (Input.GetKey(KeyCode.A) && _hellChecker == 9)
        {
            Debug.Log("A");
            _hellChecker = 10;
            _hellResetCheck = _hellChecker;
        }
        //B 9
        else if (Input.GetKey(KeyCode.B) && _hellChecker == 8)
        {
            Debug.Log("B");
            _hellChecker = 9;
            _hellResetCheck = _hellChecker;
        }
        //Right 8
        else if (Input.GetKey(KeyCode.RightArrow) && _hellChecker == 7)
        {
            Debug.Log("RIGHT");
            _hellChecker = 8;
            _hellResetCheck = _hellChecker;
        }
        //Left 7
        else if (Input.GetKey(KeyCode.LeftArrow) && _hellChecker == 6)
        {
            Debug.Log("LEFT");
            _hellChecker = 7;
            _hellResetCheck = _hellChecker;
        }
        //Right 6
        else if (Input.GetKey(KeyCode.RightArrow) && _hellChecker == 5)
        {
            Debug.Log("RIGHT");
            _hellChecker = 6;
            _hellResetCheck = _hellChecker;
        }
        //Left 5
        else if (Input.GetKey(KeyCode.LeftArrow) && _hellChecker == 4)
        {
            Debug.Log("LEFT");
            _hellChecker = 5;
            _hellResetCheck = _hellChecker;
        }
        //Down 4
        else if (Input.GetKey(KeyCode.DownArrow) && _hellChecker == 3)
        {
            Debug.Log("DOWN");
            _hellChecker = 4;
            _hellResetCheck = _hellChecker;
        }
        //Down 3
        else if (Input.GetKey(KeyCode.DownArrow) && _hellChecker == 2)
        {
            Debug.Log("DOWN");
            _hellChecker = 3;
            _hellResetCheck = _hellChecker;
        }
        //Up 2
        else if (Input.GetKey(KeyCode.UpArrow) && _hellChecker == 1)
        {
            Debug.Log("UP");
            _hellChecker = 2;
            _hellResetCheck = _hellChecker;
        }
        //Up 1
        else if(Input.GetKey(KeyCode.UpArrow) && _hellChecker == 0)
        {
            Debug.Log("UP");
            _hellChecker = 1;
            _hellResetCheck = _hellChecker;
            Invoke(nameof(HellModeCheckReset), 5f);
        }
    }

    private void HellModeCheckReset()
    {
        Debug.Log("_hellResetCheck");
        if (_hellResetCheck == _hellChecker)
        {
            return;
        }

        _hellChecker = 0;
    }
}
