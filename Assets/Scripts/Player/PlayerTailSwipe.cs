using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTailSwipe : MonoBehaviour
{
    [Header("Box Component References")]
    [SerializeField] private GameObject _tailSwipeHitbox;

    //[Header("Tail Swipe VFX")]
    //[SerializeField] private GameObject _tailSwipeVFX;

    private BoxCollider boxCol;
    private MeshRenderer boxRend;
    //private bool _canSwing = true;
    //private bool _cannotSwing = false;
    public bool cannotSwing = false;
    private float _reloadSwingTimer = 0.75f;
    private bool _tailSwipePerformed;
    private bool _hasCollided = false;

    public bool TailSwipePerformed { get { return _tailSwipePerformed; } set { _tailSwipePerformed = value; } }

    private void Start()
    {
        boxCol = _tailSwipeHitbox.GetComponent<BoxCollider>();
        boxRend = _tailSwipeHitbox.GetComponent<MeshRenderer>();
        boxCol.enabled = false;
        boxRend.enabled = false;
    }

    void Update()
    {
        GameObject.FindGameObjectsWithTag("TestBox");

        //if (cannotSwing)
        //{
        //    _anim.SetBool("SwipeGround", false);
        //}
    }

    public void TailSwipe(InputAction.CallbackContext context)
    {
        if (context.performed && !cannotSwing)
        {
            _tailSwipePerformed = true;
            StartCoroutine(PreparingSwing());

            Invoke(nameof(EnableSwipeHitBox), 0.1f);
            // was previously set to 0.5f
            // How long the hitbox appears for after the tailswipe button is pressed
            Invoke(nameof(DisableSwipeHitBox), 0.5f);
            //_anim.SetTrigger("SwipeGround");
        }
        if (_hasCollided)
        {
            Invoke(nameof(DisableSwipeHitBox), 0.1f);
        }
    }

    void DisableSwipeHitBox()
    {
        _tailSwipePerformed = false;
        boxCol.enabled = false;
        boxRend.enabled = false;
    }

    void EnableSwipeHitBox()
    {
        boxCol.enabled = true;
        //boxRend.enabled = true;
    }

    IEnumerator PreparingSwing()
    {
        //_canSwing = false;
        cannotSwing = true;

        // This causes the code to wait here for the specified time.
        yield return new WaitForSeconds(_reloadSwingTimer);

        //_canSwing = true;
        cannotSwing = false;
        _hasCollided = false;
        Debug.Log(_hasCollided);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_hasCollided)
        {
            _hasCollided = true;
            Debug.Log(_hasCollided);
        }
    }


}
