using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;
using UnityEditor;


//Spawning order
//Starts off at 1 box every 5 seconds
//Every 10 seconds it decreases the timer by .1 Ex: at 1:20 seconds it would be 1 box every 4.2 seconds
//Then, once the timer gets down to 1 every 2 seconds the timer will double and start spawning 2 boxes at once
//So it would be 2 boxes every 4 seconds which would happen at the 5 minute mark
//Then it would start decreasing the timer by .05 every 10 seconds
//Once it reaches 2 boxes every 3 seconds the the timer will increase by 2 but also increase the box count to 3
//So it would be 3 boxes every 5 seconds
//The it would decrease the timer by .1 capping out at 3 boxes every 3 seconds


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
    [Range(0, 5), SerializeField]
    private int _amountToSpawnStart = 1;
    //Should start off at 1
    private int _amountToSpawn;

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

    [Header("Game Manager")]
    [SerializeField]
    private GameObject gameManager;
    private PlayerTimer playerTimer;
    private float difficultyTimer = 0.0f;
    private int difficultyLevel = 0;

    //The grid will be a _gridWidth * _gridHeight grid where the grid points will be spaced out by _boxSize
    //So the values we are sticking with (10 width, 10 height, and 5 box size) will make a 10*10 grid with points spaced out by 5

    private bool _spawningActive;
    private float _spawnRate;
    
    private float boxNextSpawn = 0.0f;
    bool _incremented = false;
    bool _hellModeActive = false;

    public int GridWidth { get { return _gridWidth; } }
    public int GridHeight { get { return _gridHeight; } }
    public int BoxSize { get { return _boxSize; } }

    public int DifficultyLevel { get { return difficultyLevel; } }

    public int AmountSpawning { get { return _amountToSpawn; } }

    public float SpawnRate { get { return _spawnRate; } }

    // Start is called before the first frame update
    void Start()
    {
        ResetDifficulty();
        playerTimer = gameManager.GetComponent<PlayerTimer>();

        _spawningActive = true;
        Invoke(nameof(SpawnTargets), 0);
    }

    private void DisableSpawning()
    {
        _spawningActive = false;
    }

    //The whole spawning functinality
    void SpawnTargets()
    {
        //Gets a random X from 0 - _gridWidth to use to find a random spawning position for the boxes that doesnt overlap with any other random position
        int randomX1 = Random.Range(0, _gridWidth);

        int randomX2 = Random.Range(0, _gridWidth);
        //If randomX2 is the same as randomX1, reroll until different
        while (randomX2 == randomX1)
        {
            randomX2 = Random.Range(0, _gridWidth);
        }

        int randomX3 = Random.Range(0, _gridWidth);
        //If randomX3 is the same as randomX1 or randomX2 reroll until different than either
        while (randomX3 == randomX1 || randomX3 == randomX2)
        {
            randomX3 = Random.Range(0, _gridWidth);
        }

        int randomX4 = Random.Range(0, _gridWidth);
        //If randomX4 is the same as randomX1, randomX2, or randomX3 reroll until different than all
        while (randomX4 == randomX1 || randomX4 == randomX2 || randomX4 == randomX3)
        {
            randomX4 = Random.Range(0, _gridWidth);
        }

        int randomX5 = Random.Range(0, _gridWidth);
        //If randomX5 is the same as randomX1, randomX2, randomX3, or randomX4 reroll until different than all
        while (randomX5 == randomX1 || randomX5 == randomX2 || randomX5 == randomX3 || randomX5 == randomX4)
        {
            randomX5 = Random.Range(0, _gridWidth);
        }

        for (int i = 0; i < _amountToSpawn; i++)
        {
            float chosenRandomX = 0.0f;
            if (i == 0)
            {
                chosenRandomX = randomX1;
            }
            if (i == 1)
            {
                chosenRandomX = randomX2;
            }
            if (i == 2)
            {
                chosenRandomX = randomX3;
            }
            if (i == 3)
            {
                chosenRandomX = randomX4;
            }
            if (i == 4)
            {
                chosenRandomX = randomX5;
            }

            float randomPositionX = chosenRandomX * _boxSize;
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
            else if (boxChoice <= 105 && ObjectPool.SharedInstance.useRocket)
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
                if (ObjectPool.SharedInstance.useGreen)
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
                Invoke(nameof(SpawnTargets), _spawnRate);
                return;
            }

            GameObject Box = ObjectPool.SharedInstance.GetSpecifiedPooledObject(boxToSpawn);

            ObjectPool.SharedInstance.ActivateAnObject(Box);
            Box.transform.localScale = new Vector3(_boxSize, _boxSize, _boxSize);
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
        }

        if (_spawningActive)
        {
            Invoke(nameof(SpawnTargets), _spawnRate);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _hellModeActive = gameManager.GetComponent<GameManager>().HellModeEnabled;

        Debug.Log("Hellmode: " + _hellModeActive);

        if(_hellModeActive)
        {
            difficultyTimer = 10;
            _spawnRate = 1;
            _amountToSpawn = 3;

            return;
        }

        if (Mathf.FloorToInt(playerTimer.CurrentTime) == difficultyTimer)
        {
            if (!_incremented)
            {
                Invoke(nameof(DifficultyIncrement), 0);
            }
        }

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
            Invoke(nameof(SpawnTargets), 0);
        }
    }

    void ResetDifficulty()
    {
        difficultyTimer = 10;
        _spawnRate = _startingSpawnRate;
        _amountToSpawn = _amountToSpawnStart;
    }

    void DifficultyIncrement()
    {
        _incremented = true;
        Invoke(nameof(HasntIncremented), 10.0f);

        difficultyTimer += 10;
        difficultyLevel++;

        //Decrement spawnrate by 0.1 every difficulty level
        if (difficultyLevel < 15)
        {
            _spawnRate -= 0.2f;
        }
        //Set spawnrate to 4 and amount to spawn to 2 at difficlty 15 (2:30 minutes in)
        else if (difficultyLevel == 15)
        {
            _spawnRate = 4;
            _amountToSpawn = 2;
        }
        //Decrement spawnrate by 0.05 every difficulty level
        else if (difficultyLevel < 35)
        {
            _spawnRate -= 0.05f;
        }
        //Set spawnrate to 5 and amount to spawn to 3 at difficlty 50 (8 minutes 20 in)
        else if (difficultyLevel == 35)
        {
            _spawnRate = 5;
            _amountToSpawn = 3;
        }
        //Decrement spawnrate by 0.1 every difficulty level
        else if (difficultyLevel < 70)
        {
            _spawnRate -= 0.1f;
        }
        //Decrement spawnrate by 0.1 every difficulty level
        else if (difficultyLevel >= 70)
        {

        }
    }

    void HasntIncremented()
    {
        _incremented = false;
    }
}