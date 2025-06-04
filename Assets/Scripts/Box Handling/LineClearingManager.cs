using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineClearingManager : MonoBehaviour
{
    int triggeredBoxes;
    int clearedBoxes;

    int scoreToAdd;
    int multToAdd;
    int finalMult;
    int finalScore;
    int currentScoreTotal;

    int differentBoxes;

    int brownWorth;
    int brownCount;
    int brownScoreMult;

    int greenWorth;
    int greenCount;
    int greenScoreMult;

    int blueWorth;
    int blueCount;
    int blueScoreMult;

    int goldWorth;
    int goldCount;
    int goldScoreMult;

    bool startClearing;

    public int CurrentScore { get { return currentScoreTotal; } }

    // Start is called before the first frame update
    void Start()
    {
        triggeredBoxes = 0;
        clearedBoxes = 0;

        scoreToAdd = 0;
        multToAdd = 1;
        finalMult = 0;
        finalScore = 0;

        brownWorth = 0;
        brownCount = 0;
        brownScoreMult = 0;

        greenWorth = 0;
        greenCount = 0;
        greenScoreMult = 0;

        blueWorth = 0;
        blueCount = 0;
        blueScoreMult = 0;

        goldWorth = 0;
        goldCount = 0;
        goldScoreMult = 0;
    }

    private void FixedUpdate()
    {
        //If triggeredBoxes is less than 10 or clearedBoxes is greater than or equal to 10
        if (triggeredBoxes < 10 || clearedBoxes >= 10)
        {
            //Reset them
            triggeredBoxes = 0;
            clearedBoxes = 0;
        }

        Debug.Log(currentScoreTotal);
    }

    private void ResetAllValues()
    {
        scoreToAdd = 0;
        multToAdd = 1;
        finalMult = 0;
        finalScore = 0;
        differentBoxes = 0;

        brownCount = 0;
        brownScoreMult = 0;

        greenCount = 0;
        greenScoreMult = 0;

        blueCount = 0;
        blueScoreMult = 0;

        goldCount = 0;
        goldScoreMult = 0;

        startClearing = false;
    }

    private void CalculateScore()
    {
        if (brownCount > 0)
        {
            differentBoxes++;
            //Get the multiplier added by the brown boxes
            multToAdd = 1;
            for (int i = 0; i < brownCount; i++)
            {
                multToAdd = multToAdd * brownScoreMult;
            }
            finalMult += multToAdd;
        }

        if (greenCount > 0)
        {
            differentBoxes++;
            //Get the multiplier added by the green boxes
            multToAdd = 1;
            for (int i = 0; i < greenCount; i++)
            {
                multToAdd = multToAdd * greenScoreMult;
            }
            finalMult += multToAdd;
        }

        if (blueCount > 0)
        {
            differentBoxes++;
            //Get the multiplier added by the blue boxes
            multToAdd = 1;
            for (int i = 0; i < blueCount; i++)
            {
                multToAdd = multToAdd * blueScoreMult;
            }
            finalMult += multToAdd;
        }

        if (goldCount > 0)
        {
            differentBoxes++;
            //Get the multiplier added by the gold boxes
            multToAdd = 1;
            for (int i = 0; i < goldCount; i++)
            {
                multToAdd = multToAdd * goldScoreMult;
            }
            finalMult += multToAdd;
        }

        //Calcluate the total score
        //Total points
        scoreToAdd += (brownWorth * brownCount);
        scoreToAdd += (greenWorth * greenCount);
        scoreToAdd += (blueWorth * blueCount);
        scoreToAdd += (goldWorth * goldCount);
        //Final score is total points multiplied by (total mult divided by total of different boxes)
        finalScore = scoreToAdd * (finalMult / differentBoxes);

        currentScoreTotal += finalScore;
        PlayerScore.instance.IncreaseScore(finalScore);

        ResetAllValues();
    }

    private void StartClearingBoxes()
    {
        startClearing = true;
    }

    private void OnTriggerStay(Collider other)
    {
        

        //If trigger is colliding with a box
        if(other.transform.TryGetComponent(out BoxFallingBehavior boxFallingBehavior))
        {
            //Increment trigggeredBoxes
            triggeredBoxes++;
        }

        //If total boxes touched by trigger is >= 10
        if(triggeredBoxes >= 10)
        {
            Invoke(nameof(StartClearingBoxes), 0.5f);

            //Clear boxes
            if (other.transform.TryGetComponent(out BoxFallingBehavior boxFalling))
            {
                if (startClearing == true)
                {
                    BoxWorth boxesWorth = other.transform.GetComponent<BoxWorth>();

                    //Increment brownCount if the box is a brown box and assign brownScoreMult
                    if (other.tag == "BrownBox")
                    {
                        brownWorth = boxesWorth.BoxPointWorth;
                        brownCount++;
                        brownScoreMult = boxesWorth.BoxPointMultiplier;
                    }
                    //Increment greenCount if the box is a green box and assign greenScoreMult
                    else if (other.tag == "GreenBox")
                    {
                        greenWorth = boxesWorth.BoxPointWorth;
                        greenCount++;
                        greenScoreMult = boxesWorth.BoxPointMultiplier;
                    }
                    //Increment blueCount if the box is a blue box and assign blueScoreMult
                    else if (other.tag == "BlueBox")
                    {
                        blueWorth = boxesWorth.BoxPointWorth;
                        blueCount++;
                        blueScoreMult = boxesWorth.BoxPointMultiplier;
                    }
                    //Increment goldCount if the box is a gold box and assign goldScoreMult
                    else if (other.tag == "GoldBox")
                    {
                        goldWorth = boxesWorth.BoxPointWorth;
                        goldCount++;
                        goldScoreMult = boxesWorth.BoxPointMultiplier;
                    }

                    other.transform.gameObject.SetActive(false);
                    clearedBoxes++;

                    if (clearedBoxes >= 10)
                    {
                        CalculateScore();
                    }
                }
            }
        }
        else
        {
            startClearing = false;
        }
    }

    private void LateUpdate()
    {
        
    }
}