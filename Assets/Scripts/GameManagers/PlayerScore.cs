using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    // Singleton Pattern for the player score, allows for the reference of its own instance in the scene
    public static PlayerScore instance;

    [SerializeField] private TextMeshProUGUI _playerScore;

    private int _currentScore;

    public void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        // Updating the displayed score
        _playerScore.text = "Current Score:  " + _currentScore.ToString();
    }

    public void IncreaseScore(int v)
    {
        _currentScore += v;
    }

    public void SetCurrentScore(int v)
    {
        _currentScore = v;
    }
}
