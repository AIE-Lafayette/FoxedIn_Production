using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    // Internal timer
    private float _startTime = 0.0f;
    // The time that has passed in whole seconds
    private float _currentTime;
    private float _currentTimeMinutes;

    public float CurrentTime { get { return _currentTime; } }
    public float CurrentTimeMinutes { get { return _currentTimeMinutes; } }

    private void Start()
    {
        _currentTime = _startTime;
        //if (_timerText)
        //{
        //    _timerText.text = _currentTime.ToString("0");
        //}
    }

    private void Update()
    {
        //_currentTime += Time.deltaTime;
        ////_currentTime = Mathf.Clamp(_currentTime, 0, _startTime);

        //if (_timerText)
        //{
        //    _timerText.text = _currentTimeMinutes.ToString("0");
            
        //}
        

        //if (_currentTime >= 60.0f)
        //{
        //    _timerText.text = _currentTime.ToString("1: 00");
        //}
    }
    //float minutes = Mathf.FloorToInt(timeToDisplay / 60);
    //float seconds = Mathf.FloorToInt(timeToDisplay % 60);

    //void AddMinute()
    //{
    //    if (_currentTime >= 60.0f)
    //        {
    //            _currentTimeMinutes += 1.0f;
    //        }
    //}

}
