using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlLayoutSelector : MonoBehaviour
{
    private PlayerInput _inputSelector;

    public void Awake()
    {
        _inputSelector = GetComponent<PlayerInput>();
        // Setting the defauly action map
        //_inputSelector.SwitchCurrentActionMap("Player");
    }

    public void Update()
    {
        
    }
    public void DefaultControls()
    {
        // This is an option to switch back to the standard action map
        _inputSelector.SwitchCurrentActionMap("Player");
    }

    public void ControlScheme2()
    {
        // This is an option to switch to an action map that uses space as jump.
        _inputSelector.SwitchCurrentActionMap("Player1");
    }
}
