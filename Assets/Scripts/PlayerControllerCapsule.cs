using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerControllerCapsule : MonoBehaviour
{
    public bool jumpInput;
    public bool isJumping;
    public int jumpCount;
    public enum Animals
    {
        Shark,
        Gorilla,
        Vulture
    }
    public Animals animal;
    public PoolManager poolManager { get; private set; }
    public Transform spawnPoint;
    public Vector3 throwVector;

    private Animator _animator;
    private Coroutine _currentCoroutine;
    


    void Awake()
    {
        _animator = GetComponent<Animator>();    

        GameObject poolManagerObject = GameObject.Find("Pool Manager");
        poolManager = poolManagerObject.GetComponent<PoolManager>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputCheck();
        //SwitchCaseCheck();
    }

    public IEnumerator Jump()
    {
        _animator.SetTrigger("Jump");
        isJumping = true;
        jumpCount += 1;

        yield return new WaitForSeconds(0.5f);

        isJumping = false;
        _currentCoroutine = null;
    }

    public void Spawn()
    {
        var obj = poolManager.GetPooledObject();

        obj.SetActive(true);
        //obj.transform.SetParent(null);
        obj.transform.position = spawnPoint.transform.position;

        var rb = obj.GetComponent<Rigidbody>();
        rb.AddForce(this.throwVector);
    }

    private void InputCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.A))
        {
            jumpInput = true;
            if (_currentCoroutine == null)
            {
                _currentCoroutine = StartCoroutine(Jump());
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.A))
        {
            jumpInput = false;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            animal = Animals.Shark;
            SwitchCaseCheck();
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            animal = Animals.Gorilla;
            SwitchCaseCheck();
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            animal = Animals.Vulture;
            SwitchCaseCheck();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Spawn();
        }
    }

    private void SwitchCaseCheck()
    {
        switch (animal)
        {
            case Animals.Shark:
                Debug.Log("SHARK!!!");
                break;
            case Animals.Gorilla:
                Debug.Log("GORILLA!!!");
                break;
            case Animals.Vulture:
                Debug.Log("VULTURE!!!");
                break;
        }
    }
}
