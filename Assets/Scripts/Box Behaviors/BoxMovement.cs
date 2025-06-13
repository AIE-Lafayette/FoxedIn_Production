using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BoxMovement : MonoBehaviour
{
    public GameObject player;

    private float _nearestGridPoint;
    private bool _sliding;
    private bool _hitFromLeft;
    private bool _anObjectToLeft;
    private GameObject _objectToLeft;
    private bool _anObjectToRight;
    private GameObject _objectToRight;
    private float _distancePerIter = 0.15f;
    private float _distanceTracker = 0.0f;

    public bool IsSliding { get { return _sliding; } }

    //True means hit from left false means hit from right
    public bool HitDirection { get { return _hitFromLeft; } }



    // Start is called before the first frame update
    void Start()
    {
        Collider boxesCollider = GetComponent<BoxCollider>();
        Collider playerCollider = player.GetComponent<CapsuleCollider>();
        Physics.IgnoreCollision(boxesCollider, playerCollider);

        _sliding = false;
        _nearestGridPoint = FindNearestXGridPoint();
    }

    public void StartSliding()
    {
        _sliding = true;
    }
    void StopSliding()
    {
        _sliding = false;
        _distanceTracker = 0;
    }

    //True means hit from left false means hit from right
    public void SetHitDirection(bool boxWasHitFromLeft)
    {
        _hitFromLeft = boxWasHitFromLeft;
    }

    float FindNearestXGridPoint()
    {
        float pos = transform.position.x;
        float closestGridPoint = 0.0f;
        float gridCount = 0;

        //Find how many times 5 (the size of a box) goes into the position
        gridCount = pos / 5;
        //Round the result to the nearest whole number
        gridCount = Mathf.Round(gridCount);
        //Multiply 5 (the size of a box) by the rounded result to get the closest grid point
        closestGridPoint = 5 * gridCount;

        return closestGridPoint;
    }

    // Update is called once per frame
    void Update()
    {
        #region "Raycast Checks"

        //Left Cast
        if (Physics.Raycast(transform.position, new Vector3(-1, 0, 0), transform.localScale.x / 2))
        {
            _anObjectToLeft = true;
            if (!_hitFromLeft)
            {
                StopSliding();
            }
        }
        else
        {
            _anObjectToLeft = false;
        }
        //Right Cast
        if (Physics.Raycast(transform.position, new Vector3(1, 0, 0), transform.localScale.x / 2))
        {
            _anObjectToRight = true;
            if (_hitFromLeft)
            {
                StopSliding();
            }
        }
        else
        {
            _anObjectToRight = false;
        }

        #endregion

        if (!_sliding)
        {
            transform.position = new Vector3 (FindNearestXGridPoint(), transform.position.y, transform.position.z);
            
            _distanceTracker = 0;
            return;
        }

        //If the box was hit from the left (meaning the player was on the left on the box)
        if (_hitFromLeft == true && _anObjectToRight == false)
        {
            //Push box to Right

            //Used
            #region "Box pushing with translate"

            //Setting iter value
            _distancePerIter = Mathf.Abs(_distancePerIter);

            //Translating right
            Vector3 translate = new Vector3(_distancePerIter, 0.0f, 0.0f);
            transform.Translate(translate);

            //Increment __distanceTracker
            _distanceTracker += _distancePerIter;

            //Stop sliding after translating a total of a box to the right
            if (_distanceTracker >= 5)
            {
                _nearestGridPoint = FindNearestXGridPoint();
                StopSliding();
            }

            #endregion

            //Unused
            #region "Box pushing with velocity"

            //boxRigidBody.velocity = new Vector3(15, 0, 0);
            //Invoke(nameof(StopSliding), 0.5f);

            #endregion

        }
        //If the box was hit from the right (meaning the player was on the right on the box)
        else if (_hitFromLeft == false && _anObjectToLeft == false)
        {
            //Push box to left

            //Used
            #region "Box pushing with translate"

            //Setting iter value
            _distancePerIter = Mathf.Abs(_distancePerIter);
            _distancePerIter = _distancePerIter * -1;

            //Translating left
            Vector3 translate = new Vector3(_distancePerIter, 0.0f, 0.0f);
            transform.Translate(translate);

            //Increment i
            _distanceTracker -= _distancePerIter;

            //Stop sliding after translating a total of a box to the left
            if (_distanceTracker >= 5)
            {
                _nearestGridPoint = FindNearestXGridPoint();
                StopSliding();
            }

            #endregion

            //Unused
            #region "Box pushing with velocity"

            //boxRigidBody.velocity = new Vector3(-15, 0, 0);
            //Invoke(nameof(StopSliding), 0.5f); 

            #endregion

        }

    }

    private void OnDisable()
    {
        StopSliding();
    }

    void LateUpdate()
    {
        if (!_sliding)
        {
            _nearestGridPoint = FindNearestXGridPoint();
        }
    }
}
