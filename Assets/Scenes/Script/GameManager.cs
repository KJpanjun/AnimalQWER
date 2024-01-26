using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject foe1;
    public GameObject foe2;
    public GameObject foe3;
    public GameObject foe4;
    public GameObject foe5;
    public int enemyRemain = 5;
    private void Update()
    {
        enemyRemain = 0;
        if (foe1.activeSelf) enemyRemain++;
        if (foe2.activeSelf) enemyRemain++;
        if (foe3.activeSelf) enemyRemain++;
        if (foe4.activeSelf) enemyRemain++;
        if (foe5.activeSelf) enemyRemain++;
    }
}
