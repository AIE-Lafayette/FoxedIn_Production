using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;

    [Header("ObjectPool")]
    public List<GameObject> pooledObjects;

    [Header("Objects in pool")]
    public GameObject brownToPool;
    public GameObject greenToPool;
    public GameObject blueToPool;
    public GameObject goldToPool;

    [Header("Objects To Use")]
    public bool useBrown = false;
    public bool useGreen = false;
    public bool useBlue = false;
    public bool useGold = false;

    //[Header("Amount of each object in pool")]
    //[Range(0, 100)]
    int amountToPool = 100;

    void Awake()
    {
        //Makes the shared instance
        SharedInstance = this;
    }

    //Get the first not active object in the pool
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
    //Gets the first non active object of a specified type
    public GameObject GetSpecifiedPooledObject(GameObject gameObject)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i].activeInHierarchy)
            {
                continue;
            }

            if (pooledObjects[i].gameObject.tag == gameObject.tag)
            {
                return pooledObjects[i];
            }
        }
        //If all objects of type are already active return null
        return null;
    }

    //Activates the first gameObject that is the same as the one entered
    public GameObject ActivateAnObject(GameObject gameObject)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i].activeInHierarchy)
            {
                continue;
            }

            if (pooledObjects[i].gameObject.tag == gameObject.tag)
            {
                pooledObjects[i].SetActive(true);
                return pooledObjects[i];
            }
        }
        //If all objects of type are already active return null
        return null;
    }

    //Activates a random object in pool
    public GameObject ActivateRandomObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            int chosen = Random.Range(0, pooledObjects.Count);

            if (pooledObjects[chosen].activeInHierarchy)
            {
                continue;
            }

            pooledObjects[chosen].SetActive(true);
            return pooledObjects[i];
        }

        return null;
    }
    //Returns a int count of all currently active objects
    public int ActiveObjectCount()
    {
        int count = 0;

        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i].activeInHierarchy)
            {
                count++;
            }
        }

        return count;
    }
    //Returns a list of all currently active objects
    public List<GameObject> AllActiveObjects()
    {
        List<GameObject> activeObjects = new List<GameObject>();

        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i].activeInHierarchy)
            {
                activeObjects.Add(pooledObjects[i]);
            }
        }

        if (activeObjects.Count >= 1)
        {
            return activeObjects;
        }

        return null;
    }

    //Gets an object in the pool using its int position in the pool
    public GameObject GetActiveObject(int Object)
    {
        return pooledObjects[Object];
    }
    


    //Add the object given into the pool
    public void AddObjectToPool(GameObject addedObject)
    {
        GameObject temp;
        temp = Instantiate(addedObject);
        temp.SetActive(false);
        pooledObjects.Add(temp);
    }

    void Start()
    {
        //Makes the pool
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            if (useGold)
            {
                AddObjectToPool(goldToPool);
            }
            if (useBlue)
            {
                AddObjectToPool(blueToPool);
            }
            if (useGreen)
            {
                AddObjectToPool(greenToPool);
            }
            if (useBrown)
            {
                AddObjectToPool(brownToPool);
            }
        }
    }
}