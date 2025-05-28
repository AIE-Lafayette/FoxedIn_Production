using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public static PlayerScore instance;

    [SerializeField] private TextMeshProUGUI _playerScore;

    public int _currentScore;

    public void Awake()
    {
        instance = this;
    }

    public void Update()
    {
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
