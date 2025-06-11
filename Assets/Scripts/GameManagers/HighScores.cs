using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScores : MonoBehaviour
{
    // using a list to sort the high scores, do to it being easier to sort and resize than an array
    // initializing a blank list
    List<HighScoreEntry> scores = new List<HighScoreEntry>();
    // This array stores references to each one of the high score display scripts that are on the scoreboard
    public HighScoreTable[] highScoreDisplayArray;

   

    private void Start()
    {
        // test for adding high scores
        AddNewHighScore(/*1,*/ "Bronny", 10000);
        AddNewHighScore(/*2,*/ "Bron", 9);
        AddNewHighScore(/*3,*/ "Leebron", 0);
        AddNewHighScore(/*4,*/ "Lebronto", 10);
        AddNewHighScore(/*5,*/ "Lebron", 1);

        UpdateDisplay();
    }

    // Creating a function to create new high score entries
    void AddNewHighScore(/*int entryRank, */string entryName, int entryScore)
    {
        scores.Add(new HighScoreEntry { /*rank = entryRank,*/ name = entryName, score = entryScore });
    }

    void UpdateDisplay()
    {
        // Sorting the list by comparing x and y to see which score is higher
        scores.Sort((HighScoreEntry x, HighScoreEntry y) => y.score.CompareTo(x.score));

        // Looping through the list of scores, and adding the data to a row if it is a high score
        for (int i = 0; i < highScoreDisplayArray.Length; i++)
        {
            if (i < scores.Count)
            {
                highScoreDisplayArray[i].DisplayHighScores(scores[i].rank, scores[i].name, scores[i].score);
            }
            // if there are no more entries to display, display a blank row
            else
            {
                highScoreDisplayArray[i].HideEntryDisplay();
            }
        }
    }
}
