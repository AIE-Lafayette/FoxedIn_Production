using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private bool _onGround;
    public bool OnGround { get { return _onGround; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        _onGround = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 3 || other.gameObject.layer == 6)
        {
            _onGround = true;
        }
    }
}
