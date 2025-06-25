using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject player;
    public GameObject lineCheck;
    public GameObject roofBoxcheck;
    private GameObject controlLayoutSelectorObject;

    [Header("Ending Events")]
    public UnityEvent OnPlayerCrushed;
    public UnityEvent OnBoxReachesTop;

    [Header("Pausing Events")]
    public UnityEvent OnGamePause;
    public UnityEvent OnGameResume;

    //Player Scripts
    private PlayerDeath _playerDeath;
    private PlayerInput _playerInput;
    private PlayerMovement _playerMovement;
    private PlayerTailSwipe _playerTailSwipe;

    //Trigger based box managers
    private LineClearingManager _lineManager;
    private RoofBoxManager _roofBoxManager;

    [Header("End Screen Reference")]
    [SerializeField]
    private GameObject _endScreenTextBackground;

    int _layout;
    bool _hellMode = false;

    public bool HellModeEnabled { get { return _hellMode; } }

    // Start is called before the first frame update
    void Start()
    {
        controlLayoutSelectorObject = GameObject.Find("LayoutSelector");

        if (controlLayoutSelectorObject != null)
        {
            _layout = controlLayoutSelectorObject.GetComponent<ControlLayoutSelector>().ControlScheme;
            _hellMode = controlLayoutSelectorObject.GetComponent<HellCheck>().HellModeEnabled;
        }

        //Player Script checks
        if (!(player.TryGetComponent<PlayerDeath>(out _playerDeath)))
            Debug.LogError("GameManager: Start, Could not get _playerDeath");
        if (!(player.TryGetComponent<PlayerInput>(out _playerInput)))
            Debug.LogError("GameManager: Start, Could not get _playerInput");
        if (!(player.TryGetComponent<PlayerMovement>(out _playerMovement)))
            Debug.LogError("GameManager: Start, Could not get _playerMovement");
        if (!(player.TryGetComponent<PlayerTailSwipe>(out _playerTailSwipe)))
            Debug.LogError("GameManager: Start, Could not get _playerTailSwipe");

        //Line Manager script checks
        if (!(lineCheck.TryGetComponent<LineClearingManager>(out _lineManager)))
            Debug.LogError("GameManager: Start, Could not get _lineManager");

        //Roof box scripts checks
        if (!(roofBoxcheck.TryGetComponent<RoofBoxManager>(out _roofBoxManager)))
            Debug.LogError("GameManager: Start, Could not get _roofBoxManager");

        //Set player controls
        switch (_layout)
        {
            //Control scheme 1 (W to jump SPACE to tailswipe)
            case 1:
                _playerInput.actions.FindActionMap("Player").Enable();
                _playerInput.actions.FindActionMap("Player1").Disable();
                break;

            //Control scheme 2 (SPACE to jump W to tailswipe)
            case 2:
                _playerInput.actions.FindActionMap("Player1").Enable();
                _playerInput.actions.FindActionMap("Player").Disable();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Lose case checks
        if (_playerDeath.WasCrushed && player.activeInHierarchy)
        {
            OnPlayerCrushed.Invoke();
            RestartGame();
        }
        if (_roofBoxManager.BoxAtTop)
        {
            OnBoxReachesTop.Invoke();
            RestartGame();
        }


    }

    public void RestartGame()
    {
        _endScreenTextBackground.SetActive(true);
    }
}