using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Vector2 _moveDirection;
    [SerializeField] private float _jumpPower;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform groundCheck;
    [SerializeField] private Vector3 _objectSize;
    [SerializeField] private float _maxDistance;

    // Standard float for horizontal movement
    private float horizontal;

    public InputActionReference move;

    // Checking the value of a vector2 composite.
    private void Update()
    {
        // Seeing what button is being pressed and if it is a button that moves the player. Apply it in fixed update.
           _moveDirection = move.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        // Moves the character
        //_rb.velocity = new Vector2(x: _moveDirection.x * _moveSpeed, y: _moveDirection.y * _moveSpeed);
        // Temporarily setting this as the character movement
        _rb.velocity = new Vector2(x: _moveDirection.x * _moveSpeed, y: _moveDirection.y * _moveSpeed);
    }

    // Horizontal Movement
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        // If the jump control is performed and we are grounded
        if(context.performed && IsGrounded())
        {
            // Set our rigidbody velocity equal to our jumping power and leave the x velocity the same
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1f, 0.1f), CapsuleDirection2D.Horizontal, 0, layerMask);
    }
}
