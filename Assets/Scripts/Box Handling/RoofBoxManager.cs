using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofBoxManager : MonoBehaviour
{
    private bool _boxAtTop;

    public bool BoxAtTop { get { return _boxAtTop; } }

    // Start is called before the first frame update
    void Start()
    {
        _boxAtTop = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.TryGetComponent(out BoxFallingBehavior boxFalling))
        {
            if (!boxFalling.Falling)
            {
                _boxAtTop = true;
            }
        }
    }
}