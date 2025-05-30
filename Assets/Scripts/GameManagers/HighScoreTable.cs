using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreTable : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI _scoreboardRankText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    public void DisplayHighScores(int scoreboardRank, string name, int score)
    {
        //_scoreboardRankText.text = string.Format("{0}", scoreboardRank);
        _nameText.text = name;
        _scoreText.text = string.Format("{0}", score);
    }

    public void HideEntryDisplay()
    {
        //_scoreboardRankText.text = "";
        _nameText.text = "";
        _scoreText.text = "";
    }
}
