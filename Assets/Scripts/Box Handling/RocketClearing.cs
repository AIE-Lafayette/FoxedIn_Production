using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketClearing : MonoBehaviour
{
    [Range(1, 100), SerializeField]
    private float _rocketSpeed = 20;

    Rigidbody rocketRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rocketRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rocketRigidbody.velocity = new Vector3(0, _rocketSpeed, 0);
        if (transform.position.y >= 50)
        {
            transform.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out BoxFallingBehavior boxFalling))
        {
            other.transform.gameObject.SetActive(false);
            BoxWorth worth = other.transform.GetComponent<BoxWorth>();
            PlayerScore.instance.IncreaseScore(worth.BoxPointWorth);
            PlayerScore.instance.DisplayGainedScore(worth.BoxPointWorth);
            PlayerScore.instance.SetGainedScorePosition(other.transform.position);
        }
    }
}
