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
        if (transform.position.y >= 47.5)
        {
            transform.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.TryGetComponent(out BoxFallingBehavior boxFalling))
        {
            other.transform.gameObject.SetActive(false);
        }
    }
}
