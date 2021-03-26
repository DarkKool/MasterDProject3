using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml.Linq;

public class DataRepository : MonoBehaviour
{
    //Create Singleton Instance
    private static DataRepository instance;


    //Game Data Holding String
    private string repository;

    //XML Document Holding Game Data
    private XDocument xmlData;


    //Number of times game was played
    private int gameID;

    //Most points made in a single game session
    private int highscore;

    //Number of coins holding at the moment
    private int coins;

    //Number of missiles shot throughout the entire game
    private int missiles;

    //Number of enemies killed throughout the entire game
    private int enemies;

    //Number of bosses killed throughout the entire game
    private int bosses;


    //Spaceship Selected Body Part
    private string bodySelected;

    //Spaceship Selected Boost Part
    private string boostSelected;

    //Spaceship Selected Weapon Part
    private string weaponSelected;


    //Spaceship Purchased Body Part
    private List<string> bodyPurchased = new List<string>();

    //Spaceship Purchased Boost Part
    private List<string> boostPurchased = new List<string>();

    //Spaceship Purchased Weapon Part
    private List<string> weaponPurchased = new List<string>();


    //Spaceship Available Body Part
    private List<string> bodyAvailable = new List<string>();

    //Spaceship Available Boost Part
    private List<string> boostAvailable = new List<string>();

    //Spaceship Purchased Weapon Part
    private List<string> weaponAvailable = new List<string>();


    //Music Volume
    private float musicVolume;

    //SFX Volume
    private float sfxVolume;


    private void Awake()
    {
        LoadGameDataFile();
        LoadGameStats();

        #region Singleton Instantiation
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion
    }

    private void CreateGameDataFile()
    {
        #region Document Creation
        //Create Game Data XML Document
        XDocument xmlDocument = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
        #endregion

        #region Root Element
        //Add a Root Element "DATA"
        xmlDocument.Add(new XElement("DATA"));
        #endregion

        #region Progress Element
        //Create a "PROGRESS" Element
        XElement progressElement = new XElement("PROGRESS");

        //Add Stats Elements to "DATA" Element
        progressElement.Add(new XElement("GAME_ID", 0),
                            new XElement("HIGHSCORE", 0),
                            new XElement("COINS", 0),
                            new XElement("MISSILES", 0),
                            new XElement("ENEMIES", 0),
                            new XElement("BOSSES", 0));
        
        //Add "PROGRESS" Element to Root Element "DATA"
        xmlDocument.Root.Add(progressElement);
        #endregion

        #region Spaceship Element
        //Create a "SPACESHIP" Element
        XElement spaceshipElement = new XElement("SPACESHIP");

        #region Active Spaceship Element
        //Create an "ACTIVE_SPACESHIP" Element
        XElement activeSpaceshipElement = new XElement("ACTIVE_SPACESHIP");

        //Add "SELECTED" Elements to "ACTIVE_SPACESHIP" Element
        activeSpaceshipElement.Add(new XElement("SELECTED_BODY", "A"),
                                   new XElement("SELECTED_BOOST", "A"),
                                   new XElement("SELECTED_WEAPON", "A"));

        //Add "ACTIVE_SPACESHIP" Element to "SPACESHIP" Element
        spaceshipElement.Add(activeSpaceshipElement);
        #endregion

        #region Purchased Parts Element
        //Create a "PURCHASED_PARTS" Element
        XElement purchasedPartsElement = new XElement("PURCHASED_PARTS");

        //Create Purchased Parts Elements and Add Purchased ones.
        XElement bodyPurchasedElement = new XElement("PURCHASED_BODY");
        bodyPurchasedElement.Add(new XElement("PURCHASED_BODY_PART", "A"));

        XElement boostPurchasedElement = new XElement("PURCHASED_BOOST");
        boostPurchasedElement.Add(new XElement("PURCHASED_BOOST_PART", "A"));

        XElement weaponPurchasedElement = new XElement("PURCHASED_WEAPON");
        weaponPurchasedElement.Add(new XElement("PURCHASED_WEAPON_PART", "A"));

        //Add Purchased Parts Elements to "PURCHASED_PARTS" Element
        purchasedPartsElement.Add(bodyPurchasedElement,
                                  boostPurchasedElement,
                                  weaponPurchasedElement);

        //Add "PURCHASED_PARTS" Element to "SPACESHIP" Element
        spaceshipElement.Add(purchasedPartsElement);
        #endregion

        #region Available Parts Element
        //Create an "AVAILABLE_PARTS" Element
        XElement availablePartsElement = new XElement("AVAILABLE_PARTS");

        //Create Available Parts Elements and Add Available ones.
        XElement bodyAvailableElement = new XElement("AVAILABLE_BODY");
        bodyAvailableElement.Add(new XElement("AVAILABLE_BODY_PART", "B"),
                                 new XElement("AVAILABLE_BODY_PART", "C"),
                                 new XElement("AVAILABLE_BODY_PART", "D"),
                                 new XElement("AVAILABLE_BODY_PART", "E"));

        XElement boostAvailableElement = new XElement("AVAILABLE_BOOST");
        boostAvailableElement.Add(new XElement("AVAILABLE_BOOST_PART", "B"),
                                  new XElement("AVAILABLE_BOOST_PART", "C"),
                                  new XElement("AVAILABLE_BOOST_PART", "D"),
                                  new XElement("AVAILABLE_BOOST_PART", "E"));

        XElement weaponAvailableElement = new XElement("AVAILABLE_WEAPON");
        weaponAvailableElement.Add(new XElement("AVAILABLE_WEAPON_PART", "B"),
                                   new XElement("AVAILABLE_WEAPON_PART", "C"),
                                   new XElement("AVAILABLE_WEAPON_PART", "D"),
                                   new XElement("AVAILABLE_WEAPON_PART", "E"));

        //Add Available Parts Elements to "AVAILABLE_PARTS" Element
        availablePartsElement.Add(bodyAvailableElement,
                                  boostAvailableElement,
                                  weaponAvailableElement);

        //Add "AVAILABLE_PARTS" Element to "SPACESHIP" Element
        spaceshipElement.Add(availablePartsElement);
        #endregion

        //Add "SPACESHIP" Element to Root Element "DATA"
        xmlDocument.Root.Add(spaceshipElement);

        #endregion

        #region Game History Element
        //Add "GAME_HISTORY" Element to Root Element "DATA"
        xmlDocument.Root.Add(new XElement("GAME_HISTORY_LIST"));
        #endregion

        #region Settings Element
        //Create a "SETTINGS" Element
        XElement settingsElement = new XElement("SETTINGS");

        //Add "VOLUMES" Elements to "SETTINGS" Element
        settingsElement.Add(new XElement("MUSIC_VOLUME", "0,5"),
                            new XElement("SFX_VOLUME", "0,5"));

        //Add "SETTINGS" Element to Root Element "DATA"
        xmlDocument.Root.Add(settingsElement);
        #endregion

        //Save XML Document
        xmlDocument.Save(Application.persistentDataPath + "/GameData.xml");
    }

