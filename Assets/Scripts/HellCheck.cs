using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellCheck : MonoBehaviour
{
    [SerializeField] private GameObject _MainCamera;

    public static bool _hellModeEnabled = false;

    public bool HellModeEnabled { get { return _hellModeEnabled; } }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!_MainCamera)
        {
            return;
        }

        _hellModeEnabled = _MainCamera.GetComponent<StartScene>().HellModeActive;
    }
}
