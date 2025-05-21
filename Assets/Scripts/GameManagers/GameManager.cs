using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject player;
    public GameObject lineCheck;
    public GameObject roofBoxcheck;

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

    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        //Lose case checks
        if (_playerDeath.WasCrushed && player.activeInHierarchy)
        {
            OnPlayerCrushed.Invoke();
        }
        if (_roofBoxManager.BoxAtTop)
        {
            Debug.Log("On Box invoked");
            OnBoxReachesTop.Invoke();
        }


    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}