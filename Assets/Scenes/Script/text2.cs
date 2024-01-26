using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class text2 : MonoBehaviour
{
    public GameManager _gameManager;
    private TextMeshProUGUI _textMeshProUGUI;
    void Awake()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        var manager = _gameManager.GetComponent<GameManager>();
        _textMeshProUGUI.text = _gameManager.enemyRemain.ToString("F1")+" remained";
    }
}
