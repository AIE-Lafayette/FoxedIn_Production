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
    [SerializeField] private float _jumpPower = 12.5f;
    [SerializeField] private float _jumpPowerLong = 17.5f;
    [SerializeField] private float _maxDistance = 1.0f;

    [Header("Ground Check")]
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform groundCheck;
    [SerializeField] private Vector3 _objectSize;

    // Standard float for horizontal movement
    private float horizontal;
    private Vector2 _moveDirection;
    private bool _jumped = false;
    private Rigidbody _playerRB;

    public InputActionReference move;

    private void Start()
    {
        _playerRB = GetComponent<Rigidbody>();
    }

    // Checking the value of a vector2 composite.
    private void Update()
    {
        // Seeing what button is being pressed and if it is a button that moves the player. Apply it in fixed update.
        //_moveDirection = move.action.ReadValue<Vector2>();
        //_moveDirection = move.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        // Moves the character
        _playerRB.velocity = new Vector2(horizontal * _moveSpeed, _playerRB.velocity.y);

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
            _playerRB.velocity = new Vector2(_playerRB.velocity.x, _jumpPower);
            _jumped = true;
            Invoke(nameof(JumpCheck), 0.45f);
 
        }
    }

    private void JumpCheck()
    {
        _jumped = false;
    }

    public void HighJump(InputAction.CallbackContext context)
    {
        // If the high jump control is performed and the player is grounded
        if (context.performed && _jumped/* GroundCheck()*/)
        {
            // Set our rigidbody velocity equal to our jumping power and leave the x velocity the same
            _playerRB.velocity = new Vector2(_playerRB.velocity.x, _jumpPowerLong);
        }
    }

    public void Fall(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _playerRB.velocity = new Vector2(_playerRB.velocity.x, -_jumpPower);
        }
    }

    bool GroundCheck()
    {
        if (Physics.BoxCast(transform.position, _objectSize, -transform.up, transform.rotation, _maxDistance, layerMask))
        {
            _jumped = false;
            return true;
            
        }
        else
        {
            return false;
        }
    }
}
