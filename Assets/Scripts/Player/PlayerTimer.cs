using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;

    // The time that has passed
    private float _currentTime;

    public float CurrentTime { get { return _currentTime; } }

    private void Update()
    {
        //_currentTime += Time.deltaTime;
        ////_currentTime = Mathf.Clamp(_currentTime, 0, _startTime);

        //if (_timerText)
        //{
        //    //_timerText.text = _currentTime.ToString("0");


        //}
        DisplayCurrentTime();
    }

    void DisplayCurrentTime()
    {
        //_currentTime += 1;
        float minutes = Mathf.FloorToInt(_currentTime / 60);
        float seconds = Mathf.FloorToInt(_currentTime % 60);
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
