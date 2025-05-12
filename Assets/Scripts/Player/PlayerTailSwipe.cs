using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTailSwipe : MonoBehaviour
{
    //[SerializeField] private Transform _attackPoint;
    //[SerializeField] private float _attackRange = 0.5f;
    //[SerializeField] private LayerMask _enemyLayer;
    [Header("Box Component References")]
    [SerializeField] private GameObject _tailSwipeHitbox;
    [SerializeField] private Rigidbody _brb;

    [Header("Player Settings")]
    [SerializeField] private Rigidbody _rb;

    private bool _faceingRight = true;
    private bool _canSwipe = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            _faceingRight = true;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _faceingRight = false;
        }
        GameObject.FindGameObjectsWithTag("TestBox");
        Debug.Log(_canSwipe);
    }

    void TailSwipe(InputAction.CallbackContext context)
    {
        Vector3 tailSwipePos;
        if (context.performed && _canSwipe)
        {
            if (_faceingRight == true)
            {
                tailSwipePos = transform.position + (transform.right);
                _brb.AddForce(Vector3.right * 750.0f);
            }
            else
            {
                tailSwipePos = transform.position - (transform.right);
                _brb.AddForce(Vector3.left * 750.0f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TestBox")
        {
            _canSwipe = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _canSwipe = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
