using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxWorth : MonoBehaviour
{
    [Header("Worth Settings")]
    [Range(0, 1000), SerializeField]
    int _boxWorth = 5;
    [Range(0, 10), SerializeField]
    int _boxMultiplier = 2;

    public int BoxPointWorth { get { return _boxWorth; } }
    public int BoxPointMultiplier { get { return _boxMultiplier; } }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

