using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SomeText : MonoBehaviour
{
    public GameObject _player;
    private TextMeshProUGUI _textMeshProUGUI;
    void Awake()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        var player = _player.GetComponent<Player>();
        _textMeshProUGUI.text = player.qSkillCoolTime.ToString("F1");
    }
}
