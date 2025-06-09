using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimationsTrigger : MonoBehaviour
{
    private Animator _anim;
    //private Rigidbody _rb;
    private PlayerMovement _playerMovement;
    private float _speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        //_rb = GetComponent<Rigidbody>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        bool moving = Input.GetKey("a") || Input.GetKey("d");
        if (moving)
        {
            //_anim.SetBool("IdleWalkRun", true);
            _anim.SetFloat("Speed", _speed);
        }
        if (_playerMovement.GroundCheck() == false)
        {
            _anim.SetFloat("Speed", 0);
        }
    }
}
