using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public EnemyData enemyData;

    public int currentHp;
    public Animator animator;
    public float timeToInactive;

    protected Coroutine _currentCoroutine;

    public void Init()
    {
        currentHp = enemyData.maxHp;
        animator = GetComponent<Animator>();
        
    }

    public void End()
    {
        gameObject.SetActive(false);
    }

    void Awake()
    {
        Invoke("End", timeToInactive);
    }

    void OnEnable()
    {
        Invoke("End", timeToInactive);
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        
    }
}
