using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
            BoxHealth boxHealth = other.GetComponent<BoxHealth>();

            #region "Damaging Boxes"

            boxHealth.SubtractHealth();

            #endregion

            #region "Moving Boxes"

            //If box is sliding
            if (boxMove.IsSliding())
            {
                //Ignore
                return;
            }
            boxMove.StartSliding();

            //If the box is on the right
            if (other.transform.position.x - transform.position.x > 0)
            {
                boxMove.SetHitDirection(true);

                ////Box pushing with velocity
                //boxRigidBody.velocity = new Vector3(15, 0, 0);
            }
            //If the box is on the left
            else
            {
                boxMove.SetHitDirection(false);

                ////Box pushing with velocity
                //boxRigidBody.velocity = new Vector3(-15, 0, 0);
            }

            #endregion

        }
    }
}
