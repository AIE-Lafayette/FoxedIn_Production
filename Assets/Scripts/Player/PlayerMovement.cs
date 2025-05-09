using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Component Reference")]
    [SerializeField] private Rigidbody _rb;

    [Header("Player Settings")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _maxDistance;

    [Header("Ground Check")]
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform groundCheck;
    [SerializeField] private Vector3 _objectSize;

    // Standard float for horizontal movement
    private float horizontal;
    private Vector2 _moveDirection;
    


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
        //_rb.velocity = new Vector2(x: _moveDirection.x * _moveSpeed, y: _moveDirection.y * _moveSpeed);
        _rb.velocity = new Vector2(horizontal * _moveSpeed, _rb.velocity.y);
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
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower);
        }
        //_jumpPower = 0f;
    }

    public void Fall(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, -_jumpPower);
        }
    }

    // Slightly more complicated ground check, trying out another ground check temporarily
    //private bool IsGrounded()
    //{
    //    //return true;
    //    //return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.0f, 0.1f), CapsuleDirection2D.Horizontal, 0, layerMask);
    //    return Physics.OverlapCapsule(groundCheck.position, new Vector3(1.0f, 0.1f, 0f), 0, layerMask);
    //}
    //private void OnTriggerStay(Collider other)
    //{
    //    // Whenever it collides the jump is on, whenever not colliding jump is off.
    //    _jumpPower = 5.0f;
    //    Debug.LogError(_jumpPower);
    //    GetComponentInChildren(BoxCollider boxCollider);
    //}

    bool GroundCheck()
    {
        if (Physics.BoxCast(transform.position, _objectSize, -transform.up, transform.rotation, _maxDistance, layerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
