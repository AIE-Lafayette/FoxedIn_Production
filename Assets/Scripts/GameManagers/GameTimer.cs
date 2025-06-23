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
    [Header("Box Spawn Manager Reference")]
    [SerializeField] private GameObject _boxSpawnManager;
    private BoxSpawner _boxSpawner;

    // The time that has passed
    private float _currentTime;
    private float _difficultyIncreaseTime;
    //private float _currentGameSpeed = 0.9f;
    //private int _boxesfalling = 1;
    //private bool _increasedTime;
    //private int _increasedTimeCounter = 0;
    public float CurrentTime { get { return _currentTime; } }

    private void Start()
    {
        _boxSpawner = _boxSpawnManager.GetComponent<BoxSpawner>();
    }

    void Update()
    {
        DisplayCurrentTime();
        DisplayDifficultyIncrease();
        DisplayCurrentDifficulty();
    }

    void DisplayCurrentTime()
    {
        // Guard clause
        if (!_player)
            return;

        // Adding delta time and displaying the current time
        _currentTime += Time.deltaTime;
        float minutes = Mathf.FloorToInt(_currentTime / 60);
        float seconds = Mathf.FloorToInt(_currentTime % 60);
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void DisplayDifficultyIncrease()
    {
        // Guard clause
        if (!_player)
            return;

        // If the current time is greater than the difficulty increase time, increase the difficulty increase timer
        if (_currentTime >= _difficultyIncreaseTime)
        {
            _difficultyIncreaseTime += 10.0f;
        }
        float minutes = Mathf.FloorToInt(_difficultyIncreaseTime / 60);
        float seconds = Mathf.FloorToInt(_difficultyIncreaseTime % 60);

        // Displaying the next time the difficulty will be increased
        _timerDifficultyIncreaseText.text = string.Format("Next Difficulty Increase:  {0:00}:{1:00}", minutes, seconds);
    }

    void DisplayCurrentDifficulty()
    {
        // Guard clause
        if (!_player)
            return;

        //// If time is increased, increase game speed counter
        //if (_increasedTime)
        //{
        //    _increasedTime = false;
        //    _currentGameSpeed += 0.1f;
        //}
        //// If the time is increased 12 times, reset the counter and increase the boxes falling counter
        //if (_increasedTimeCounter >= 30)
        //{
        //    _boxesfalling += 1;
        //    _increasedTimeCounter = 0;
        //}
        // Display the current game speed and the amount of boxes falling

        float spawnrate = _boxSpawner.SpawnRate;
        spawnrate = Mathf.Round(spawnrate * 100) * 0.01f;

        _timerCurrentDifficultyText.text = string.Format("Drop Rate: " + spawnrate + "\n" +
            "Boxes Falling:  " + _boxSpawner.AmountSpawning);
    }
}
