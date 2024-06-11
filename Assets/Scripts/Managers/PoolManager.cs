using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    public List<GameObject> prefabs = new List<GameObject>();
    public List<GameObject> pooledObjects;
    public PlayerControllerCapsule player { get; private set; }
    public int poolAmount;
    

    void Awake()
    {
        GameObject playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<PlayerControllerCapsule>();
    }

    void Start()
    {
        StartPool();
    }


    void Update()
    {
    
    }

    public void StartPool()
    {
        pooledObjects = new List<GameObject>();
        //int rand = UnityEngine.Random.Range(0, 2);

        for (int i = 0; i < poolAmount; i++)
        {
            var obj = Instantiate(prefabs[0], transform);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < poolAmount; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;   
    }
}
