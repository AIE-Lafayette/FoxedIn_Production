using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

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

    // Start is called before the first frame update
    void Start()
    {
        _spawnRate = _startingSpawnRate;
        _spawningActive = true;
        Invoke(nameof(SpawnTarget), 0);
    }

    public int GridWidth()
    {
        return _gridWidth;
    }
    public int GridHeight()
    {
        return _gridHeight;
    }
    public int BoxSize()
    {
        return _boxSize;
    }

    private void DisableSpawning()
    {
        _spawningActive = false;
    }

    //The whole spawning functinality
    void SpawnTarget()
    {
        _spawningActive = true;

        //Gets a random X from 0-_gridWidth to use to find a random spawning position for the next box
        float randomX = Random.Range(0, _gridWidth);
        float randomPositionX = randomX * _boxSize;
        Vector3 randomPosition = new Vector3(randomPositionX, _gridHeight * _boxSize, 5);

        //Gets a number 0-100 to choose the next box
        int boxChoice = Random.Range(0, 100);
        GameObject boxToSpawn;
        //5% chance for gold
        if (boxChoice < 5)
        {
            boxChoice = 100;
            boxToSpawn = ObjectPool.SharedInstance.goldToPool;
        }
        //15% chance for blue
        else if (boxChoice < 20)
        {
            boxChoice = 50;
            boxToSpawn = ObjectPool.SharedInstance.blueToPool;
        }
        //30% chance for green
        else if (boxChoice < 50)
        {
            boxChoice = 25;
            boxToSpawn = ObjectPool.SharedInstance.greenToPool;
        }
        //50% chance for brown
        else
        {
            boxChoice = 10;
            boxToSpawn = ObjectPool.SharedInstance.brownToPool;
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
        Box.transform.position = randomPosition;
        Box.transform.localScale = new Vector3(_boxSize, _boxSize, 10);


        Invoke(nameof(SpawnTarget), _spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        //Activates spawning if it was false
        if (_spawningActive == false)
        {
            Invoke(nameof(SpawnTarget), 0);
        }
    }
}
