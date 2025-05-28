using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScores : MonoBehaviour
{
    // using a list to sort the high scores, do to it being easier to sort and resize than an array
    List<HighScoreEntry> scores = new List<HighScoreEntry>();
}
