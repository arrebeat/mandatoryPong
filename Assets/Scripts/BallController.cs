using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameManager gameManager { get; private set; }
    public SpriteRenderer[] spriteRenderers { get; private set; }
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Vector2 startPosition;
    public Vector2 startDirection;
    public float throwForce;
    public float speedIncrement;

    void Awake()
    {
        GameObject gameManagerObject = GameObject.Find("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();

        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        //SetBall();
    }


    void Update()
    {
        
    }

    public void StartBall()
    {
        transform.position = startPosition;
        gameObject.SetActive(true);

        float randX = UnityEngine.Random.Range(0.5f, 1.1f);
        float randY = UnityEngine.Random.Range(0.5f, 1.1f);

        float rand50X = UnityEngine.Random.Range(0,101);
        if (rand50X <= 50)
        {
            float newRandX = -randX;
            randX = newRandX;
        }

        float rand50Y = UnityEngine.Random.Range(0,101);
        if (rand50Y <= 50)
        {
            float newRandY = -randY;
            randY = newRandY;
        }

        Vector2 newRandomDirection = new Vector2(randX, randY);
        startDirection = newRandomDirection;

        rb.velocity = startDirection * throwForce;
    }

    public void ProbabilityCheck(float randX, float RandY)
    {
        float rand = UnityEngine.Random.Range(0, 101);
        if (rand <= 50)
        {
            
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector2 newVelocity = rb.velocity;

            newVelocity.x = -newVelocity.x;
            rb.velocity = newVelocity * speedIncrement;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            Vector2 newVelocity = rb.velocity;

            newVelocity.x = -newVelocity.x;
            rb.velocity = newVelocity * speedIncrement;
        }

        if (other.gameObject.CompareTag("Wall"))
        {    
            Vector2 newVelocity = rb.velocity;

            newVelocity.y = -newVelocity.y;
            rb.velocity = newVelocity;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.name);

        if (other.gameObject.CompareTag("PointPlayer"))
        {
            gameObject.SetActive(false);
            gameManager.ScoreGoal(0);
        }

        if (other.gameObject.CompareTag("PointEnemy"))
        {
            gameObject.SetActive(false);
            gameManager.ScoreGoal(1);
        }
    }
}
