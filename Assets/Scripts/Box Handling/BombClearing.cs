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

    void RemoveBomb()
    {
        transform.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Invoke(nameof(RemoveBomb), 0.5f);
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.transform.TryGetComponent(out BoxHealth boxHealth))
        {
            other.transform.gameObject.SetActive(false);
        }
    }
}
