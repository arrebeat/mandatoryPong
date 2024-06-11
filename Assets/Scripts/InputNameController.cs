using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InputNameController : MonoBehaviour
{
    public bool isPlayer;
    public TMP_InputField inputField;

    void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    void Start()
    {
        inputField.onValueChanged.AddListener(UpdateName);
    }

    public void UpdateName(string name)
    {
        if (isPlayer)
        {
            SaveController.Instance.namePlayer = name;
        }
        else
        {
            SaveController.Instance.nameEnemy = name;
        }
    }
}
