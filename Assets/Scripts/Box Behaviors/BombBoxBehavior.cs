using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class BombBoxBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject _bomb;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnBomb()
    {
        GameObject bomb = ObjectPool.SharedInstance.ActivateAnObject(_bomb);
        bomb.transform.position = transform.position;
        bomb.SetActive(true);
    }
}
