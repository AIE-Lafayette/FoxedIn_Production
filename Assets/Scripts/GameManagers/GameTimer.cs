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

        float spawnrate = _boxSpawner.SpawnRate;
        spawnrate = Mathf.Round(spawnrate * 100) * 0.01f;

        _timerCurrentDifficultyText.text = string.Format("Drop Rate: " + spawnrate + "\n" + "\n" +
            "Boxes Falling: " + "\n" + _boxSpawner.AmountSpawning);
    }
}
