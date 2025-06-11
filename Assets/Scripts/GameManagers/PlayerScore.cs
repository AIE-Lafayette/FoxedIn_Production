using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    // Singleton Pattern for the player score, allows for the reference of its own instance in the scene
    public static PlayerScore instance;

    [Header("Player Score References")]
    [SerializeField] private TextMeshProUGUI _playerScore;
    [SerializeField] private GameObject _scoreEarnedImage;
    public GameObject canvasUI;
    private TextMeshProUGUI _playerScoreEarned;
    private GameObject _scoreVFX;

    private int _currentScore;
    private int _scoreEarned;
    private float _displayLength = 5.0f;
    private bool _displayActive = false;

    public void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        // Updating the displayed score
        _playerScore.text = "Current Score: " + _currentScore.ToString();

        if (_displayActive)
        {
            _playerScoreEarned.text = "+" + _scoreEarned.ToString();
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
        GameObject image = ObjectPool.SharedInstance.ActivateAnObject(_scoreEarnedImage);
        image.transform.SetParent(canvasUI.transform);
        Transform PointsEarnedText = image.gameObject.transform.GetChild(0);
        _scoreVFX = image.gameObject.transform.GetChild(1).gameObject;
        _playerScoreEarned = PointsEarnedText.GetComponent<TextMeshProUGUI>();

        _scoreEarned = v;
        _displayLength -= Time.deltaTime;
        _displayActive = true;
    }

    public void SetGainedScorePosition(Vector3 pos)
    {
        Vector3 posSet = new Vector3(pos.x - 5, pos.y, 0);
        _scoreVFX.transform.position = posSet;

        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, posSet);
        _playerScoreEarned.transform.position = screenPoint;
    }

    IEnumerator DeativateScore()
    {
        yield return new WaitForSeconds(_displayLength);
        _scoreEarnedImage.SetActive(false);
    }

}
