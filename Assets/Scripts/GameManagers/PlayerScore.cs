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
    [SerializeField] private GameObject _scoreEarnedImage;

    private int _currentScore;
    private int _scoreEarned;
    private float _displayLength = 5.0f;
    private bool _displayActive = false;

    public void Awake()
    {
        instance = this;
        _scoreEarnedImage.SetActive(false);
    }

    public void Update()
    {
        // Updating the displayed score
        _playerScore.text = "Current Score: " + _currentScore.ToString();
        _playerScoreEarned.text = "+" + _scoreEarned.ToString();

        if (_displayActive)
        {
            StartCoroutine(DeativateScore());
            _displayActive = false;
        }
    }

    public void IncreaseScore(int v)
    {
        _currentScore += v;
    }

    public void DisplayGainedScore(int v)
    {
        _scoreEarnedImage.SetActive(true);
        _scoreEarned = v;
        _displayLength -= Time.deltaTime;
        _displayActive = true;
    }

    IEnumerator DeativateScore()
    {
        yield return new WaitForSeconds(_displayLength);
        _scoreEarnedImage.SetActive(false);
    }

}