    private void LoadGameDataFile()
    {
        if (!System.IO.File.Exists(Application.persistentDataPath + "/GameData.xml"))
        {
            CreateGameDataFile();
        }

        //Load Game Data Document
        repository = System.IO.File.ReadAllText(Application.persistentDataPath + "/GameData.xml");

        //Open Game Data XML Document
        xmlData = XDocument.Parse(repository);
    }

    public void LoadGameStats()
    {
        //Progress Data
        XElement elementProgress = xmlData.Root.Element("PROGRESS");

        //Set Properties
        gameID = int.Parse(elementProgress.Element("GAME_ID").Value);
        highscore = int.Parse(elementProgress.Element("HIGHSCORE").Value);
        coins = int.Parse(elementProgress.Element("COINS").Value);
        enemies = int.Parse(elementProgress.Element("ENEMIES").Value);
        missiles = int.Parse(elementProgress.Element("MISSILES").Value);

        //Spaceship Data
        XElement elementSpaceship = xmlData.Root.Element("SPACESHIP");

        //Spaceship Selected Data
        XElement elementActiveSpaceship = elementSpaceship.Element("ACTIVE_SPACESHIP");

        //Set Spaceship Selected Parts
        bodySelected = elementActiveSpaceship.Element("SELECTED_BODY").Value;
        boostSelected = elementActiveSpaceship.Element("SELECTED_BOOST").Value;
        weaponSelected = elementActiveSpaceship.Element("SELECTED_WEAPON").Value;

        //Spaceship Purchased Data
        XElement elementPurchasedSpaceship = elementSpaceship.Element("PURCHASED_PARTS");

        //Set Spaceship Purchased Body Parts
        foreach (XElement bodyPart in elementPurchasedSpaceship.Element("PURCHASED_BODY").Elements("PURCHASED_BODY_PART")) {
            
            bodyPurchased.Add(bodyPart.Value);
        }

        //Set Spaceship Purchased Boost Parts
        foreach (XElement boostPart in elementPurchasedSpaceship.Element("PURCHASED_BOOST").Elements("PURCHASED_BOOST_PART"))
        {
            boostPurchased.Add(boostPart.Value);
        }

        //Set Spaceship Purchased Weapon Parts
        foreach (XElement weaponPart in elementPurchasedSpaceship.Element("PURCHASED_WEAPON").Elements("PURCHASED_WEAPON_PART"))
        {
            weaponPurchased.Add(weaponPart.Value);
        }

        //Spaceship Available Data
        XElement elementAvailableSpaceship = elementSpaceship.Element("AVAILABLE_PARTS");

        //Set Spaceship Available Body Parts
        foreach (XElement bodyPart in elementAvailableSpaceship.Element("AVAILABLE_BODY").Elements("AVAILABLE_BODY_PART"))
        {
            bodyAvailable.Add(bodyPart.Value);
        }

        //Set Spaceship Available Boost Parts
        foreach (XElement boostPart in elementAvailableSpaceship.Element("AVAILABLE_BOOST").Elements("AVAILABLE_BOOST_PART"))
        {
            boostAvailable.Add(boostPart.Value);
        }

        //Set Spaceship Available Body Parts
        foreach (XElement weaponPart in elementAvailableSpaceship.Element("AVAILABLE_WEAPON").Elements("AVAILABLE_WEAPON_PART"))
        {
            weaponAvailable.Add(weaponPart.Value);
        }

        //Settings Data
        XElement settings = xmlData.Root.Element("SETTINGS");

        //Set Music Volume
        musicVolume = float.Parse(settings.Element("MUSIC_VOLUME").Value);

        //Set SFX Volume
        sfxVolume = float.Parse(settings.Element("SFX_VOLUME").Value);

    }

