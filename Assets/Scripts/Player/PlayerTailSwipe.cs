using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTailSwipe : MonoBehaviour
{
    // Need access to box rigidbody to move it
    // we only want to move the x position of the box whenver it is hit
    // We do not want to be able to run into a box and move it that way

    [Header("Box Component Reference")]
    // _brb stands for box rigid body
    [SerializeField] private Rigidbody _brb;

    [Header("Player Settings")]
    // How far the box will be pushed
    [SerializeField] private float _pushPower;

    private bool _attackable = false;

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{

    //}

    private void OnCollisionEnter(Collision collision)
    {
        _attackable = true;
    }

    public void TailSwipe(InputAction.CallbackContext context)
    {
        if (context.performed && _attackable)
        {
            _brb.AddForce(10, 0, 0);
        }
    }
}
