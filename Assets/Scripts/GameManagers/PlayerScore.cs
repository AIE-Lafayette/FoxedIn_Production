using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    // Singleton Pattern for the player score, allows for the reference of its own instance in the scene
    public static PlayerScore instance;

    [Header("Player Score References")]
    [SerializeField] private TextMeshProUGUI _playerScoreEarned;
    [SerializeField] private TextMeshProUGUI _playerScore;

    private int _currentScore;
    private int _scoreEarned;


    public void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        // Updating the displayed score
        _playerScore.text = "Current Score: " + _currentScore.ToString();
        _playerScoreEarned.text = _scoreEarned.ToString();
    }

    public void IncreaseScore(int v)
    {
        _currentScore += v;
    }

    public void DisplayGainedScore(int v)
    {
        _scoreEarned = v;
    }
}
