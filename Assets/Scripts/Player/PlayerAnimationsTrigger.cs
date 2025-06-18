using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
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
    private bool _canJump;

    public Animator Anim { get { return _anim; } }

    // Start is called before the first frame update
    void Start()
    {
        _canJump = false;
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
            //_tailSwipe.TailSwipePerformed = false;
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
        if (!moving && !swiping && !jumping && _playerMovement.OnGround())
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

        if (moving && _playerMovement.OnGround())
        {
            //Set to IdleWalkRun and set to the run value
            _anim.SetBool("IdleWalkRun", true);
            _anim.SetFloat("Speed", _speed);
        }
        if (!moving && _playerMovement.OnGround())
        {
            //Set to IdleWalkRun and set to the idle value
            _anim.SetBool("IdleWalkRun", true);
            _anim.SetFloat("Speed", 0);
        }

        if (swiping && _playerMovement.OnGround())
        {
            //Set to SwipeGround
            _anim.SetBool("SwipeGround", true);
            _anim.SetFloat("Speed", 0);
        }
        else if(swiping && !_playerMovement.OnGround())
        {
            //Set to AirSwipe
            _anim.SetBool("AirSwipe", true);
            _anim.SetFloat("Speed", 0);
        }

        //if not jumpin and on ground
        if(!jumping && _playerMovement.OnGround())
        {
            _anim.SetBool("JumpStart", false);
            _anim.SetBool("UpAir", false);
            _anim.SetBool("FallAir", false);
            _anim.SetBool("JumpEnd", false);
            _anim.SetBool("AirSwipe", false);
        }

        Debug.Log(jumping);

        //Jump Start anim
        if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("AirSwipe") && jumping)
        {
            if (_anim.GetCurrentAnimatorStateInfo(0).IsName("JumpStart"))
            {
                _playerMovement.JumpPerformed = false;
            }

            Debug.Log("JumpStarted");
            _anim.SetBool("IdleWalkRun", false);
            _anim.SetBool("JumpStart", true);
            _anim.SetFloat("Speed", 0);
        }

        //Up Air Anim
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("JumpStart") && _playerMovement.OnGround() || _anim.GetCurrentAnimatorStateInfo(0).IsName("JumpStart") && !_playerMovement.OnGround() || _anim.GetCurrentAnimatorStateInfo(0).IsName("AirSwipe") && _rb.velocity.y >= 5 || _anim.GetCurrentAnimatorStateInfo(0).IsName("TailSwipe") && _rb.velocity.y >= 5)
        {
            _anim.SetBool("IdleWalkRun", false);
            _anim.SetBool("JumpStart", false);
            _anim.SetBool("UpAir", true);
            _anim.SetFloat("Speed", 0);
        }

        //Fall Air Anim
        if (!_playerMovement.OnGround() && _rb.velocity.y <=1 || _anim.GetCurrentAnimatorStateInfo(0).IsName("UpAir") && _rb.velocity.y <= 1)
        {
            _anim.SetBool("IdleWalkRun", false);
            _anim.SetBool("UpAir", false);
            _anim.SetBool("FallAir", true);
            _anim.SetFloat("Speed", 0);
        }

        //Jump End anim
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("FallAir") && _playerMovement.OnGround() || _anim.GetCurrentAnimatorStateInfo(0).IsName("AirSwipe") && _playerMovement.OnGround() || _anim.GetCurrentAnimatorStateInfo(0).IsName("TailSwipe") && _playerMovement.OnGround())
        {
            _anim.SetBool("FallAir", false);
            _anim.SetBool("JumpEnd", true);
            _anim.SetFloat("Speed", 0);
        }
    }
}
