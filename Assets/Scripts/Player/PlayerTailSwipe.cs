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
    private PlayerAnimationsTrigger animTrigger;
    //private bool _canSwing = true;
    //private bool _cannotSwing = false;
    public bool cannotSwing = false;
    private float _reloadSwingTimer = 0.75f;
    private bool _tailSwipePerformed;

    public bool TailSwipePerformed { get { return _tailSwipePerformed; } set { _tailSwipePerformed = value; } }

    private void Start()
    {
        animTrigger = GetComponent<PlayerAnimationsTrigger>();
        boxCol = _tailSwipeHitbox.GetComponent<BoxCollider>();
        boxRend = _tailSwipeHitbox.GetComponent<MeshRenderer>();
        boxCol.enabled = false;
        boxRend.enabled = false;
    }

    void Update()
    {
        GameObject.FindGameObjectsWithTag("TestBox");

        if (animTrigger.Anim.GetCurrentAnimatorStateInfo(0).IsName("AirSwipe"))
        {
            Debug.Log("AirSwipe");
        }
        if (animTrigger.Anim.GetCurrentAnimatorStateInfo(0).IsName("TailSwipe"))
        {
            Debug.Log("TailSwipe");
        }

        if (!animTrigger.Anim.GetCurrentAnimatorStateInfo(0).IsName("AirSwipe") && !animTrigger.Anim.GetCurrentAnimatorStateInfo(0).IsName("TailSwipe"))
        {
            Invoke(nameof(DisableSwipeHitBox), 0.0f);
        }
    }

    public void TailSwipe(InputAction.CallbackContext context)
    {
        if (context.performed && !cannotSwing)
        {
            StartCoroutine(PreparingSwing());
            _tailSwipePerformed = true;

            Invoke(nameof(EnableSwipeHitBox), 0.1f);
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
        boxRend.enabled = true;
    }

    IEnumerator PreparingSwing()
    {
        //_canSwing = false;
        cannotSwing = true;

        // This causes the code to wait here for the specified time.
        yield return new WaitForSeconds(_reloadSwingTimer);

        //_canSwing = true;
        cannotSwing = false;
    }

    
}
