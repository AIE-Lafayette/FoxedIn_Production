using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailSwipeCollision : MonoBehaviour
{
    private void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        //Layer 3 is Box
        if (other.gameObject.layer == 3)
        {
            Rigidbody boxRigidBody = other.GetComponent<Rigidbody>();
            BoxMovement boxMove = other.GetComponent<BoxMovement>();
            boxMove.StartSliding();
            //If the box is on the right
            if (other.transform.position.x - transform.position.x > 0)
            {
                boxRigidBody.velocity = new Vector3(30, 0, 0);
            }
            //If the box is on the left
            else
            {
                boxRigidBody.velocity = new Vector3(-30, 0, 0);
            }
        }
    }
}
