using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonalUIController : MonoBehaviour
{
    //Audio Controller Reference
    private DataRepository repository;

    //Game Controller Reference
    private PersonalGameController gameController;

    //Transition Scenes Reference
    private TransitionScenes transitionScene;

    //Base UI Canvas Reference
    private GameObject uiBase;

    //Animation UI Canvas Reference
    private GameObject uiAnimation;

    //Pause Menu UI Canvas Reference
    private GameObject uiPauseMenu;

    //Pause Menu UI MenuOptions Reference
    private GameObject uiPauseMenuOptions;

    //Pause Menu UI MenuOptions Music Volume Reference
    private GameObject uiPauseMenuOptionsMusicVolume;

    //Pause Menu UI MenuOptions SFX Volume Reference
    private GameObject uiPauseMenuOptionsSFXVolume;

    //Transition Scene UI Canvas Reference
    private GameObject uiTransition;

    //End Game UI Canvas Reference
    private GameObject uiEndGame;

    //Base UI Wave Reference
    private Text waveText;

    //Base UI Enemies Reference
    private Text enemiesText;

    //Base UI Points Reference
    private Text pointsText;

    //Base UI Coins Reference
    private Text coinsText;

    //Animation UI WaveInformation Text
    private GameObject waveInformationText;

    //Animation UI BossIntroduction Text
    private GameObject bossIntroductionText;

    //Animation UI ThreatClearInformation Text
    private GameObject threatClearInformationText;

    //Pause Menu UI Wave Reference
    private Text waveTextPause;

    //Pause Menu UI Enemies Reference
    private Text enemiesTextPause;

    //Pause Menu UI Points Reference
    private Text pointsTextPause;

    //Pause Menu UI Coins Reference
    private Text coinsTextPause;

    //Options Menu UI Music Options Slider Reference
    private Slider musicSlider;

    //Options Menu UI SFX Options Slider Reference
    private Slider sfxSlider;

    //End Game UI Wave Reference
    private Text waveTextEndGame;

    //End Game UI Points Reference
    private Text pointsTextEndGame;

    //End Game UI Coins Reference
    private Text coinsTextEndGame;

    //End Game UI Shots Fired Reference
    private Text shotsFiredTextEndGame;

    //End Game UI Enemies Killed Reference
    private Text enemiesKilledTextEndGame;

    //End Game UI Bosses Killed Reference
    private Text bossesKilledTextEndGame;

    // Start is called before the first frame update
    void Start()
    {
        //Get References
        gameController = GameObject.Find("GameController").GetComponent<PersonalGameController>();
        repository = GameObject.Find("DataRepository").GetComponent<DataRepository>();

        uiBase = GameObject.Find("UIBase");
        uiAnimation = GameObject.Find("UIAnimations");
        uiPauseMenu = GameObject.Find("UIPauseMenu");
        uiPauseMenuOptions = GameObject.Find("UIPauseMenuOptions");
        uiPauseMenuOptionsMusicVolume = GameObject.Find("UIPauseMenuOptionsMusicVolume");
        uiPauseMenuOptionsSFXVolume = GameObject.Find("UIPauseMenuOptionsSFXVolume");
        uiTransition = GameObject.Find("UISceneTransition");
        uiEndGame = GameObject.Find("UIEndGame");

        waveText = uiBase.transform.GetChild(0).GetChild(1).GetComponent<Text>();
        enemiesText = uiBase.transform.GetChild(0).GetChild(3).GetComponent<Text>();
        pointsText = uiBase.transform.GetChild(0).GetChild(5).GetComponent<Text>();
        coinsText = uiBase.transform.GetChild(0).GetChild(7).GetComponent<Text>();

        musicSlider = uiPauseMenuOptionsMusicVolume.transform.GetChild(2).GetChild(1).GetComponent<Slider>();
        sfxSlider = uiPauseMenuOptionsSFXVolume.transform.GetChild(2).GetChild(1).GetComponent<Slider>();

        waveInformationText = uiAnimation.transform.GetChild(0).gameObject;
        bossIntroductionText = uiAnimation.transform.GetChild(1).gameObject;
        threatClearInformationText = uiAnimation.transform.GetChild(2).gameObject;

        waveTextPause = uiPauseMenu.transform.GetChild(1).GetChild(1).GetComponent<Text>();
        enemiesTextPause = uiPauseMenu.transform.GetChild(1).GetChild(3).GetComponent<Text>();
        pointsTextPause = uiPauseMenu.transform.GetChild(1).GetChild(5).GetComponent<Text>();
        coinsTextPause = uiPauseMenu.transform.GetChild(1).GetChild(7).GetComponent<Text>();

        waveTextEndGame = uiEndGame.transform.GetChild(1).GetChild(2).GetChild(1).GetComponent<Text>();
        pointsTextEndGame = uiEndGame.transform.GetChild(1).GetChild(2).GetChild(3).GetComponent<Text>();
        coinsTextEndGame = uiEndGame.transform.GetChild(1).GetChild(2).GetChild(5).GetComponent<Text>();
        shotsFiredTextEndGame = uiEndGame.transform.GetChild(1).GetChild(2).GetChild(7).GetComponent<Text>();
        enemiesKilledTextEndGame = uiEndGame.transform.GetChild(1).GetChild(2).GetChild(9).GetComponent<Text>();
        bossesKilledTextEndGame = uiEndGame.transform.GetChild(1).GetChild(2).GetChild(11).GetComponent<Text>();

        transitionScene = uiTransition.GetComponent<TransitionScenes>();

        //Set Slider's Position
        musicSlider.value = repository.GetMusicVolume();
        sfxSlider.value = repository.GetSFXVolume();

        //Deactivate Menus
        DeactivateBaseMenu();
        DeactivatePauseMenu();
        DeactivateOptionsMenu();
        DeactivateMusicVolumeMenu();
        DeactivateSFXVolumeMenu();
        DeactivateEndGameMenu();

        //Deactivate UIAnimation Text's
        waveInformationText.SetActive(false);
        bossIntroductionText.SetActive(false);
        threatClearInformationText.SetActive(false);

        //Start Scene Transition
        StartInitialSceneTransition();
    }

    //Update Wave Round Information
    public void UpdateWaveRoundInformation(int waveRound, int numberOfEnemies, int points, int coins)
    {
        waveText.text = waveRound.ToString();
        enemiesText.text = numberOfEnemies.ToString();
        pointsText.text = points.ToString();
        coinsText.text = coins.ToString();
    }

    //Begin Wave Round Introduction Animation
    public void WaveStartAnimation(int waveRound)
    {
        waveInformationText.SetActive(true);
        waveInformationText.GetComponent<Text>().text = "// Wave " + waveRound + " //";
    }

    //End Wave Round Introduction Animation
    public void WaveInformationEndAnimation()
    {
        waveInformationText.SetActive(false);
        gameController.WaveInformationEnd();
    }

    //Begin Boss Start Animation
    public void BossStartAnimation()
    {
        bossIntroductionText.SetActive(true);
    }

    //End Boss Start Animation
    public void BossIntroductionEndAnimation()
    {
        bossIntroductionText.SetActive(false);
        gameController.BossIntroductionEnd();
    }

    //Begin Boss Death Animation
    public void BossClearStartAnimation()
    {
        threatClearInformationText.SetActive(true);
    }

    //End Boss Death Animation
    public void BossClearEndAnimation()
    {
        threatClearInformationText.SetActive(false);
        gameController.BossClearEnd();
    }

    //End Game
    public void EndGame()
    {
        DeactivateAllMenus();

        ActivateEndGameMenu();
    }

    #region Base Menu
    private void ActivateBaseMenu()
    {
        uiBase.SetActive(true);
    }

    private void DeactivateBaseMenu()
    {
        uiBase.SetActive(false);
    }
    #endregion

    #region Pause Menu
    public void PauseGame()
    {
        DeactivateBaseMenu();
        ActivatePauseMenu();
    }

    private void ActivatePauseMenu()
    {
        uiPauseMenu.SetActive(true);
    }

    private void DeactivatePauseMenu()
    {
        uiPauseMenu.SetActive(false);
    }

    //Update Pause Menu Wave Round Information
    public void UpdateWaveRoundInformationPause(int waveRound, int numberOfEnemies, int points, int coins)
    {
        waveTextPause.text = waveRound.ToString();
        enemiesTextPause.text = numberOfEnemies.ToString();
        pointsTextPause.text = points.ToString();
        coinsTextPause.text = coins.ToString();
    }
    #endregion

    #region Options Menu
    private void ActivateOptionsMenu()
    {
        uiPauseMenuOptions.SetActive(true);
    }

    private void DeactivateOptionsMenu()
    {
        uiPauseMenuOptions.SetActive(false);
    }
    #endregion

    #region Music Volume
    private void ActivateMusicVolumeMenu()
    {
        uiPauseMenuOptionsMusicVolume.SetActive(true);
    }

    private void DeactivateMusicVolumeMenu()
    {
        uiPauseMenuOptionsMusicVolume.SetActive(false);
    }
    #endregion

    #region SFX Volume
    private void ActivateSFXVolumeMenu()
    {
        uiPauseMenuOptionsSFXVolume.SetActive(true);
    }

    private void DeactivateSFXVolumeMenu()
    {
        uiPauseMenuOptionsSFXVolume.SetActive(false);
    }
    #endregion

    #region End Game
    private void ActivateEndGameMenu()
    {
        uiEndGame.SetActive(true);
    }

    private void DeactivateEndGameMenu()
    {
        uiEndGame.SetActive(false);
    }

    public void UpdateWaveRoundInformationEndGame(int waveRound, int points, int coins, int shotsFired, int numberOfEnemiesKilled, int numberOfBossesKilled)
    {
        waveTextEndGame.text = waveRound.ToString();
        pointsTextEndGame.text = points.ToString();
        coinsTextEndGame.text = coins.ToString();
        shotsFiredTextEndGame.text = shotsFired.ToString();
        enemiesKilledTextEndGame.text = numberOfEnemiesKilled.ToString();
        bossesKilledTextEndGame.text = numberOfBossesKilled.ToString();
    }
    #endregion

    //Deactivate All Menus except TransitionMenu
    public void DeactivateAllMenus()
    {
        DeactivateBaseMenu();
        uiAnimation.SetActive(false);
        DeactivatePauseMenu();
        DeactivateOptionsMenu();
        DeactivateMusicVolumeMenu();
        DeactivateSFXVolumeMenu();
        DeactivateEndGameMenu();
    }

    #region Buttons

    #region Pause Menu
    public void ResumeGameButton()
    {
        DeactivatePauseMenu();
        ActivateBaseMenu();
        gameController.ResumeGame();
    }

    public void RestartGameButton()
    {
        gameController.RestartGame();
    }

    public void OptionsButton()
    {
        ActivateOptionsMenu();
        DeactivatePauseMenu();
    }

    public void QuitButton()
    {
        DeactivateAllMenus();

        StartFinalSceneTransition();
    }
    #endregion

    #region Options Menu
    public void MusicVolumeButton()
    {
        ActivateMusicVolumeMenu();
        DeactivateOptionsMenu();
    }

    public void SFXVolumeButton()
    {
        ActivateSFXVolumeMenu();
        DeactivateOptionsMenu();
    }

    public void BackToPauseMenuFromMusicVolumeButton()
    {
        ActivateOptionsMenu();
        DeactivateMusicVolumeMenu();
    }

    public void BackToPauseMenuFromSFXVolumeButton()
    {
        ActivateOptionsMenu();
        DeactivateSFXVolumeMenu();
    }

    public void BackToPauseMenuFromOptionsMenu()
    {
        ActivatePauseMenu();
        DeactivateOptionsMenu();
    }
    #endregion

    #endregion

    #region Sliders
    public void MusicVolumeSlider()
    {

    }

    public void SFXVolumeSlider()
    {

    }
    #endregion

    #region Initial Scene Transition
    public void StartInitialSceneTransition()
    {
        transitionScene.SetTimeLerpStarted();
        transitionScene.DarkToWhiteTransition(this);
    }

    public void EndInitialSceneTransition()
    {
        uiTransition.SetActive(false);
        uiBase.SetActive(true);

        gameController.StartGame();
    }
    #endregion

    #region Final Scene Transition 
    public void StartFinalSceneTransition()
    {
        uiTransition.SetActive(true);

        transitionScene.SetTimeLerpStarted();
        transitionScene.WhiteToDarkTransition(this);
    }

    public void EndFinalSceneTransition()
    {
        gameController.QuitGame();
    }
    #endregion
}
