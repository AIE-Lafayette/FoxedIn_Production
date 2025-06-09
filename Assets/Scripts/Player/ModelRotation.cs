using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRotation : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            // -110
            transform.eulerAngles = new Vector3(0, -110, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            // 200
            transform.eulerAngles = new Vector3(0, 200, 0);
        }
        
    }
}
