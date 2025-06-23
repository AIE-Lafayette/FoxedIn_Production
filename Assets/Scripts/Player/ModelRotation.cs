using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRotation : MonoBehaviour
{
    private PlayerMovement _playerMovement;

    private void Start()
    {
        _playerMovement = GetComponentInParent<PlayerMovement>();
    }

    void Update()
    {
        //if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        //{
        //    // -110
        //    transform.eulerAngles = new Vector3(0, -110, 0);
        //}
        //if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        //{
        //    // 200
        //    transform.eulerAngles = new Vector3(0, -180, 0);
        //}

        if (_playerMovement.PlayerHorizontal <= -1)
        {
            // -110
            transform.eulerAngles = new Vector3(0, -70, 0);
        }
        if (_playerMovement.PlayerHorizontal >= 1)
        {
            // -180
            transform.eulerAngles = new Vector3(0, -220, 0);
        }
    }
}
