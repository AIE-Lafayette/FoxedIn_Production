using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField]
    bool _canDie = true;

    private bool _wasCrushed = false;

    public bool WasCrushed { get { return _wasCrushed; } }

    private bool collisionAbove = false;
    private bool collisionBelow = false;

    // Update is called once per frame
    void Update()
    {
        if(!(_canDie))
        {
            collisionAbove = false;
            collisionBelow = false;
        }

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
        //If collision isn't with a box or ground (Layer 3 is box and Layer 6 is ground)
        if (!(collision.gameObject.layer == 3) && !(collision.gameObject.layer == 6))
        {
            //Ignore
            return;
        }
        //If the collision is to far to have been actually squishing them
        if (collision.transform.position.x - transform.position.x >= 2.5 || collision.transform.position.x - transform.position.x <= -2.5)
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
        //If the collision is below them or they are low enough
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
