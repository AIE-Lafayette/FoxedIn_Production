using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombClearing : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RemoveBombCollider()
    {
        transform.GetComponent<BoxCollider>().enabled = false;
        Invoke(nameof(RemoveBomb), 1.0f);
    }

    void RemoveBomb()
    {
        transform.gameObject.SetActive(false);
        transform.GetComponent<BoxCollider>().enabled = true;
    }

    private void OnEnable()
    {
        Invoke(nameof(RemoveBombCollider), 0.25f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.TryGetComponent(out BoxFallingBehavior boxFalling))
        {
            
            BoxWorth worth = other.transform.GetComponent<BoxWorth>();
            PlayerScore.instance.IncreaseScore(worth.BoxPointWorth);
            PlayerScore.instance.DisplayGainedScore(worth.BoxPointWorth);
            PlayerScore.instance.SetGainedScorePosition(other.transform.position);
            other.transform.gameObject.SetActive(false);
        }
    }
}
