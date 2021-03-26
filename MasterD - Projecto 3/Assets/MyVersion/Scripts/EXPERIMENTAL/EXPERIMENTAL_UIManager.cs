using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPERIMENTAL_UIManager : MonoBehaviour
{
    private GameObject uiCanvas;

    private Text textId;
    private Text textHighscore;
    private Text textCoins;
    private Text textEnemies;
    private Text textMissiles;

    private void Awake()
    {
        uiCanvas = GameObject.Find("Canvas");
        textId = uiCanvas.transform.GetChild(0).GetComponent<Text>();
        textHighscore = uiCanvas.transform.GetChild(1).GetComponent<Text>();
        textCoins = uiCanvas.transform.GetChild(2).GetComponent<Text>();
        textEnemies = uiCanvas.transform.GetChild(3).GetComponent<Text>();
        textMissiles = uiCanvas.transform.GetChild(4).GetComponent<Text>();
    }

    public void UpdateUI(int[] stats)
    {
        textId.text = "ID: " + stats[0].ToString();
        textHighscore.text = "HIGHSCORE: " + stats[1].ToString();
        textCoins.text = "COINS: " + stats[2].ToString();
        textEnemies.text = "ENEMIES: " + stats[3].ToString();
        textMissiles.text = "MISSILES: " + stats[4].ToString();
    }
}
