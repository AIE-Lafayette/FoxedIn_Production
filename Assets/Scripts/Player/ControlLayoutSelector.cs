using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlLayoutSelector : MonoBehaviour
{
    private GameObject _player;
    private PlayerInput _playerInput;

    
    [SerializeField]
    GameObject DefaultControlImage;
    [SerializeField]
    GameObject AltControlImage;
    [SerializeField]
    GameObject OptionsImage;

    private static int _controlScheme = 1;
    public int ControlScheme { get { return _controlScheme; } set { _controlScheme = value; } }

    public void Start()
    {
        DontDestroyOnLoad(transform.root.gameObject);
       _playerInput = GetComponent<PlayerInput>();
        // Setting the defauly action map
        //_inputSelector.SwitchCurrentActionMap("Player");
    }

    public void Update()
    {

        //Guard
        if (DefaultControlImage == null || AltControlImage == null || OptionsImage == null)
        {
            return;
        }

        if(OptionsImage.activeInHierarchy)
        {
            if (_controlScheme == 1)
            {
                AltControlImage.SetActive(false);
                DefaultControlImage.SetActive(true);
            }
            else if(_controlScheme == 2)
            {
                DefaultControlImage.SetActive(false);
                AltControlImage.SetActive(true);
            }
        }

    }

    //Default
    public void ControlScheme1()
    {
        _controlScheme = 1;
    }

    //Alt
    public void ControlScheme2()
    {
        _controlScheme = 2;
    }
}
