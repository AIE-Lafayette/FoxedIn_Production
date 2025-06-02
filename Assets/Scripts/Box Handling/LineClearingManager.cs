using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LineClearingManager : MonoBehaviour
{
    int triggeredBoxes;
    int clearedBoxes;

    int scoreToAdd;
    int multToAdd;
    int finalMult;
    int finalScore;
    int scoreTotal;

    int differentBoxes;

    int brownCount;
    int brownScoreMult;

    int greenCount;
    int greenScoreMult;

    int blueCount;
    int blueScoreMult;

    int goldCount;
    int goldScoreMult;

    public int CurrentScore { get { return scoreTotal; } }

    // Start is called before the first frame update
    void Start()
    {
        triggeredBoxes = 0;
        clearedBoxes = 0;

        scoreToAdd = 0;
        multToAdd = 1;
        finalMult = 0;
        finalScore = 0;

        brownCount = 0;
        brownScoreMult = 0;

        greenCount = 0;
        greenScoreMult = 0;

        blueCount = 0;
        blueScoreMult = 0;

        goldCount = 0;
        goldScoreMult = 0;
    }

    private void Update()
    {
        //Debug.Log(scoreTotal);
    }

    private void ResetAllValues()
    {
        scoreToAdd = 0;
        multToAdd = 1;
        finalMult = 0;
        finalScore = 0;

        brownCount = 0;
        brownScoreMult = 0;

        greenCount = 0;
        greenScoreMult = 0;

        blueCount = 0;
        blueScoreMult = 0;

        goldCount = 0;
        goldScoreMult = 0;

    }

    private void CalculateScore()
    {
        //Find how many different colored boxes there are
        if (brownCount > 0)
        {
            differentBoxes++;
        }
        if (greenCount > 0)
        {
            differentBoxes++;
        }
        if (blueCount > 0)
        {
            differentBoxes++;
        }
        if (goldCount > 0)
        {
            differentBoxes++;
        }

        //Get the multiplier added by the brown boxes
        for (int i = 0; i < brownCount; i++)
        {
            multToAdd = multToAdd * brownScoreMult;
        }
        finalMult += multToAdd;
        multToAdd = 1;

        //Get the multiplier added by the green boxes
        for (int i = 0; i < greenCount; i++)
        {
            multToAdd = multToAdd * greenScoreMult;
        }
        finalMult += multToAdd;
        multToAdd = 1;

        //Get the multiplier added by the blue boxes
        for (int i = 0; i < blueCount; i++)
        {
            multToAdd = multToAdd * blueScoreMult;
        }
        finalMult += multToAdd;
        multToAdd = 1;

        //Get the multiplier added by the gold boxes
        for (int i = 0; i < goldCount; i++)
        {
            multToAdd = multToAdd * goldScoreMult;
        }
        finalMult += multToAdd;
        multToAdd = 1;

        //Calcluate the total score
        finalScore = scoreToAdd * (finalMult / differentBoxes);

        scoreTotal += finalScore;

        PlayerScore.instance.IncreaseScore(scoreTotal);
        PlayerScore.instance.DisplayGainedScore(scoreTotal);
        //PlayerScoreEarned.instance.SpawnScoreVFX();

        ResetAllValues();
    }

    private void OnTriggerStay(Collider other)
    {
        //If total boxes touched by trigger is >= 10
        if(triggeredBoxes >= 10)
        {
            //Clear boxes
            if (other.transform.TryGetComponent(out BoxFallingBehavior boxFalling))
            {
                BoxWorth boxesWorth = other.transform.GetComponent<BoxWorth>();

                //Increment brownCount if the box is a brown box and assign brownScoreMult
                if (other.tag == "BrownBox")
                {
                    brownCount++;
                    brownScoreMult = boxesWorth.BoxPointMultiplier;
                }
                //Increment greenCount if the box is a green box and assign greenScoreMult
                else if (other.tag == "GreenBox")
                {
                    greenCount++;
                    greenScoreMult = boxesWorth.BoxPointMultiplier;
                }
                //Increment blueCount if the box is a blue box and assign blueScoreMult
                else if (other.tag == "BlueBox")
                {
                    blueCount++;
                    blueScoreMult = boxesWorth.BoxPointMultiplier;
                }
                //Increment goldCount if the box is a gold box and assign goldScoreMult
                else if (other.tag == "GoldBox")
                {
                    goldCount++;
                    goldScoreMult = boxesWorth.BoxPointMultiplier;
                }

                scoreToAdd += boxesWorth.BoxPointWorth;

                other.transform.gameObject.SetActive(false);
                clearedBoxes++;
                if (clearedBoxes >= 10)
                {
                    CalculateScore();
                }
            }
        }
        //If trigger is colliding with a box
        if(other.transform.TryGetComponent(out BoxFallingBehavior boxFallingBehavior))
        {
            //Increment trigggeredBoxes
            triggeredBoxes++;
        }
    }

    private void LateUpdate()
    {
        //If triggeredBoxes is less than 10 or clearedBoxes is less than or equal to 10
        if (triggeredBoxes < 10 || clearedBoxes >= 10)
        {
            //Reset them
            triggeredBoxes = 0;
            clearedBoxes = 0;
        }
    }
}