    public void UpdateGameStats(int points, int coins, int missiles, int enemies, int bosses)
    {
        //Update Variables
        this.gameID++;

        if(points > highscore)
        {
            highscore = points;
        }

        this.coins += coins;
        this.missiles += missiles;
        this.enemies += enemies;
        this.bosses += bosses;

        //Get "PROGRESS" Element
        XElement progress = xmlData.Root.Element("PROGRESS");

        //Overwrite Stats
        progress.Element("GAME_ID").Value = this.gameID.ToString();
        progress.Element("HIGHSCORE").Value = highscore.ToString();
        progress.Element("COINS").Value = this.coins.ToString();
        progress.Element("MISSILES").Value = this.missiles.ToString();
        progress.Element("ENEMIES").Value = this.enemies.ToString();
        progress.Element("BOSSES").Value = this.bosses.ToString();

        SaveGameData();
    }

    public void CreateGameSessionHistory(int points, int coins, int missiles, int enemies, int bosses)
    {
        //Get GAME_HISTORY_ELEMENT
        XElement gameHistoryList = xmlData.Root.Element("GAME_HISTORY_LIST");

        if (gameHistoryList.Elements("GAME_HISTORY").Count() >= 5)
        {
            //Get Last GAME_HISTORY Element and Delete it
            gameHistoryList.Elements("GAME_HISTORY").Last().Remove();
        }

        //Create Element GAME_HISTORY Standard Organization with GameID Attribute
        XElement gameHistory = new XElement("GAME_HISTORY");

        //Create Game History Stats Elements and Add to GAME_HISTORY Element
        gameHistory.Add(new XElement("GAME_ID", gameID.ToString()),
                        new XElement("POINTS", points.ToString()),
                        new XElement("COINS", coins.ToString()),
                        new XElement("MISSILES", missiles.ToString()),
                        new XElement("ENEMIES", enemies.ToString()),
                        new XElement("BOSSES", bosses.ToString()));

        //Add GAME_HISTORY to GAME_HISTORY_LIST first place
        gameHistoryList.AddFirst(gameHistory);

        UpdateGameStats(points, coins, missiles, enemies, bosses);

        SaveGameData();
    }

    private void SaveGameData()
    {
        xmlData.Save(Application.persistentDataPath + "/GameData.xml");
    }

    #region Get's & Set's
    public int[] GetAllStats()
    {
        return new int[] { gameID, highscore, coins, missiles, enemies, bosses};
    }

