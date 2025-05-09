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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(triggeredBoxes >= 10)
        {
            other.transform.gameObject.SetActive(false);
            clearedBoxes++;
        }
        triggeredBoxes++;
    }

    private void LateUpdate()
    {
        if (triggeredBoxes < 10 || clearedBoxes == 10)
        {
            triggeredBoxes = 0;
            clearedBoxes = 0;
        }
    }
}