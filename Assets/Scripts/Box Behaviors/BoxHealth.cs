using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [Range(0, 5), SerializeField]
    int _boxMaxHealth = 5;
    [Range(0, 5), SerializeField]
    int _boxStartingHealth;
    int _boxCurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        _boxCurrentHealth = _boxStartingHealth;
    }

    public int CurrentHealth()
    {
        return _boxCurrentHealth;
    }
    public void SubtractHealth()
    {
        _boxCurrentHealth--;
    }


// Update is called once per frame
void Update()
    {
        Debug.Log("Max" + _boxMaxHealth);
        Debug.Log("Start" + _boxStartingHealth);
        Debug.Log("current" + _boxCurrentHealth);
    }

    private void OnDisable()
    {
        _boxCurrentHealth = _boxMaxHealth;
    }
}