    #region GameID
    public int GetGameID()
    {
        return gameID;
    }

    public void SetGameID(int gameID)
    {
        this.gameID = gameID;

        UpdateGameID();
    }

    private void UpdateGameID()
    {
        xmlData.Root.Element("PROGRESS").Element("GAME_ID").Value = gameID.ToString();

        SaveGameData();
    }
    #endregion

    #region Highscore
    public int GetHighscore()
    {
        return highscore;
    }

    public void SetHighscore(int highscore)
    {
        this.highscore = highscore;

        UpdateHighscore();
    }

    private void UpdateHighscore()
    {
        xmlData.Root.Element("PROGRESS").Element("HIGHSCORE").Value = highscore.ToString();

        SaveGameData();
    }
    #endregion

    #region Coins
    public int GetCoins()
    {
        return coins;
    }

    public void SetCoins(int coins)
    {
        this.coins = coins;

        UpdateCoins();
    }

    private void UpdateCoins()
    {
        xmlData.Root.Element("PROGRESS").Element("COINS").Value = coins.ToString();

        SaveGameData();
    }
    #endregion

    #region Missiles
    public int GetMissiles()
    {
        return missiles;
    }

    public void SetMissiles(int missiles)
    {
        this.missiles = missiles;

        UpdateMissiles();
    }

    private void UpdateMissiles()
    {
        xmlData.Root.Element("PROGRESS").Element("MISSILES").Value = missiles.ToString();

        SaveGameData();
    }
    #endregion

    #region Enemies
    public int GetEnemies()
    {
        return enemies;
    }

    public void SetEnemies(int enemies)
    {
        this.enemies = enemies;

        UpdateEnemies();
    }

    private void UpdateEnemies()
    {
        xmlData.Root.Element("PROGRESS").Element("ENEMIES").Value = enemies.ToString();

        SaveGameData();
    }
    #endregion

    #region Bosses
    public int GetBosses()
    {
        return bosses;
    }

    public void SetBosses(int bosses)
    {
        this.bosses = bosses;

        UpdateBosses();
    }

    private void UpdateBosses()
    {
        xmlData.Root.Element("PROGRESS").Element("BOSSES").Value = bosses.ToString();

        SaveGameData();
    }
    #endregion

    #region Selected Parts

    #region Selected Body Part
    public string GetSelectedBodyPart()
    {
        return bodySelected;
    }

    public void SetSelectedBodyPart(string body)
    {
        bodySelected = body;

        UpdateSelectedBodyPart();
    }

    private void UpdateSelectedBodyPart()
    {
        xmlData.Root.Element("SPACESHIP").Element("ACTIVE_SPACESHIP").Element("SELECTED_BODY").Value = bodySelected;

        SaveGameData();
    }
    #endregion

    #region Selected Boost Part
    public string GetSelectedBoostPart()
    {
        return boostSelected;
    }

    public void SetSelectedBoostPart(string boost)
    {
        boostSelected = boost;

        UpdateSelectedBoostPart();
    }

    private void UpdateSelectedBoostPart()
    {
        xmlData.Root.Element("SPACESHIP").Element("ACTIVE_SPACESHIP").Element("SELECTED_BOOST").Value = boostSelected;

        SaveGameData();
    }
    #endregion

    #region Selected Weapon Part
    public string GetSelectedWeaponPart()
    {
        return weaponSelected;
    }

    public void SetSelectedWeaponPart(string weapon)
    {
        weaponSelected = weapon;

        UpdateSelectedWeaponPart();
    }

    private void UpdateSelectedWeaponPart()
    {
        xmlData.Root.Element("SPACESHIP").Element("ACTIVE_SPACESHIP").Element("SELECTED_WEAPON").Value = weaponSelected;

        SaveGameData();
    }
    #endregion

    #endregion

    #region Purchased Parts

    #region Purchased Body Parts
    public List<string> GetPurchasedBodyParts()
    {
        return bodyPurchased;
    }

    public void AddPurchasedBodyPart(string bodyPart)
    {
        bodyPurchased.Add(bodyPart);

        xmlData.Root.Element("SPACESHIP").Element("PURCHASED_PARTS").Element("PURCHASED_BODY").Add(new XElement("PURCHASED_BODY_PART", bodyPart));

        SaveGameData();
    }
    #endregion

