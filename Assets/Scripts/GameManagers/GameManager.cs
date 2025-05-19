using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;

    [Header("Player Events")]
    public UnityEvent OnPlayerCrushed;

    private PlayerDeath _playerDeath;

    [Header("End Screen Reference")]
    [SerializeField]
    private GameObject _endScreenTextBackground;

    // Start is called before the first frame update
    void Start()
    {
        if (!(player.TryGetComponent<PlayerDeath>(out _playerDeath)))
            Debug.LogError("GameManager: Start, Could not get _playerDeath");
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerDeath.WasCrushed && player.activeInHierarchy)
        {
            OnPlayerCrushed.Invoke();
            RestartGame();
        }
    }

    public void RestartGame()
    {
        _endScreenTextBackground.SetActive(true);
    }
}