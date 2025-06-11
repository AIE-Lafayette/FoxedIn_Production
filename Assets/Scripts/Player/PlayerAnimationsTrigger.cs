using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimationsTrigger : MonoBehaviour
{
    //private Rigidbody _rb;
    [Header("Player Component References")]
    private PlayerMovement _playerMovement;
    private PlayerTailSwipe _tailSwipe;
    private Animator _anim;

    private float _speed = 1.0f;
    private float _jumpSpeed = 0.0f;

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
        bool swiping = Input.GetKeyDown("e");

        if (swiping)
        {
            StartCoroutine(PreparingAnimations("SwipeGround"));
        }

        if (moving)
        {
            //_anim.SetBool("IdleWalkRun", true);
            _anim.SetFloat("Speed", _speed);
        }
        else if (!moving)
        {
            _anim.SetFloat("Speed", 0);
        }

        bool jumping = Input.GetKey("w");

        if (jumping)
        {
            _anim.SetTrigger("JumpCycle");
            //_anim.SetBool("JumpCycle", true);
            //_anim.SetFloat("Speed", _jumpSpeed);
            //_jumpSpeed += 0.1f;
        }
        else if (!jumping)
        {
            _anim.SetBool("JumpCycle", false);
            //_jumpSpeed = 0.0f;
        }

        //if (swiping)
        //{
        //    _anim.SetTrigger("SwipeGround");
        //    // Wait for the transition to end
        //    //yield return new WaitUntil(() => _anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
        //}
    }

    IEnumerator PreparingAnimations(string swipeGround)
    {
        _anim.SetTrigger("SwipeGround");

        //// Wait for transition to end
        //yield return new WaitUntil(() => _anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);

        //// Wait for the animation to end
        //yield return new WaitWhile(() => _anim.GetCurrentAnimatorStateInfo(0).IsName("SwipeGround"));

        while (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        Debug.Log("Animation finished");
    }
}
