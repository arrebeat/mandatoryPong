using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject canvas { get; private set; }
    
    public GameObject containerScore { get; private set;}
    public TextMeshProUGUI[] scoreValueTexts { get; private set; }
    public TextMeshProUGUI scoreValueTextPlayer { get; private set; }
    public TextMeshProUGUI scoreValueTextEnemy { get; private set; }

    public GameObject containerEndGame { get; private set;}
    public TextMeshProUGUI endGameText;
    
    public PlayerControllerPaddle player { get; private set; }
    public EnemyControllerPaddle enemy { get; private set; }
    public BallController ball { get; private set; }
    

    //public SaveController saveController { get; private set; }
    
    public Vector3 playerStartPosition;
    public Vector3 enemyStartPosition;
    public float ballSetDelay;

    [Header("Score")]
    public int scorePlayer;
    public int scoreEnemy;
    public int scoreWin;

    private Coroutine _coroutine;

    void Awake()
    {
        canvas = GameObject.Find("Canvas");
        containerScore = GameObject.Find("Container: Score");
        containerEndGame = GameObject.Find("Container: EndGame");

        scoreValueTexts = canvas.GetComponentsInChildren<TextMeshProUGUI>();
        scoreValueTextPlayer = scoreValueTexts[0];
        scoreValueTextEnemy = scoreValueTexts[1];

        endGameText = containerEndGame.GetComponentInChildren<TextMeshProUGUI>();

        GameObject playerObject = GameObject.Find("PaddlePlayer");
        player = playerObject.GetComponent<PlayerControllerPaddle>();

        GameObject enemyObject = GameObject.Find("PaddleEnemy");
        enemy = enemyObject.GetComponent<EnemyControllerPaddle>();

        GameObject ballObject = GameObject.Find("Ball");
        ball = ballObject.GetComponent<BallController>();

        //GameObject saveControllerObject = GameObject.Find("SaveController");
        //saveController = saveControllerObject.GetComponent<SaveController>();
    }

    void Start()
    {
        Init();
    }
    
    void Update()
    {
        
    }

    public void Init()
    {
        containerScore.SetActive(false);
        containerEndGame.SetActive(false);
        
        player.spriteRenderer.enabled = false;
        player.spriteRenderer.material = SaveController.Instance.materialPlayer;
        enemy.spriteRenderer.enabled = false;
        enemy.spriteRenderer.material = SaveController.Instance.materialEnemy;
        
        foreach(var obj in ball.spriteRenderers)
        {
            obj.enabled = false;
        }
    }


    public void StartMatch()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(StartMatchHandler());
        }
    }
    public IEnumerator StartMatchHandler()
    {
        containerScore.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        player.transform.position = playerStartPosition;
        enemy.transform.position = enemyStartPosition;
        
        player.spriteRenderer.enabled = true;
        enemy.spriteRenderer.enabled = true;

        yield return new WaitForSeconds(0.5f);
        foreach (var obj in ball.spriteRenderers)
        {
            obj.enabled = true;
        }

        yield return new WaitForSeconds(1f);
        ball.StartBall();

        _coroutine = null;
        yield return null;
    }

    public void EndMatch()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(EndMatchHandler());
        }
    }
    public IEnumerator EndMatchHandler()
    {
        string winner = SaveController.Instance.GetName(scorePlayer > scoreEnemy);
        endGameText.text = "_win: " + winner;

        SaveController.Instance.SaveWinner(winner);
        yield return new WaitForSeconds(1f);
        containerEndGame.SetActive(true);

        _coroutine = null;
        yield return null;
    }

    public void ResetMatch()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(ResetMatchHandler());
        }
    }
    public IEnumerator ResetMatchHandler()
    {
        scorePlayer = 0;
        scoreEnemy = 0;
        scoreValueTextPlayer.text = scorePlayer.ToString();
        scoreValueTextEnemy.text = scoreEnemy.ToString();

        player.transform.position = playerStartPosition;
        enemy.transform.position = enemyStartPosition;
        ball.transform.position = ball.startPosition;

        yield return new WaitForSeconds(0.5f);
        player.spriteRenderer.enabled = true;
        enemy.spriteRenderer.enabled = true;
        
        yield return new WaitForSeconds(0.5f);
        ball.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        ball.StartBall();

        _coroutine = null;
        yield return null;
    }

    public void ScoreGoal(int goal)
    {
        if (goal == 0)
        {
            scorePlayer += 1;
            scoreValueTextPlayer.text = scorePlayer.ToString();
                
            if (scorePlayer < scoreWin)
                Invoke("SetBallEnemy", 1);            
        }
        else if (goal == 1)
        {
            scoreEnemy += 1;
            scoreValueTextEnemy.text = scoreEnemy.ToString();
                
            if (scoreEnemy < scoreWin)
                Invoke("SetBallPlayer", 1);
        }
        
        if (scorePlayer >= scoreWin || scoreEnemy >= scoreWin)
        {
            EndMatch();
        }
    }

    public void SetBallPlayer()
    {
        ball.transform.position = ball.startPosition;
        ball.gameObject.SetActive(true);

        float randX = UnityEngine.Random.Range(0.5f, 1.1f);
        float randY = UnityEngine.Random.Range(0.5f, 1.1f);

        /*float rand50X = UnityEngine.Random.Range(0,101);
        if (rand50X <= 50)
        {
            float newRandX = -randX;
            randX = newRandX;
        }*/

        float rand50Y = UnityEngine.Random.Range(0,101);
        if (rand50Y <= 50)
        {
            float newRandY = -randY;
            randY = newRandY;
        }

        Vector2 newRandomDirection = new Vector2(randX, randY);
        ball.startDirection = newRandomDirection;

        ball.rb.velocity = ball.startDirection * ball.throwForce;
    }

    public void SetBallEnemy()
    {
        ball.transform.position = ball.startPosition;
        ball.gameObject.SetActive(true);

        float randX = UnityEngine.Random.Range(-0.5f, -1.1f);
        float randY = UnityEngine.Random.Range(0.5f, 1.1f);

        /*float rand50X = UnityEngine.Random.Range(0,101);
        if (rand50X <= 50)
        {
            float newRandX = -randX;
            randX = newRandX;
        }*/

        float rand50Y = UnityEngine.Random.Range(0,101);
        if (rand50Y <= 50)
        {
            float newRandY = -randY;
            randY = newRandY;
        }

        Vector2 newRandomDirection = new Vector2(randX, randY);
        ball.startDirection = newRandomDirection;

        ball.rb.velocity = ball.startDirection * ball.throwForce;
    }
}
