using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

// The player currently gets stuck on walls if either the a or d key is held. This could either be a feature to the game such as holding on to walls for potentially a wall jump or it will have to be prevented
public class PlayerMovement : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float _moveSpeed = 10.0f;
    [SerializeField] private float _jumpPower = 32.5f;
    [SerializeField] private float _maxDistance = 1.0f;

    [Header("Ground Check")]
    //The Layermask is what the ground check will be checking for
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform groundCheck;
    [SerializeField] private Vector3 _objectSize;

    // Standard float for horizontal movement
    private float horizontal;
    private Rigidbody _playerRB;
    private Animator _animator;
    private bool _increaseGravity;
    private float _velocityCheck;

    //public InputActionReference move;

    private void Start()
    {
        _playerRB = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _velocityCheck = 0.0f;
    }

    private void FixedUpdate()
    {
        // Moves the character
        _playerRB.velocity = new Vector2(horizontal * _moveSpeed, _playerRB.velocity.y);

        

        if (_velocityCheck > _playerRB.velocity.y)
        {
            _increaseGravity = true;
        }

        _velocityCheck = _playerRB.velocity.y;

        if (_increaseGravity)
        {
            _playerRB.velocity = new Vector2(_playerRB.velocity.x, _playerRB.velocity.y - 0.5f);
        }

        //Rotates the character
        Vector3 movement = new Vector3(horizontal, 0.0f, 0.0f);

        if (movement.x == 0)
        {
            return;
        }

        transform.rotation = Quaternion.LookRotation(movement);
    }

    // Horizontal Movement
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        // If the jump control is performed and we are grounded
        if (context.performed && GroundCheck())
        {
            // Set our rigidbody velocity equal to our jumping power and leave the x velocity the same
            _playerRB.velocity = new Vector2(/*_playerRB.velocity.x*/0, _jumpPower);

 
        }

        // Whenever button is released, cut the y velocity.
        if (context.canceled && _playerRB.velocity.y > 0f)
        {
            _playerRB.velocity = new Vector2(_playerRB.velocity.x, _playerRB.velocity.y * 0.3f);
            _increaseGravity = true;
        }
    }

    //public void Fall(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //    {
    //        _playerRB.velocity = new Vector2(_playerRB.velocity.x, -_jumpPower);
    //    }
    //}

    bool GroundCheck()
    {
        if (Physics.BoxCast(transform.position, _objectSize, -transform.up, transform.rotation, _maxDistance, layerMask))
        {
            _increaseGravity = false;
            return true;
            
        }
        else
        {
            return false;
        }
    }
}
