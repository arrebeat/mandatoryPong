using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    public int maxHp;

    [Header("Jumper")]
    public float jumpDuration;
    public float jumpRate;
    
    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
