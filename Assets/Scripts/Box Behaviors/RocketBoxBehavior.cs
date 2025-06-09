using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class RocketBoxBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject _rocket;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnRocket()
    {
        GameObject rocket = ObjectPool.SharedInstance.ActivateAnObject(_rocket);
        rocket.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        rocket.SetActive(true);
    }
}
