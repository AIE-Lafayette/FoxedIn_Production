using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class PlayerTimer : MonoBehaviour
{
    [Header("Game Timer Text Reference")]
    [SerializeField] private TextMeshProUGUI _timerText;
    [Header("Time to difficulty increase")]
    [SerializeField] private TextMeshProUGUI _timerDifficultyIncreaseText;
    [Header("Current difficulty")]
    [SerializeField] private TextMeshProUGUI _timerCurrentDifficultyText;
    [Header("Player Object Reference")]
    [SerializeField] private GameObject _player;

    // The time that has passed
    private float _currentTime;
    private float _currentGameSpeed = 1.10f;
    private float _boxesfalling = 2;
    private float _increaseTime = 1;
    public float CurrentTime { get { return _currentTime; } }

    void Update()
    {
        //if (_playerDeath.WasCrushed)
        //{
        //    _currentTime = Time.deltaTime;
        //}

        DisplayCurrentTime();
        DisplayDifficultyIncrease();
        DisplayCurrentDifficulty();
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

    void DisplayDifficultyIncrease()
    {
        if (!_player)
            return;

        _timerDifficultyIncreaseText.text = string.Format("Next Difficulty Increase: " + _increaseTime);
    }

    void DisplayCurrentDifficulty()
    {
        if (!_player)
            return;

        _timerCurrentDifficultyText.text = string.Format("Game Speed: " + _currentGameSpeed + "    Boxes Falling:  " + _boxesfalling);
        //_timerCurrentDifficultyText.text = string.Format("{0}:{3}" + "{1}:{2}", _currentGameSpeed, _boxesfalling);
    }
}
