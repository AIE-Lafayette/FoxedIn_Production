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

    public int CurrentHealth { get { return _boxCurrentHealth; } }

    // Start is called before the first frame update
    void Start()
    {
        _boxCurrentHealth = _boxStartingHealth;
    }

    public void SubtractHealth()
    {
        _boxCurrentHealth--;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Max" + _boxMaxHealth);
        //Debug.Log("Start" + _boxStartingHealth);
        //Debug.Log("current" + _boxCurrentHealth);

        if (CurrentHealth <= 0)
        {
            if (tag == "RocketBox" || tag == "BombBox")
            {
                if (transform.TryGetComponent(out RocketBoxBehavior rocketBoxBehavior))
                {
                    RocketBoxBehavior rocketBehavior = GetComponent<RocketBoxBehavior>();
                    rocketBehavior.SpawnRocket();
                }
                else if (transform.TryGetComponent(out BombBoxBehavior bombBoxBehavior))
                {
                    BombBoxBehavior bombBehavior = GetComponent<BombBoxBehavior>();
                    bombBehavior.SpawnBomb();
                }
            }
            transform.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        _boxCurrentHealth = _boxMaxHealth;
    }
}

