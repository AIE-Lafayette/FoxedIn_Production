using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class PlayerTimer : MonoBehaviour
{
    [Header("Game Timer Text Reference")]
    [SerializeField] private TextMeshProUGUI _timerText;

    // The time that has passed
    private float _currentTime;

    public float CurrentTime { get { return _currentTime; } }

    private void Update()
    {
        DisplayCurrentTime();
    }

    void DisplayCurrentTime()
    {
        _currentTime += Time.deltaTime;
        float minutes = Mathf.FloorToInt(_currentTime / 60);
        float seconds = Mathf.FloorToInt(_currentTime % 60);
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
