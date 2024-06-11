using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuReseter : MonoBehaviour
{
    public TextMeshProUGUI lastWinnerText;

    void Awake()
    {

    }

    void Start()
    {
        SaveController.Instance.Reset();   

        string lastWinner = SaveController.Instance.GetLastWinner();

        if (lastWinner != "")
        {
            lastWinnerText.text = "Last Winner: " + lastWinner;
        }
        else
        {
            lastWinnerText.text = "";
        }
    }
}
