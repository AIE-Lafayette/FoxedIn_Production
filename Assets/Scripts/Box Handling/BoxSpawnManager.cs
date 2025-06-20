using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;
using UnityEditor;

public class BoxSpawner : MonoBehaviour
{
    //[SerializeField]
    //GameObject _brownBox;
    //[SerializeField]
    //GameObject _greenBox;zzzz
    //[SerializeField]
    //GameObject _blueBox;
    //[SerializeField]
    //GameObject _goldBox;

    [Header ("Spawning")]
    [Range(0, 5), SerializeField]
    float _startingSpawnRate = 4.0f;
    [Range(0, 100), SerializeField]
    int _spawnLimit = 100;

    [Header ("Debug Spawning (ONLY ONE AT A TIME)")]
    [SerializeField]
    public bool _debugSpawnInOneSpot = false;
    [Range(0, 45)]
    float _debugSpawnLocation = 15.0f;
    [SerializeField]
    public bool _debugSpawnLeftToRight = false;

    [Header("Grid Setting")]
    [SerializeField]
    private int _gridWidth = 10;
    [SerializeField]
    private int _gridHeight = 10;

    [Header("Box Setting")]
    [SerializeField]
    private int _boxSize = 5;

    //The grid will be a _gridWidth * _gridHeight grid where the grid points will be spaced out by _boxSize
    //So the values we are sticking with (10 width, 10 height, and 5 box size) will make a 10*10 grid with points spaced out by 5

    private bool _spawningActive;
    private float _spawnRate;

    private float boxNextSpawn = 0.0f;

    public int GridWidth { get { return _gridWidth; } }
    public int GridHeight { get { return _gridHeight; } }
    public int BoxSize { get { return _boxSize; } }

    // Start is called before the first frame update
    void Start()
    {
        _spawnRate = _startingSpawnRate;
        _spawningActive = true;
        Invoke(nameof(SpawnTarget), 0);
    }

    private void DisableSpawning()
    {
        _spawningActive = false;
    }

    //The whole spawning functinality
    void SpawnTarget()
    {
        //Gets a random X from 0-_gridWidth to use to find a random spawning position for the next box
        float randomX = Random.Range(0, _gridWidth);
        float randomPositionX = randomX * _boxSize;
        Vector3 randomPosition = new Vector3(randomPositionX, _gridHeight * _boxSize, 5);

        //Gets a number 0-100 to choose the next box
        int boxChoice = Random.Range(0, 105);
        GameObject boxToSpawn = null;

        //gold
        if (boxChoice < 5 && ObjectPool.SharedInstance.useGold)
        {
            boxChoice = 100;
            boxToSpawn = ObjectPool.SharedInstance.goldBoxToPool;
        }
        //blue
        else if (boxChoice < 20 && ObjectPool.SharedInstance.useBlue)
        {
            boxChoice = 50;
            boxToSpawn = ObjectPool.SharedInstance.blueBoxToPool;
        }
        //green
        else if (boxChoice < 50 && ObjectPool.SharedInstance.useGreen)
        {
            boxChoice = 25;
            boxToSpawn = ObjectPool.SharedInstance.greenBoxToPool;
        }
        //brown
        else if (boxChoice < 100 && ObjectPool.SharedInstance.useBrown)
        {
            boxChoice = 10;
            boxToSpawn = ObjectPool.SharedInstance.brownBoxToPool;
        }
        //bomb
        else if (boxChoice < 103 && ObjectPool.SharedInstance.useBomb)
        {
            boxToSpawn = ObjectPool.SharedInstance.bombBoxToPool;
        }
        //rocket
        else if(boxChoice <= 105 && ObjectPool.SharedInstance.useRocket)
        {
            boxToSpawn = ObjectPool.SharedInstance.rocketBoxToPool;
        }
        //brown
        else if (boxChoice > 0 && ObjectPool.SharedInstance.useBrown)
        {
            boxChoice = 10;
            boxToSpawn = ObjectPool.SharedInstance.brownBoxToPool;
        }

        //If number doesn't allign with anything that is enabled
        else
        {
            //Check if green is enabled
            if(ObjectPool.SharedInstance.useGreen)
            {
                //Spawn a green
                boxToSpawn = ObjectPool.SharedInstance.greenBoxToPool;
            }
            //Otherwise check if blue is enabled
            else if (ObjectPool.SharedInstance.useBlue)
            {
                //Spawn a blue
                boxToSpawn = ObjectPool.SharedInstance.blueBoxToPool;
            }
            //else check if gold is enabled
            else if (ObjectPool.SharedInstance.useGold)
            {
                //Spawn a gold
                boxToSpawn = ObjectPool.SharedInstance.goldBoxToPool;
            }
            //if all else fails
            else
            {
                //Spawn a rocket or a bomb

                // If bomb is disabled use rocket
                if (!(ObjectPool.SharedInstance.useBomb))
                {
                    boxToSpawn = ObjectPool.SharedInstance.rocketBoxToPool;
                }
                // If rocket is disabled use bomb
                if (!(ObjectPool.SharedInstance.useRocket))
                {
                    boxToSpawn = ObjectPool.SharedInstance.bombBoxToPool;
                }

                // If both are active choose one
                int ranBox = Random.Range(0, 100);
                //If ranBox is one use bomb
                if (ranBox < 50)
                {
                    boxToSpawn = ObjectPool.SharedInstance.bombBoxToPool;
                }
                //If ranBox is two use rocket
                if (ranBox >= 50)
                {
                    boxToSpawn = ObjectPool.SharedInstance.rocketBoxToPool;
                }
            }
        }

        if (boxToSpawn == null)
        {
            Invoke(nameof(SpawnTarget), _spawnRate);
            return;
        }

        GameObject Box = ObjectPool.SharedInstance.GetSpecifiedPooledObject(boxToSpawn);
        //If somehow all the boxes of this color are already active then instantiate a new one
        if (Box == null)
        {
            GameObject temp = Instantiate(boxToSpawn);
            temp.SetActive(false);
            ObjectPool.SharedInstance.AddObjectToPool(boxToSpawn);
            Box = temp;
        }

        ObjectPool.SharedInstance.ActivateAnObject(Box);
        Box.transform.localScale = new Vector3(_boxSize, _boxSize, 7.5f);
        Box.transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 5, 0);

        if (!(_debugSpawnLeftToRight) && !(_debugSpawnInOneSpot))
        {
            Box.transform.position = randomPosition;
        }

        #region "Debug Spawn Options"

        //For testing, boxes will all fall in a specific column
        if (_debugSpawnInOneSpot)
        {
            Box.transform.position = new Vector3(_debugSpawnLocation, _gridHeight * _boxSize, 5);
        }

        if (_debugSpawnLeftToRight)
        {
            //For testing, boxes will all fall in a row, Left to right
            Box.transform.position = new Vector3(boxNextSpawn, _gridHeight * _boxSize, 5);
            if (boxNextSpawn >= 45)
            {
                boxNextSpawn = 0;
            }
            else
            {
                boxNextSpawn += 5;
            }
        }

        #endregion

        if (_spawningActive)
        {
            Invoke(nameof(SpawnTarget), _spawnRate);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If the spawn limit is reached prevent spawning
        if (ObjectPool.SharedInstance.ActiveObjectCount() >= _spawnLimit - 1)
        {
            _spawningActive = false;
            return;
        }
        //Activates spawning if it was false
        else if (_spawningActive == false)
        {
            _spawningActive = true;
            Invoke(nameof(SpawnTarget), 0);
        }
    }
}