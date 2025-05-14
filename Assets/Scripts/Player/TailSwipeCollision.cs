using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailSwipeCollision : MonoBehaviour
{
    private bool _canSwipe = false;
    private void Update()
    {
        Debug.Log(_canSwipe);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TestBox")
        {
            _canSwipe = true;
        }
        else
        {
            _canSwipe = false;
        }
    }
}
