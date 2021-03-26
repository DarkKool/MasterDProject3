using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersonalGameController : MonoBehaviour
{
    //Game States
    public enum GameState
    {
        Opening,
        Intro,
        IntroWave,
        View2D,
        BossIntroduction,
        View3D,
        BossEnd,
        GameEnd,
        Pause
    }

    //Current Game State
    private GameState currentGameState;

    //Previous Game State
    private GameState previousGameState;

    //Data Repository Reference
    private DataRepository repository;

    //Enemy Controller Reference
    private PersonalEnemyController enemyController;

    //UI Controller Reference
    private PersonalUIController uiController;

    //Audio Controller Reference
    private AudioController audioController;

    //Player Controller Reference
    private PersonalPlayerController playerController;

    private GameObject player;

    //Wave Round
    private int waveRound;

    //Number of Enemies
    private int numberOfEnemies;

    //Number of Points
    private int numberOfPoints;

    //Number of Coins
    private int numberOfCoins;

    //Auxiliar Coin Counting
    private int coinStackCounter;

    #region Extra Stats
    //Number of Shots Fired
    private int numberOfShotsFired;
    
    //Number of Killed Enemies
    private int numberOfEnemiesKilled;

    //Number of Killed Bosses
    private int numberOfBossesKilled;
    #endregion

    //Multiplier to decide how Many Enemies It will Exist
    [SerializeField] private float enemiesBaseValue;

    //Clamped value of Number of Enemies
    [SerializeField] private int minEnemiesNumber;
    [SerializeField] private int maxEnemiesNumber;

    //The Number of Enemies in the Last Round
    private int lastRoundNumberOfEnemies;

    //SkyBox Rotation Camera
    private Transform skyboxCamera;

    //Skybox Rotation Speed
    private float skyboxRotationSpeed;

    private bool pressed;
    private float fadeTimer;
    private float fadeDuration;
    public Cinemachine.CinemachineVirtualCamera camera2D;
    public Cinemachine.CinemachineVirtualCamera camera3D;

    // Start is called before the first frame update
    void Awake()
    {
        //Get References
        repository = GameObject.Find("DataRepository").GetComponent<DataRepository>();
        enemyController = GameObject.Find("EnemyController").GetComponent<PersonalEnemyController>();
        uiController = GameObject.Find("UIController").GetComponent<PersonalUIController>();
        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();

        //Instantiate Player
        player = Instantiate(Resources.Load<GameObject>("FinalModels/PlayerSpaceships/Prefabs/" + repository.GetSelectedBodyPart() + repository.GetSelectedBoostPart() + repository.GetSelectedWeaponPart()));
        player.name = "Player";

        //Get PlayerController Reference
        playerController = GameObject.Find("Player").GetComponent<PersonalPlayerController>();

        //Set Player Stats
        playerController.SetStats(repository.GetSelectedBodyPart(), repository.GetSelectedBoostPart(), repository.GetSelectedWeaponPart());

        skyboxCamera = GameObject.Find("SkyboxCamera").transform;

        //Set Current Game State
        currentGameState = GameState.Opening;

        //Change Skybox Tint
        RenderSettings.skybox.SetColor("_Tint", Color.white);

        //Skybox Rotation Speed
        skyboxRotationSpeed = playerController.GetFinalSpeed();

        //Set Wave Round
        waveRound = 0;

        //Set Enemies
        numberOfEnemies = 0;

        //Set Points
        numberOfPoints = 0;

        //Set Coins
        numberOfCoins = 0;

        //Set Auxiliar Coin Counting
        coinStackCounter = 0;

        //Deactivate Player
        playerController.gameObject.SetActive(false);

        //Set Audio Volumes

    }

    // Update is called once per frame
    void Update()
    {
        BackgroundSettings();

        if (Input.GetKeyDown(KeyCode.Escape) && currentGameState != GameState.Pause && currentGameState != GameState.GameEnd)
        {
            PauseGame();

            uiController.UpdateWaveRoundInformationPause(waveRound, numberOfEnemies, numberOfPoints, numberOfCoins);
        }

        //When the Boss appears, change Skybox color to Red
        if (currentGameState == GameState.BossIntroduction)
        {
            BackgroundChange(1f, 0f);
        }
        //When the Boss dies, change Skybox color to White
        else if (currentGameState == GameState.BossEnd)
        {
            BackgroundChange(0f, 1f);
        }
    }

    //Skybox Rotation
    private void BackgroundSettings()
    {
        if (currentGameState == GameState.View2D || currentGameState == GameState.IntroWave)
        {
            skyboxCamera.Rotate(new Vector3(Time.deltaTime * -skyboxRotationSpeed, 0f, 0f));
        }
    }

    //Change Skybox Color Transition
    private void BackgroundChange(float begin, float end)
    {
        float timeSinceStarted = Time.time - fadeTimer;
        float percentangeComplete = timeSinceStarted / fadeDuration;

        RenderSettings.skybox.SetColor("_Tint", new Color(1, Mathf.Lerp(begin, end, percentangeComplete), Mathf.Lerp(begin, end, percentangeComplete), 1));
    }

    //Change Camera's View
    private void ChangeView(int priority2D, int priority3D)
    {
        camera2D.Priority = priority2D;
        camera3D.Priority = priority3D;
    }

    //After Scene Transition End, Game Starts
    public void StartGame()
    {
        currentGameState = GameState.Intro;

        playerController.gameObject.SetActive(true);
    }

    //Beginning of a Wave
    public void WaveStart()
    {
        currentGameState = GameState.IntroWave;

        uiController.WaveStartAnimation(waveRound + 1);
    }

    //Increase WaveRound, Check if Normal Wave or Boss Wave. Get Number of Enemies
    private void NextWave()
    {
        waveRound++;

        if(waveRound == 1)
        {
            numberOfEnemies = minEnemiesNumber;
            lastRoundNumberOfEnemies = numberOfEnemies;

            enemyController.ChooseEnemies(numberOfEnemies, waveRound);
        }
        else if(waveRound % 5 != 0)
        {
            //numberOfEnemies = Mathf.FloorToInt(Mathf.Clamp(Mathf.Pow(enemiesBaseValue, waveRound), minEnemiesNumber, maxEnemiesNumber));
            if(waveRound < 15)
            {
                numberOfEnemies = Mathf.FloorToInt(Mathf.Clamp(lastRoundNumberOfEnemies + enemiesBaseValue * lastRoundNumberOfEnemies, minEnemiesNumber, maxEnemiesNumber));
                lastRoundNumberOfEnemies = numberOfEnemies;
            }
            else
            {
                numberOfEnemies = Mathf.FloorToInt(Mathf.Clamp((enemiesBaseValue * waveRound / maxEnemiesNumber) + lastRoundNumberOfEnemies + enemiesBaseValue * (lastRoundNumberOfEnemies / minEnemiesNumber), minEnemiesNumber, maxEnemiesNumber));
                lastRoundNumberOfEnemies = numberOfEnemies;
            }

            enemyController.ChooseEnemies(numberOfEnemies, waveRound);
        }
        else
        {
            numberOfEnemies = 1;
            
            currentGameState = GameState.BossIntroduction;

            fadeTimer = Time.time;
            
            ChangeView(1, 2);
            

            uiController.BossStartAnimation();

            playerController.SetCanMove(false);
            playerController.ResetPosition();

            enemyController.SpawnBoss(waveRound);

            audioController.MusicPause();
            audioController.PlayWarningSFX();
        }

        uiController.UpdateWaveRoundInformation(waveRound, numberOfEnemies, numberOfPoints, numberOfCoins);
    }

    //Return Player Reference
    public GameObject GetPlayer()
    {
        return player;
    }

    //Add Shots Fired
    public void AddShots()
    {
        numberOfShotsFired++;
    }

    //Add Game Session Points
    public void AddPoints(int points)
    {
        numberOfPoints += points;
        coinStackCounter += points;
        AddCoins(numberOfPoints);
    }

    //Add Coins
    private void AddCoins(int points)
    {
        if (coinStackCounter >= 30)
        {
            coinStackCounter -= 30;
            numberOfCoins += 1;
        }

        uiController.UpdateWaveRoundInformation(waveRound, numberOfEnemies, numberOfPoints, numberOfCoins);
    }

    //Remove Enemy from Gamefield
    public void EnemyDown()
    {
        numberOfEnemies -= 1;
        numberOfEnemiesKilled++;

        uiController.UpdateWaveRoundInformation(waveRound, numberOfEnemies, numberOfPoints, numberOfCoins);

        if (numberOfEnemies <= 0)
        {
            WaveStart();
        }
    }

    public void BossDown()
    {
        numberOfEnemies -= 1;
        numberOfBossesKilled++;

        uiController.UpdateWaveRoundInformation(waveRound, numberOfEnemies, numberOfPoints, numberOfCoins);

        BossClearStart();
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        currentGameState = GameState.GameEnd;

        repository.CreateGameSessionHistory(numberOfPoints, numberOfCoins, numberOfShotsFired, numberOfEnemiesKilled, numberOfBossesKilled);

        uiController.EndGame();
        uiController.UpdateWaveRoundInformationEndGame(waveRound, numberOfPoints, numberOfCoins, numberOfShotsFired, numberOfEnemiesKilled, numberOfBossesKilled);

        audioController.PlayEndGameSong();
    }

    #region Animations Related
    //End of Wave Round Information
    public void WaveInformationEnd()
    {
        NextWave();
    }

    //End of Boss Introduction
    public void BossIntroductionEnd()
    {
        currentGameState = GameState.View3D;
        playerController.ChangePlayerView();
        playerController.SetCanMove(true);

        enemyController.StartBossBattle();

        audioController.PlayBossSong();
    }

    //Boss's Death Start Animation
    public void BossClearStart()
    {
        currentGameState = GameState.BossEnd;

        fadeTimer = Time.time;

        ChangeView(2, 1);

        uiController.BossClearStartAnimation();

        playerController.SetCanMove(false);
        playerController.ResetPosition();

        audioController.MusicPause();
        audioController.PlayClearSFX();
    }

    //Boss's Death End Animation
    public void BossClearEnd()
    {
        currentGameState = GameState.View2D;

        playerController.ChangePlayerView();
        playerController.SetCanMove(true);

        WaveStart();

        audioController.PlayGameSong();
    }
    #endregion

    #region GameSceneControl
    private void PauseGame()
    {
        previousGameState = currentGameState;
        currentGameState = GameState.Pause;
        Time.timeScale = 0;
        uiController.PauseGame();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        currentGameState = previousGameState;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Personal Menu");
        Time.timeScale = 1;
    }
    #endregion
}
