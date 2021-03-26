using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml.Linq;

public class EXPERIMENTAL_DataRepository
{
    //Stats Holding Document
    private string repository;

    //Document Holding XML Data
    private XDocument xmlData;

    //The number the times the game was played.
    private int gameId;

    //Most points made in a single game session
    private int highscore;

    //Number of coins holding at the moment
    private int coins;

    //Number of enemies killed throughout the entire game
    private int enemies;

    //Number of missiles shot throughout the entire game
    private int missiles;

    public EXPERIMENTAL_DataRepository()
    {
        //Load Game Data
        LoadGameData();

        //Get progress stats
        LoadStats();
    }

    //Gets Information from XML Document "GamePoints"
    private void LoadStats()
    {
        //Progress Data
        XElement elementProgress = xmlData.Root.Element("PROGRESS");

        //Set Properties
        gameId = int.Parse(elementProgress.Element("GAME_ID").Value);
        highscore = int.Parse(elementProgress.Element("HIGHSCORE").Value);
        coins = int.Parse(elementProgress.Element("COINS").Value);
        enemies = int.Parse(elementProgress.Element("ENEMIES").Value);
        missiles = int.Parse(elementProgress.Element("MISSILES").Value);
    }

    public void UpdateStats(int points, int coins, int enemies, int missiles)
    {
        //Update Variables
        gameId++;

        if(points > highscore)
        {
            highscore = points;
        }

        this.coins += coins;
        this.enemies += enemies;
        this.missiles += missiles;

        //Get PROGRESS Element
        XElement progress = xmlData.Root.Element("PROGRESS");

        //Overwrite Stats
        progress.Element("GAME_ID").Value = gameId.ToString();
        progress.Element("HIGHSCORE").Value = highscore.ToString();
        progress.Element("COINS").Value = this.coins.ToString();
        progress.Element("ENEMIES").Value = this.enemies.ToString();
        progress.Element("MISSILES").Value = this.missiles.ToString();

        SaveGameData();
    }

    public void CreateGameSessionHistory(int id, int points, int coins, int enemies, int missiles)
    {
        //Get GAME_HISTORY_ELEMENT
        XElement gameHistoryList = xmlData.Root.Element("GAME_HISTORY_LIST");

        if(gameHistoryList.Elements("GAME_HISTORY").Count() >= 5)
        {
            //Get Last GAME_HISTORY Element and Delete it
            gameHistoryList.Elements("GAME_HISTORY").Last().Remove();
        }

        //Create Element GAME_HISTORY Standard Organization with GameID Attribute
        XElement gameHistory = new XElement("GAME_HISTORY", new XAttribute("id", id.ToString()));

        //Create Game History Stats Elements and Add to GAME_HISTORY Element
        gameHistory.Add(new XElement("POINTS", points.ToString()), 
                        new XElement("COINS", coins.ToString()), 
                        new XElement("ENEMIES", enemies.ToString()),
                        new XElement("Missiles", missiles.ToString()));

        //Add GAME_HISTORY to GAME_HISTORY_LIST first place
        gameHistoryList.AddFirst(gameHistory);

        //Update XML Document
        SaveGameData();
    }

    public void SaveGameData()
    {
        xmlData.Save("Assets/Resources/GameData.xml");
    }

    private void LoadGameData()
    {
        //Load Game Data Document
        repository = System.IO.File.ReadAllText("Assets/Resources/GameData.xml");
        
        //Update XMLData Document
        xmlData = XDocument.Parse(repository);
    }

    public int[] GetStats()
    {
        return new int[] {gameId, highscore, coins, enemies, missiles};
    }

    public void Description()
    {
        Debug.Log("GameId: " + gameId + ": \n" +
                  "Highscore - " + highscore + "; \n" +
                  "Coins - " + coins + "; \n" +
                  "Enemies - " + enemies + "; \n" +
                  "Missiles - " + missiles);
    }
}
