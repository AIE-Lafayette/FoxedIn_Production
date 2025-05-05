using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
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
        _rb.velocity = new Vector2(x: _moveDirection.x * _moveSpeed, y: _moveDirection.y * _moveSpeed);
    }
}
