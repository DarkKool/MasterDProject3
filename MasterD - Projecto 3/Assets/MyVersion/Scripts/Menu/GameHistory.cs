using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHistory : MonoBehaviour
{
    //Game ID Text Reference
    private static GameObject gameIDText;

    //Game ID
    private string gameID;

    //Points
    private string points;

    //Coins
    private string coins;

    //Missiles
    private string missiles;

    //Enemies
    private string enemies;

    //Bosses
    private string bosses;

    private void Awake()
    {
        gameIDText = transform.GetChild(1).gameObject;
    }

    public void SetInformation(List<string> info)
    {
        gameID = info[0];
        points = info[1];
        coins = info[2];
        missiles = info[3];
        enemies = info[4];
        bosses = info[5];

        gameIDText.GetComponent<Text>().text = gameID;
    }

    public string[] GetInformation()
    {
        return new string[] {gameID, points, coins, missiles, enemies, bosses};
    }
}
