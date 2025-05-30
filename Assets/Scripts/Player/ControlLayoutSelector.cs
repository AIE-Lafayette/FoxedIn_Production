using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlLayoutSelector : MonoBehaviour
{
    private GameObject _player;
    private PlayerInput _playerInput;

    public void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        // Setting the defauly action map
        //_inputSelector.SwitchCurrentActionMap("Player");
    }

    public void Update()
    {
        
    }
    public void DefaultControls()
    {
        // This is an option to switch back to the standard action map
        //_playerInput.SwitchCurrentActionMap("Player");
        _playerInput.actions.FindActionMap("Player").Enable();
        _playerInput.actions.FindActionMap("Player1").Disable();
    }

    public void ControlScheme2()
    {
        // This is an option to switch to an action map that uses space as jump.
        //_playerInput.SwitchCurrentActionMap("Player1");
        _playerInput.actions.FindActionMap("Player1").Enable();
        _playerInput.actions.FindActionMap("Player").Disable();
    }
}
