using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoxFallingBehavior : MonoBehaviour
{
    Rigidbody ObjectRigidBody;
    bool colliding;
    bool _falling = true;
    public bool Falling { get { return _falling; } }

    // Start is called before the first frame update
    void Start()
    {
        ObjectRigidBody = GetComponent<Rigidbody>();
        colliding = false;
    }

    float FindNearestYGridPoint()
    {
        float pos = transform.position.y;
        float closestGridPoint = 0.0f;
        float gridCount = 0;

        //Find how many times 5 goes into the position
        gridCount = pos / 5;
        //Round the result to the nearest whole number
        gridCount = Mathf.Round(gridCount);
        //Multiply 5 by the rounded result to get the closest grid point
        closestGridPoint = 5 * gridCount;

        return closestGridPoint;
    }

    void DisableFalling()
    {
        _falling = false;
        //Disable gravity
        ObjectRigidBody.useGravity = false;
        //Set velocity to 0
        ObjectRigidBody.velocity = new Vector3(ObjectRigidBody.velocity.x, 0, 0);
        //Set y position to the nearest grid point
        transform.position = new Vector3(transform.position.x, FindNearestYGridPoint(), transform.position.z);

    }
    void EnableFalling()
    {
        _falling = true;
        //Enable gravity
        ObjectRigidBody.useGravity = true;
    }

    // Update is called once per frame
    void Update()
    {
        //If object is at or somehow below 0
        if(ObjectRigidBody.position.y <= 0)
        {
            DisableFalling();
        }
        //If object is not at 0 and isnt colliding
        else if(colliding == false)
        {
            EnableFalling();
        }

        colliding = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        Vector3 otherPosition = collision.transform.position;

        //If the collision object is in the same grid column
        if (transform.position.x == otherPosition.x)
        {
            //If the collision object is below
            if(otherPosition.y < transform.position.y)
            {
                colliding = true;
                DisableFalling();
            }
        }
    }
}
