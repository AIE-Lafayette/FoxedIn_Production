using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    int triggeredBoxes;
    int clearedBoxes;

    // Start is called before the first frame update
    void Start()
    {
        triggeredBoxes = 0;
        clearedBoxes = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        //If total boxes touched by trigger is >= 10
        if(triggeredBoxes >= 10)
        {
            //Clear boxes
            if (other.transform.TryGetComponent(out BoxFallingBehavior boxFalling))
            {
                other.transform.gameObject.SetActive(false);
                clearedBoxes++;
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