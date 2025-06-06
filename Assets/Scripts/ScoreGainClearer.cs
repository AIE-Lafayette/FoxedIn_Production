using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreGainClearer : MonoBehaviour
{
    float upCount;
    private TextMeshProUGUI _playerScoreEarned;

    private void OnEnable()
    {
        Invoke(nameof(DisableScoreEarned), 0.75f);
        upCount = 1.0f;
        Transform PointsEarnedText = gameObject.transform.GetChild(0);
        _playerScoreEarned = PointsEarnedText.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        float random = Random.Range(0, 100);

        if (random < 50 && upCount >= 0)
        {
            _playerScoreEarned.transform.Translate(new Vector3(-upCount / 2, upCount * 0.9f, 0));
            upCount -= 0.005f;
        }
        else if (random >= 50 && upCount >= 0)
        {
            _playerScoreEarned.transform.Translate(new Vector3(upCount / 2, upCount * 0.9f, 0));
            upCount -= 0.005f;
        }
    }

    private void DisableScoreEarned()
    {
        transform.gameObject.SetActive(false);
    }
}
