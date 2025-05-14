using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    public GameObject player;

    private float _nearestGridPoint;
    private bool _sliding;

    // Start is called before the first frame update
    void Start()
    {
        Collider boxesCollider = GetComponent<BoxCollider>();
        Collider playerCollider = player.GetComponent<CapsuleCollider>();
        Physics.IgnoreCollision(boxesCollider, playerCollider);

        _sliding = false;
        _nearestGridPoint = FindNearestXGridPoint();
    }

    float FindNearestXGridPoint()
    {
        float pos = transform.position.x;
        float closestGridPoint = 0.0f;
        float gridCount = 0;

        //Find how many times 5 goes into the position
        gridCount = pos / 5;
        //Round the result to the nearest whole number
        gridCount = Mathf.Round(gridCount);
        //Multiply 5 by the rounded result to get the closest grid point
        closestGridPoint = 5 * gridCount;

        return closestGridPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if(!_sliding)
        {
            Debug.Log("slotted into grid");
            transform.position = new Vector3 (_nearestGridPoint, transform.position.y, transform.position.z);
        }
    }

    public void StartSliding()
    {
        _sliding = true;
        Invoke(nameof(StopSliding), 0.5f);
        Debug.Log("Started Sliding");
    }
    void StopSliding()
    {
        _sliding = false;
        Debug.Log("Stopped Sliding");
    }

    void LateUpdate()
    {
        if (!_sliding)
        {
            _nearestGridPoint = FindNearestXGridPoint();
        }
    }
}
