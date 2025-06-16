using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using static UnityEngine.Rendering.DebugUI;

public class PlayerAnimationsTrigger : MonoBehaviour
{
    [Header("Player Component References")]
    private PlayerMovement _playerMovement;
    private PlayerTailSwipe _tailSwipe;
    private Animator _anim;
    private Rigidbody _rb;

    private float _speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
        _tailSwipe = GetComponent<PlayerTailSwipe>();
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        bool moving;
        bool swiping;
        bool jumping;

        if (_playerMovement.PlayerHorizontal == 0)
        {
            moving = false;
        }
        else
        {
            moving = true;
        }

        if (_tailSwipe.TailSwipePerformed)
        {
            swiping = true;
            _tailSwipe.TailSwipePerformed = false;
        }
        else
        {
            swiping = false;
        }

        if (_playerMovement.JumpPerformed)
        {
            jumping = true;
        }
        else
        {
            jumping = false;
        }


        //If there is no input, is grounded,
        if (!moving && !swiping && !jumping && _playerMovement.GroundCheck())
        {
            //...and the current animation is done
            if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
            {
                //reset triggers

                //...return to idle animation
                _anim.SetBool("IdleWalkRun", true);
                _anim.SetFloat("Speed", 0);
            }
        }

        if (moving && _playerMovement.GroundCheck())
        {
            //Set to IdleWalkRun and set to the run value
            _anim.SetBool("IdleWalkRun", true);
            _anim.SetFloat("Speed", _speed);
        }
        if (!moving && _playerMovement.GroundCheck())
        {
            //Set to IdleWalkRun and set to the idle value
            _anim.SetBool("IdleWalkRun", true);
            _anim.SetFloat("Speed", 0);
        }

        if (swiping && _playerMovement.GroundCheck())
        {
            //Set to SwipeGround
            _anim.SetBool("SwipeGround", true);
            _anim.SetFloat("Speed", 0);
        }
        else if(swiping && !_playerMovement.GroundCheck())
        {
            //Set to AirSwipe
            _anim.SetBool("AirSwipe", true);
            _anim.SetFloat("Speed", 0);
        }

        //if not jumpin and on ground
        if(!jumping && _playerMovement.GroundCheck())
        {
            _anim.SetBool("JumpStart", false);
            _anim.SetBool("JumpAir", false);
            _anim.SetBool("JumpEnd", false);
        }

        //If jumping and and can jump
        if (jumping && !_anim.GetCurrentAnimatorStateInfo(0).IsName("AirSwipe") && _rb.velocity.y > 1)
        {
            _anim.SetBool("IdleWalkRun", false);
            _anim.SetBool("JumpStart", true);
            _anim.SetFloat("Speed", 0);
        }

        //If curret state isnt airswipe and in air
        if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("AirSwipe") && !_playerMovement.GroundCheck() && _rb.velocity.y <=1)
        {
            _anim.SetBool("IdleWalkRun", false);
            _anim.SetBool("JumpStart", false);
            _anim.SetBool("JumpAir", true);
            _anim.SetFloat("Speed", 0);
        }

        //If current state is JumpAir and grounded
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("JumpAir") && _playerMovement.GroundCheck())
        {
            _anim.SetBool("JumpAir", false);
            _anim.SetBool("JumpEnd", true);
            _anim.SetFloat("Speed", 0);
        }
    }
}
