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
            StartCoroutine(PreparingSwing());

            boxCol.enabled = true;
            boxRend.enabled = true;
            // was previously set to 0.5f
            // How long the hitbox appears for after the tailswipe button is pressed
            Invoke(nameof(DisableSwipeHitBox), 0.2f);
            //_anim.SetTrigger("SwipeGround");
        }
    }

    void DisableSwipeHitBox()
    {
        boxCol.enabled = false;
        boxRend.enabled = false;
        
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
