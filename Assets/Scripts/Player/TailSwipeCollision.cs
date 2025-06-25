using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.VFX;

public class TailSwipeCollision : MonoBehaviour
{
    [SerializeField]
    VisualEffect HitEffect;
    [SerializeField]
    AudioSource HitAudio;

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

            if (!boxHealth.CanBeHit)
            {
                return;
            }

            #region "Hit Effect"

            Vector3 collisionPoint = transform.position;
            if (collisionPoint.x < other.transform.position.x)
            {
                collisionPoint = new Vector3(other.transform.position.x - 2.5f, collisionPoint.y, collisionPoint.z);
            }
            if (collisionPoint.x > other.transform.position.x)
            {
                collisionPoint = new Vector3(other.transform.position.x + 2.5f, collisionPoint.y, collisionPoint.z);
            }
            //Debug.Log("Collision Point x" + collisionPoint.x);
            //Debug.Log("Collision Point y" + collisionPoint.y);
            //Debug.Log("Collision Point z" + collisionPoint.z);

            HitEffect.transform.position = collisionPoint;
            HitEffect.Play();
            HitAudio.Play();

            #endregion

            #region "Damaging Boxes"

            boxHealth.SubtractHealth();

            #endregion

            #region "Moving Boxes"

            //If box is sliding
            if (boxMove.IsSliding)
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
