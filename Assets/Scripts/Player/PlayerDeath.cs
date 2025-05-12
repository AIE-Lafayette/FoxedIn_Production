using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDeath : MonoBehaviour
{
    private bool _wasCrushed = false;

    public bool WasCrushed { get { return _wasCrushed; } }

    private bool collisionAbove = false;
    private bool collisionBelow = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (collisionAbove && collisionBelow)
        {
            _wasCrushed = true;
        }
        else
        {
            _wasCrushed = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //If collision isn't with a box
        if (!collision.transform.TryGetComponent(out BoxFallingBehavior boxFallingBehavior))
        {
            //Ignore
            return;
        }
        //If the collision is above them
        if (collision.transform.position.y > transform.position.y)
        {
            //collisionAbove is true
            collisionAbove = true;
        }
        //If the collision is below them
        if (collision.transform.position.y < transform.position.y || transform.position.y <= -1.3)
        {
            //collisionBelow is true
            collisionBelow = true;
        }
    }

    private void LateUpdate()
    {
        collisionAbove = false;
        collisionBelow = false;
    }
}
