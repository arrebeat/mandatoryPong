using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;

public class GridManager : MonoBehaviour
{
    public GameManager gameManager { get; private set; }

    public List<GameObject> gridPrefabs = new List<GameObject>();
    public List<GameObject> pooledObjects;
    public int gridPoolAmount;
    public float startDelay;
    public float delayBetweenCells;

    private GameObject _containerGrid;
    private Coroutine _coroutine;

    void Awake()
    {
        GameObject gameManagerObject = GameObject.Find("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();

        _containerGrid = GameObject.Find("Container: Grid");
    }

    void Start()
    {
        StartPool();
        
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(ShowGrid());
        }
    }

    void Update()
    {
        
    }

    public void StartPool()
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < gridPoolAmount; i++)
        {
            var obj = Instantiate(gridPrefabs[0], _containerGrid.transform);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public IEnumerator ShowGrid()
    {
        yield return new WaitForSeconds(startDelay);

        foreach(var obj in pooledObjects)
        {
            yield return new WaitForSeconds(delayBetweenCells);
            obj.SetActive(true);
            obj.GetComponent<Animator>().SetTrigger("Enter");
        }
        
        _coroutine = null;
        gameManager.StartMatch();
        yield return null;
    }
}
