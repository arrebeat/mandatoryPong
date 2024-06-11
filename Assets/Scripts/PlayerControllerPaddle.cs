using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerPaddle : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public float moveInput;
    public Vector3 currentPosition;
    public bool isMovingUp;
    public bool isSliding;
    public bool isMovingDown;
    

    [Header("Move Engine")]
    [Range(0, 10)]
    public float moveTime;
    public float moveAccelTime;
    public float moveDeccelTime;
    [Range(0, 1)]
    public float moveNormalizedTime;
    public float moveMaxSpeed;
    [Range(0, 10)]
    public float moveSpeed;


    public EnemyControllerPaddle enemyPaddle;

    private PlayerControls _playerControls;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        _playerControls = new PlayerControls();

        _playerControls.InGame.MovePlayer.started += OnMovePlayer_Started;
        _playerControls.InGame.MovePlayer.performed += OnMovePlayer_Performed;
        _playerControls.InGame.MovePlayer.canceled += OnMovePlayer_Canceled;

        GameObject enemyPaddleObject = GameObject.Find("PaddleEnemy");
        enemyPaddle = enemyPaddleObject.GetComponent<EnemyControllerPaddle>();
    }

    void OnEnable() 
    {
        _playerControls.Enable();
    }

    void OnDisable()
    {
        _playerControls.Disable();
    }

    void Start()
    {
        
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        //check movement value
        moveInput = _playerControls.InGame.MovePlayer.ReadValue<float>();
        currentPosition = transform.position;

        //check movement direction
        if (moveInput != 0 && moveSpeed != 0)
        {
            if (moveInput > 0)
            {
                isMovingUp = true;
                isMovingDown = false;
            }
            else if (moveInput < 0)
            {
                isMovingDown = true;
                isMovingUp = false;
            }
        }
        else if (moveInput == 0 && moveSpeed == 0)
        {
            isMovingUp = false;
            isMovingDown = false;
            isSliding = false;
        }

        //movement acceleration
        if (moveInput != 0)
        {
            moveNormalizedTime = moveTime / moveAccelTime;
            moveTime = Mathf.Clamp(moveTime, 0, moveAccelTime);
            moveTime -= Time.deltaTime;
            
            moveSpeed = Mathf.Lerp(moveMaxSpeed, 0, moveNormalizedTime);
        }
        //movement decceleration
        else if (moveInput == 0)
        {
            moveNormalizedTime = moveTime / moveDeccelTime;
            moveTime = Mathf.Clamp(moveTime, 0, moveDeccelTime);
            moveTime -= Time.deltaTime;

            moveSpeed = Mathf.Lerp(0, moveMaxSpeed, moveNormalizedTime);

            if (moveTime < 0)
            {
                moveTime = 0;
            }
        }

        //move paddle
        if (moveInput != 0)
        {
            if (moveInput > 0)
            {
                if (isMovingDown && isSliding)
                {
                    currentPosition.y -= moveSpeed * Time.deltaTime;
                }
                else
                {
                    currentPosition.y += moveInput * moveSpeed * Time.deltaTime;
                }
            }
            else if (moveInput < 0)
            {
                if (isMovingUp && isSliding)
                {
                    currentPosition.y += moveSpeed * Time.deltaTime;
                }
                else
                {
                    currentPosition.y += moveInput * moveSpeed * Time.deltaTime;
                }
            }

            transform.position = currentPosition;
        }
        //drag paddle
        else if (moveInput == 0)
        {
            if (isMovingUp)
            {
                isSliding = true;
                currentPosition.y += moveSpeed * Time.deltaTime;
            }
            else if (isMovingDown)
            {
                isSliding = true;
                currentPosition.y -= moveSpeed * Time.deltaTime;
            }
            
            transform.position = currentPosition;
        }
    }

    #region INPUT CALLBACKS
    private void OnMovePlayer_Started(InputAction.CallbackContext context)
    {
        //move
        if (!isSliding)
        {
            moveTime = (moveSpeed == 0) ? moveAccelTime : moveAccelTime * (1 - moveNormalizedTime);
        }
    }
    private void OnMovePlayer_Performed(InputAction.CallbackContext context)
    {
        //pressing UP while sliding DOWN
        if (moveInput > 0 && isMovingDown && isSliding)
        {
            moveTime = (moveSpeed == moveMaxSpeed) ? moveDeccelTime : moveDeccelTime * (1 - moveNormalizedTime);    
        }
        //pressing DOWN while sliding UP
        if (moveInput < 0 && isMovingUp && isSliding)
        {
            moveTime = (moveSpeed == moveMaxSpeed) ? moveDeccelTime : moveDeccelTime * (1 - moveNormalizedTime);
        }
    }
    private void OnMovePlayer_Canceled(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        if (moveSpeed != 0)
        {
            isSliding = true;
        }

        moveTime = (moveSpeed == moveMaxSpeed) ? moveDeccelTime : moveDeccelTime * (1 - moveNormalizedTime);
    }
    #endregion

    
}