    #region Purchased Boost Parts
    public List<string> GetPurchasedBoostParts()
    {
        return boostPurchased;
    }

    public void AddPurchasedBoostPart(string boostPart)
    {
        boostPurchased.Add(boostPart);

        xmlData.Root.Element("SPACESHIP").Element("PURCHASED_PARTS").Element("PURCHASED_BOOST").Add(new XElement("PURCHASED_BOOST_PART", boostPart));

        SaveGameData();
    }
    #endregion

    #region Purchased Weapon Parts
    public List<string> GetPurchasedWeaponParts()
    {
        return weaponPurchased;
    }

    public void AddPurchasedWeaponPart(string weaponPart)
    {
        boostPurchased.Add(weaponPart);

        xmlData.Root.Element("SPACESHIP").Element("PURCHASED_PARTS").Element("PURCHASED_WEAPON").Add(new XElement("PURCHASED_WEAPON_PART", weaponPart));

        SaveGameData();
    }
    #endregion

    #endregion

    #region Available Parts

    #region Available Body Parts
    public List<string> GetAvailableBodyParts()
    {
        return bodyAvailable;
    }

    public void RemoveAvailableBodyPart(string bodyPart)
    {
        bodyAvailable.Remove(bodyPart);

        XElement elementBodyParts = xmlData.Root.Element("SPACESHIP").Element("AVAILABLE_PARTS").Element("AVAILABLE_BODY");

        foreach(XElement element in elementBodyParts.Elements("AVAILABLE_BODY_PART"))
        {
            if(element.Value == bodyPart)
            {
                element.Remove();
            }
        }

        SaveGameData();
    }
    #endregion

    #region Available Boost Parts
    public List<string> GetAvailableBoostParts()
    {
        return boostAvailable;
    }

    public void RemoveAvailableBoostPart(string boostPart)
    {
        boostAvailable.Remove(boostPart);

        XElement elementBoostParts = xmlData.Root.Element("SPACESHIP").Element("AVAILABLE_PARTS").Element("AVAILABLE_BOOST");

        foreach (XElement element in elementBoostParts.Elements("AVAILABLE_BOOST_PART"))
        {
            if (element.Value == boostPart)
            {
                element.Remove();
            }
        }

        SaveGameData();
    }
    #endregion

    #region Available Weapon Parts
    public List<string> GetAvailableWeaponParts()
    {
        return weaponAvailable;
    }

    public void RemoveAvailableWeaponPart(string weaponPart)
    {
        weaponAvailable.Remove(weaponPart);

        XElement elementWeaponParts = xmlData.Root.Element("SPACESHIP").Element("AVAILABLE_PARTS").Element("AVAILABLE_WEAPON");

        foreach (XElement element in elementWeaponParts.Elements("AVAILABLE_WEAPON_PART"))
        {
            if (element.Value == weaponPart)
            {
                element.Remove();
            }
        }

        SaveGameData();
    }
    #endregion

    #endregion

    #region Game History
    public int GetNumberOfGameSessions()
    {
        return xmlData.Root.Element("GAME_HISTORY_LIST").Elements("GAME_HISTORY").Count();
    }

    public List<string> GetGameSession(int gameSession)
    {
        List<string> info = new List<string>();
        XElement gameSessionElement = xmlData.Root.Element("GAME_HISTORY_LIST").Elements("GAME_HISTORY").ElementAt(gameSession);

        foreach(XElement element in gameSessionElement.Elements())
        {
            info.Add(element.Value);
        }

        return info;
    }
    #endregion

    #region Settings

    #region Music Volume
    public float GetMusicVolume()
    {
        return musicVolume;
    }

    public void SetMusicVolume(float musicVolume)
    {
        this.musicVolume = musicVolume;

        UpdateMusicVolume();
    }

    private void UpdateMusicVolume()
    {
        xmlData.Root.Element("SETTINGS").Element("MUSIC_VOLUME").Value = musicVolume.ToString();

        SaveGameData();
    }
    #endregion

    #region
    public float GetSFXVolume()
    {
        return sfxVolume;
    }

    public void SetSFXVolume(float sfxVolume)
    {
        this.sfxVolume = sfxVolume;

        UpdateSFXVolume();
    }

    private void UpdateSFXVolume()
    {
        xmlData.Root.Element("SETTINGS").Element("SFX_VOLUME").Value = sfxVolume.ToString();

        SaveGameData();
    }
    #endregion

    #endregion

    #endregion
}
