using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SomeText : MonoBehaviour
{
    private TextMeshProUGUI _textMeshProUGUI;
    void Awake()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        _textMeshProUGUI.text = "";
    }
}
