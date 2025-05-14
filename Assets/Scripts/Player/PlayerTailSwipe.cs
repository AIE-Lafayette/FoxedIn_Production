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

    private BoxCollider boxCol;

    private void Start()
    {
        boxCol = _tailSwipeHitbox.GetComponent<BoxCollider>();
        boxCol.enabled = false;
    }

    void Update()
    {
        GameObject.FindGameObjectsWithTag("TestBox");
    }

    public void TailSwipe(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            boxCol.enabled = true;
            Invoke(nameof(DisableSwipeHitBox), 0.5f);
        }
    }

    void DisableSwipeHitBox()
    {
        boxCol.enabled = false;
    }

   
}
