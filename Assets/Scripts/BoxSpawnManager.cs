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

    [SerializeField]
    float _startingSpawnRate = 4.0f;

    GameObject[] _Boxes;

    [SerializeField]
    private int _gridWidth = 10;
    [SerializeField]
    private int _gridHeight = 10;
    [SerializeField]
    private int _BoxSize = 5;

    private bool _spawningActive;

    // Start is called before the first frame update
    void Start()
    {
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
        return _BoxSize;
    }

    private void DisableSpawning()
    {
        _spawningActive = false;
    }

    void SpawnTarget()
    {
        _spawningActive = true;

        float randomX = Random.Range(0, _gridWidth);
        float randomPositionX = randomX * _BoxSize;
        Vector3 randomPosition = new Vector3(randomPositionX, _gridHeight * _BoxSize, 5);


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

        GameObject Box = ObjectPool.SharedInstance.ActivateAnObject(boxToSpawn);
        if (Box == null)
        {
            GameObject temp = Instantiate(boxToSpawn);
            temp.SetActive(false);
            ObjectPool.SharedInstance.AddObjectToPool(boxToSpawn);
            Box = temp;
        }

        //Box = ObjectPool.SharedInstance.ActivateAnObject(boxChoice);
        Box.transform.position = randomPosition;
        Box.transform.localScale = new Vector3(_BoxSize, _BoxSize, 10);


        Invoke(nameof(SpawnTarget), _startingSpawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (_spawningActive == false)
        {
            Invoke(nameof(SpawnTarget), 0);
        }
    }
}
