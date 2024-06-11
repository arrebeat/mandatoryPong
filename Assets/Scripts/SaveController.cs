using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    public Material[] materialsPaddle;
    public Material materialPaddlePlayerRed { get; private set; }
    public Material materialPaddlePlayerGreen { get; private set; }
    public Material materialPaddlePlayerBlue { get; private set; }
    public Material materialPaddlePlayerYellow { get; private set; }
    public Material materialPaddleEnemyRed { get; private set; }
    public Material materialPaddleEnemyGreen { get; private set; }
    public Material materialPaddleEnemyBlue { get; private set; }
    public Material materialPaddleEnemyYellow { get; private set; }

    public GameObject playerPaddleObject { get; private set; }
    public GameObject enemyPaddleObject { get; private set; }
    
    public PlayerControllerPaddle playerPaddle { get; private set; }
    public EnemyControllerPaddle enemyPaddle { get; private set; }
    
    public Material materialPlayer;
    public Material materialEnemy;

    public string namePlayer;
    public string nameEnemy;

    private string _savedWinnerKey = "SavedWinner";

    public string GetName(bool isPlayer)
    {
        return isPlayer ? namePlayer : nameEnemy;
    }

    private static SaveController _instance;

    public static SaveController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<SaveController>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(SaveController).Name);
                    _instance = singletonObject.AddComponent<SaveController>();
                }
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        
        DontDestroyOnLoad(this.gameObject);

        playerPaddleObject = GameObject.Find("PaddlePlayer");
        playerPaddle = playerPaddleObject.GetComponent<PlayerControllerPaddle>();
        enemyPaddleObject = GameObject.Find("PaddleEnemy");
        enemyPaddle = enemyPaddleObject.GetComponent<EnemyControllerPaddle>();

        materialPaddlePlayerRed = materialsPaddle[0];
        materialPaddlePlayerGreen = materialsPaddle[1];
        materialPaddlePlayerBlue = materialsPaddle[2];
        materialPaddlePlayerYellow = materialsPaddle[3];

        materialPaddleEnemyRed = materialsPaddle[4];
        materialPaddleEnemyGreen = materialsPaddle[5];
        materialPaddleEnemyBlue = materialsPaddle[6];
        materialPaddleEnemyYellow = materialsPaddle[7];
    }

    public void SavePaddleMaterials()
    {
        materialPlayer = playerPaddleObject.GetComponent<SpriteRenderer>().material;
        materialEnemy = enemyPaddleObject.GetComponent<SpriteRenderer>().material;
    }    

    public void SaveWinner(string winner)
    {
        PlayerPrefs.SetString(_savedWinnerKey, winner);
    }

    public string GetLastWinner()
    {
        return PlayerPrefs.GetString(_savedWinnerKey);
    }

    public void Reset()
    {
        nameEnemy = "";
        namePlayer = "";
        materialPlayer = materialPaddlePlayerRed;
        materialEnemy = materialPaddleEnemyGreen;
    }

    public void ClearSave()
    {
        PlayerPrefs.DeleteAll();
    }
}
