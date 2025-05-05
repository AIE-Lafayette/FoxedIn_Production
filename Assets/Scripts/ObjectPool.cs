using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject brownToPool;
    public GameObject greenToPool;
    public GameObject blueToPool;
    public GameObject goldToPool;
    public int amountToPool;

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

        for (int i = 0; i < amountToPool * 4; i++)
        {
            if (pooledObjects[i].activeInHierarchy)
            {
                count++;
            }
        }

        return count;
    }
    //Gets an object in the pool
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
            AddObjectToPool(goldToPool);
            AddObjectToPool(blueToPool);
            AddObjectToPool(greenToPool);
            AddObjectToPool(brownToPool);
        }
    }
}