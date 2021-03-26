using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //Types of Menus
    public enum Menu
    {
        Main,
        Select,
        History,
        Options,
        Credits,
        Jogo
    }

    public Menu currentMenu;

    //Camera Manager Reference
    private PersonalMenuCameraController cameraController;

    //Data Repository Reference
    private DataRepository repository;

    //TransitionScenes Script Reference
    private TransitionScenes transitionScene;

    //Scene Transition Reference
    private GameObject transitionCanvas;

    //Canvas's References
    private GameObject mainCanvas;
    private GameObject selectShipCanvas;
    private GameObject historyCanvas;
    private GameObject optionsCanvas;
    private GameObject creditsCanvas;

    //SpaceShip Displayed Reference
    private ShipDisplay shipDisplay;

    //Games Played Stats Variables Text Reference
    private Text gamesPlayedText;

    //Highscore Stats Variables Text Reference
    private Text highscoreText;

    //Coins Available Stats Variables Text Reference
    private Text coinsAvailableText;

    //Missiles Shot Stats Variables Text Reference
    private Text missilesShotText;

    //Enemies Killed Stats Variables Text Reference
    private Text enemiesKilledText;

    //Bosses Killed Stats Variables Text Reference
    private Text bossesKilledText;

    //Options Menu UI Music Options Slider Reference
    private Slider musicSlider;

    //Options Menu UI SFX Options Slider Reference
    private Slider sfxSlider;

    private void Start()
    {
        //Set Current Active Menu
        currentMenu = Menu.Main;

        //Get References
        cameraController = GameObject.Find("Cameras").GetComponent<PersonalMenuCameraController>();

        repository = GameObject.Find("DataRepository").GetComponent<DataRepository>();

        mainCanvas = GameObject.Find("CanvasMainMenu");
        selectShipCanvas = GameObject.Find("CanvasSelectShipMenu");
        historyCanvas = GameObject.Find("CanvasHistory");
        optionsCanvas = GameObject.Find("CanvasOptions");
        creditsCanvas = GameObject.Find("CanvasCredits");

        transitionCanvas = GameObject.Find("CanvasTransition");

        transitionScene = transitionCanvas.GetComponent<TransitionScenes>();

        shipDisplay = GameObject.Find("Customization").GetComponent<ShipDisplay>();

        gamesPlayedText = mainCanvas.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        highscoreText = mainCanvas.transform.GetChild(1).GetChild(1).GetComponent<Text>();
        coinsAvailableText = mainCanvas.transform.GetChild(1).GetChild(2).GetComponent<Text>();
        missilesShotText = mainCanvas.transform.GetChild(1).GetChild(3).GetComponent<Text>();
        enemiesKilledText = mainCanvas.transform.GetChild(1).GetChild(4).GetComponent<Text>();
        bossesKilledText = mainCanvas.transform.GetChild(1).GetChild(5).GetComponent<Text>();

        musicSlider = optionsCanvas.transform.GetChild(2).GetComponent<Slider>();
        sfxSlider = optionsCanvas.transform.GetChild(4).GetComponent<Slider>();

        //Set Values
        SetGameStats();

        musicSlider.value = repository.GetMusicVolume();
        sfxSlider.value = repository.GetSFXVolume();

        //Active Initial Menu and Deactivate Others
        mainCanvas.SetActive(false);
        selectShipCanvas.SetActive(false);
        historyCanvas.SetActive(false);
        optionsCanvas.SetActive(false);
        creditsCanvas.SetActive(false);

        //Set Skybox Tint to White
        RenderSettings.skybox.SetColor("_Tint", Color.white);

        //Start Scene Transition
        StartInitialSceneTransition();
    }

    private void SetGameStats()
    {
        gamesPlayedText.text = repository.GetAllStats()[0].ToString();
        highscoreText.text = repository.GetAllStats()[1].ToString();
        coinsAvailableText.text = repository.GetAllStats()[2].ToString();
        missilesShotText.text = repository.GetAllStats()[3].ToString();
        enemiesKilledText.text = repository.GetAllStats()[4].ToString();
        bossesKilledText.text = repository.GetAllStats()[5].ToString();
    }

    //Activate Specific Menu
    public void ShowMenu()
    {
        if (currentMenu == Menu.Main)
        {
            mainCanvas.SetActive(true);
        }
        else if (currentMenu == Menu.Select)
        {
            selectShipCanvas.SetActive(true);
        }
        else if (currentMenu == Menu.History)
        {
            historyCanvas.SetActive(true);
        }
        else if (currentMenu == Menu.Options)
        {
            optionsCanvas.SetActive(true);
        }
        else if (currentMenu == Menu.Credits)
        {
            creditsCanvas.SetActive(true);
        }
    }

    //Hide Menus except Active
    public void HideMenu()
    {
        if(currentMenu == Menu.Main)
        {
            selectShipCanvas.SetActive(false);
            historyCanvas.SetActive(false);
            optionsCanvas.SetActive(false);
            creditsCanvas.SetActive(false);
        }
        else if(currentMenu == Menu.Select)
        {
            mainCanvas.SetActive(false);
            historyCanvas.SetActive(false);
            optionsCanvas.SetActive(false);
            creditsCanvas.SetActive(false);
        }
        else if(currentMenu == Menu.History)
        {
            mainCanvas.SetActive(false);
            selectShipCanvas.SetActive(false);
            optionsCanvas.SetActive(false);
            creditsCanvas.SetActive(false);
        }
        else if(currentMenu == Menu.Options)
        {
            mainCanvas.SetActive(false);
            selectShipCanvas.SetActive(false);
            historyCanvas.SetActive(false);
            creditsCanvas.SetActive(false);
        }
        else if (currentMenu == Menu.Credits)
        {
            mainCanvas.SetActive(false);
            selectShipCanvas.SetActive(false);
            historyCanvas.SetActive(false);
            optionsCanvas.SetActive(false);
        }
    }

    //Hide All Menus
    public void HideAllMenus()
    {
        mainCanvas.SetActive(false);
        selectShipCanvas.SetActive(false);
        historyCanvas.SetActive(false);
        optionsCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
    }

    //Set Current Menu
    public void SetMainMenu()
    {
        currentMenu = Menu.Main;
    }

    public void SetSelectShipMenu()
    {
        currentMenu = Menu.Select;
    }

    public void SetHistoryMenu()
    {
        currentMenu = Menu.History;
    }

    public void SetOptionsMenu()
    {
        currentMenu = Menu.Options;
    }

    public void SetCreditsMenu()
    {
        currentMenu = Menu.Credits;
    }

    //Update Current Camera
    private void UpdateCamera()
    {
        cameraController.ChangeActiveCamera(currentMenu);
    }

    //Button Related Functions
    public void PlayGame()
    {
        SetMainMenu();
        HideAllMenus();
        shipDisplay.DefineRotationValues();
    }

    public void SelectShip()
    {
        SetSelectShipMenu();
        HideMenu();

        UpdateCamera();
    }

    public void History()
    {
        SetHistoryMenu();
        HideMenu();

        UpdateCamera();
    }

    public void Options()
    {
        SetOptionsMenu();
        HideMenu();

        UpdateCamera();
    }

    public void Credits()
    {
        SetCreditsMenu();

        ShowMenu();
        HideMenu();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BackToPersonalMenu()
    {
        SetMainMenu();
        HideMenu();

        UpdateCamera();
    }

    public void BackToOptionsMenu()
    {
        SetOptionsMenu();
        HideMenu();
        ShowMenu();
    }

    #region Initial Scene Transition 
    public void StartInitialSceneTransition()
    {
        transitionScene.SetTimeLerpStarted();
        transitionScene.DarkToWhiteTransition(this);
    }

    public void EndInitialSceneTransition()
    {
        transitionCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }
    #endregion

    #region Final Scene Transition 
    public void StartFinalSceneTransition()
    {
        transitionCanvas.SetActive(true);

        transitionScene.SetTimeLerpStarted();
        transitionScene.WhiteToDarkTransition(this);
    }

    public void EndFinalSceneTransition()
    {
        SceneManager.LoadScene("Personal Game");
    }
    #endregion
}
