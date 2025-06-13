using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class PlayerTimer : MonoBehaviour
{
    [Header("Game Timer Text Reference")]
    [SerializeField] private TextMeshProUGUI _timerText;
    [Header("Player Object Reference")]
    [SerializeField] private GameObject _player;

    // The time that has passed
    private float _currentTime;
    private PlayerDeath _playerDeath;
    public float CurrentTime { get { return _currentTime; } }

    void Update()
    {
        //if (_playerDeath.WasCrushed)
        //{
        //    _currentTime = Time.deltaTime;
        //}

        DisplayCurrentTime();
    }

    void DisplayCurrentTime()
    {
        if (!_player)
            return;

        _currentTime += Time.deltaTime;
        float minutes = Mathf.FloorToInt(_currentTime / 60);
        float seconds = Mathf.FloorToInt(_currentTime % 60);
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
