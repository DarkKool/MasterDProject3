using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalEnemyController : MonoBehaviour
{
    //Game Controller Reference
    private PersonalGameController gameController;

    //Disruptor Animator Reference
    private DisruptorAnimator disruptorAnimator;

    //Enemies Spawn Position
    private Vector3 enemiesSpawnPosition;

    //Enemies Boss Spawn Position
    private Vector3 bossSpawnPosition;

    //Enemies Parent
    private Transform enemiesParent;

    //Enemies Array Pooling
    private Queue<GameObject> probes;
    private Queue<GameObject> sentries;
    private Queue<GameObject> tanks;
    private Queue<GameObject> observers;
    private Queue<GameObject> disruptors;

    //Maximum Stack Size
    private int maxPoolSize;

    //Enemies Reference
    private GameObject probePrefab;
    private GameObject sentryPrefab;
    private GameObject tankPrefab;
    private GameObject observerPrefab;
    private GameObject disruptorPrefab;

    //Enemy Probe Stats
    private int     initialHealthPointsProbe = 5;
    private float   initialResistanceProbe = 0;
    private float   initialSpeedProbe = 3f;
    private bool    initialDoesFireProbe = false;
    private float   initialFireRateProbe = 0;
    private float   initialReloadTimeProbe = 0;
    private float   initialDamageProbe = 0;
    private int     pointsProbe = 2;

    //Enemy Sentry Stats
    private int     initialHealthPointsSentry = 8;
    private float   initialResistanceSentry = 0.05f;
    private float   initialSpeedSentry = 3.8f;
    private bool    initialDoesFireSentry = true;
    private float   initialFireRateSentry = 1.5f;
    private float   initialReloadTimeSentry = 1.5f;
    private float   initialDamageSentry = 2;
    private int     pointsSentry = 5;

    //Enemy Tank Stats
    private int     initialHealthPointsTank = 20;
    private float   initialResistanceTank = 0.20f;
    private float   initialSpeedTank = 2.9f;
    private bool    initialDoesFireTank = false;
    private float   initialFireRateTank = 0;
    private float   initialReloadTimeTank = 0;
    private float   initialDamageTank = 0;
    private int     pointsTank = 10;

    //Enemy Observer Stats
    private int     initialHealthPointsObserver = 15;
    private float   initialResistanceObserver = 0.10f;
    private float   initialSpeedObserver = 3.2f;
    private bool    initialDoesFireObserver = true;
    private float   initialFireRateObserver = 3;
    private float   initialReloadTimeObserver = 3f;
    private float   initialDamageObserver = 6;
    private int     pointsObserver = 20;

    //Enemy Disruptor Stats
    private int     initialHealthPointsDisruptor = 30;
    private float   initialResistanceDisruptor = 0.25f;
    private float   initialSpeedDisruptor = 0;
    private bool    initialDoesFireDisruptor = true;
    private float   initialFireRateDisruptor = 2;
    private float   initialReloadTimeDisruptor = 2f;
    private float   initialDamageDisruptor = 15;
    private int     pointsDisruptor = 50;

    //Enemy Spawn Flags
    private bool isSpawnProbe;
    private bool isSpawnSentry;
    private bool isSpawnTank;
    private bool isSpawnObserver;

    //Enemy Spawn Probability
    private int initialSpawnProbabilitiesProbe;
    private int initialSpawnProbabilitiesSentry;
    private int initialSpawnProbabilitiesTank;
    private int initialSpawnProbabilitiesObserver;
    private int maxProbability;

    //Spawn X Range
    private float xSpawnRange;

    private void Start()
    {
        //Get References
        gameController = GameObject.Find("GameController").GetComponent<PersonalGameController>();
        enemiesSpawnPosition = transform.GetChild(1).transform.position;
        bossSpawnPosition = transform.GetChild(0).transform.position;
        enemiesParent = GameObject.Find("Enemies").transform;

        probePrefab = Resources.Load<GameObject>("Enemies/Prefabs/Probe");
        sentryPrefab = Resources.Load<GameObject>("Enemies/Prefabs/Sentry");
        tankPrefab = Resources.Load<GameObject>("Enemies/Prefabs/Tank");
        observerPrefab = Resources.Load<GameObject>("Enemies/Prefabs/Observer");
        disruptorPrefab = Resources.Load<GameObject>("Enemies/Prefabs/Disruptor");

        //Initialize Queue's
        probes = new Queue<GameObject>();
        sentries = new Queue<GameObject>();
        tanks = new Queue<GameObject>();
        observers = new Queue<GameObject>();
        disruptors = new Queue<GameObject>();

        //Set Maximum Stack Size
        maxPoolSize = 40;

        //Enqueue Inactive Probes Objects
        for (int i = 0; i < maxPoolSize; i++)
        {
            GameObject probe = Instantiate(probePrefab, enemiesParent);
            probes.Enqueue(probe);
            probe.SetActive(false);
        }

        //Enqueue Inactive Sentries Objects
        for (int i = 0; i < maxPoolSize; i++)
        {
            GameObject sentry = Instantiate(sentryPrefab, enemiesParent);
            sentries.Enqueue(sentry);
            sentry.SetActive(false);
        }

        //Enqueue Inactive Tanks Objects
        for (int i = 0; i < maxPoolSize; i++)
        {
            GameObject tank = Instantiate(tankPrefab, enemiesParent);
            tanks.Enqueue(tank);
            tank.SetActive(false);
        }

        //Enqueue Inactive Observers Objects
        for (int i = 0; i < maxPoolSize; i++)
        {
            GameObject observer = Instantiate(observerPrefab, enemiesParent);
            observers.Enqueue(observer);
            observer.SetActive(false);
        }

        //Enqueue Inactive Disruptor Object
        GameObject disruptor = Instantiate(disruptorPrefab, enemiesParent);
        disruptors.Enqueue(disruptor);
        disruptor.SetActive(false);

        //Set Flags
        isSpawnProbe = true;
        isSpawnSentry = false;
        isSpawnTank = false;
        isSpawnObserver = false;
        
        //Set Probabilities
        initialSpawnProbabilitiesProbe = 45;
        initialSpawnProbabilitiesSentry = 30;
        initialSpawnProbabilitiesTank = 20;
        initialSpawnProbabilitiesObserver = 5;
        maxProbability = initialSpawnProbabilitiesProbe;

        //Set Spawn Range
        xSpawnRange = 25f;
    }

    #region Probe Section
    public GameObject GetProbe()
    {
        if(probes.Count > 0)
        {
            GameObject probe = probes.Dequeue();
            probe.SetActive(true);
            return probe;
        }
        else
        {
            GameObject probe = Instantiate(probePrefab);
            return probe;
        }
    }

    public void ReturnProbe(GameObject probe)
    {
        probes.Enqueue(probe);
        probe.SetActive(false);
    }
    #endregion

    #region Sentry Section
    public GameObject GetSentry()
    {
        if (sentries.Count > 0)
        {
            GameObject sentry = sentries.Dequeue();
            sentry.SetActive(true);
            return sentry;
        }
        else
        {
            GameObject sentry = Instantiate(sentryPrefab);
            return sentry;
        }
    }

    public void ReturnSentry(GameObject sentry)
    {
        sentries.Enqueue(sentry);
        sentry.SetActive(false);
    }
    #endregion

    #region Tank Section
    public GameObject GetTank()
    {
        if (tanks.Count > 0)
        {
            GameObject tank = tanks.Dequeue();
            tank.SetActive(true);
            return tank;
        }
        else
        {
            GameObject tank = Instantiate(tankPrefab);
            return tank;
        }
    }

    public void ReturnTank(GameObject tank)
    {
        tanks.Enqueue(tank);
        tank.SetActive(false);
    }
    #endregion

    #region Observer Section
    public GameObject GetObserver()
    {
        if (observers.Count > 0)
        {
            GameObject observer = observers.Dequeue();
            observer.SetActive(true);
            return observer;
        }
        else
        {
            GameObject observer = Instantiate(observerPrefab);
            return observer;
        }
    }

    public void ReturnObserver(GameObject observer)
    {
        observers.Enqueue(observer);
        observer.SetActive(false);
    }
    #endregion

    #region Disruptor Section
    public GameObject GetDisruptor()
    {
        if (disruptors.Count > 0)
        {
            GameObject disruptor = disruptors.Dequeue();
            disruptor.SetActive(true);
            return disruptor;
        }
        else
        {
            GameObject disruptor = Instantiate(disruptorPrefab);
            return disruptor;
        }
    }

    public void ReturnDisruptor(GameObject disruptor)
    {
        disruptors.Enqueue(disruptor);
        disruptor.SetActive(false);
    }
    #endregion

    public void ChooseEnemies(int numberOfEnemies, int roundWave)
    {
        //Quanto maior for a ronda, maior a probabilidade de serem Observers.
        //Quanto maior for a ronda, mais fortes os inimigos serão
        if(roundWave > 0 && roundWave < 5)
        {
            StartCoroutine(SpawnEnemies(numberOfEnemies, roundWave));
        }
        else if (roundWave > 5 && roundWave < 10)
        {
            isSpawnSentry = true;
            maxProbability = initialSpawnProbabilitiesProbe + initialSpawnProbabilitiesSentry;
            StartCoroutine(SpawnEnemies(numberOfEnemies, roundWave));
        }
        else if (roundWave > 10 && roundWave < 15)
        {
            isSpawnTank = true;
            maxProbability = initialSpawnProbabilitiesProbe + initialSpawnProbabilitiesSentry + initialSpawnProbabilitiesTank;
            StartCoroutine(SpawnEnemies(numberOfEnemies, roundWave));
        }
        else if (roundWave > 15 && roundWave < 20)
        {
            isSpawnObserver = true;
            maxProbability = initialSpawnProbabilitiesProbe + initialSpawnProbabilitiesSentry + initialSpawnProbabilitiesTank + initialSpawnProbabilitiesObserver;
            StartCoroutine(SpawnEnemies(numberOfEnemies, roundWave));
        }
    }

    private IEnumerator SpawnEnemies(int numberOfEnemies, int roundWave)
    {
        int spawnedEnemies = 0;

        while (spawnedEnemies < numberOfEnemies)
        {
            if(isSpawnProbe && !isSpawnSentry && !isSpawnTank && !isSpawnObserver)
            {
                CompleteSpawnProbe(roundWave);
                spawnedEnemies++;
            }
            else if (isSpawnProbe && isSpawnSentry && !isSpawnTank && !isSpawnObserver)
            {
                int spawnEnemy = Random.Range(1, maxProbability + 1);

                if(spawnEnemy <= initialSpawnProbabilitiesProbe)
                {
                    CompleteSpawnProbe(roundWave);
                }
                else if(spawnEnemy > initialSpawnProbabilitiesProbe)
                {
                    CompleteSpawnSentry(roundWave);
                }

                spawnedEnemies++;
            }
            else if (isSpawnProbe && isSpawnSentry && isSpawnTank && !isSpawnObserver)
            {
                int spawnEnemy = Random.Range(1, maxProbability + 1);

                if (spawnEnemy <= initialSpawnProbabilitiesProbe)
                {
                    CompleteSpawnProbe(roundWave);
                }
                else if (spawnEnemy > initialSpawnProbabilitiesProbe && spawnEnemy <= initialSpawnProbabilitiesProbe + initialSpawnProbabilitiesSentry)
                {
                    CompleteSpawnSentry(roundWave);
                }
                else if (spawnEnemy > initialSpawnProbabilitiesProbe + initialSpawnProbabilitiesSentry)
                {
                    CompleteSpawnTank(roundWave);
                }

                spawnedEnemies++;
            }
            else if (isSpawnProbe && isSpawnSentry && isSpawnTank && isSpawnObserver)
            {
                int spawnEnemy = Random.Range(1, maxProbability + 1);

                if (spawnEnemy <= initialSpawnProbabilitiesProbe)
                {
                    CompleteSpawnProbe(roundWave);
                }
                else if (spawnEnemy > initialSpawnProbabilitiesProbe && spawnEnemy <= initialSpawnProbabilitiesProbe + initialSpawnProbabilitiesSentry)
                {
                    CompleteSpawnSentry(roundWave);
                }
                else if (spawnEnemy > initialSpawnProbabilitiesProbe + initialSpawnProbabilitiesSentry && spawnEnemy <= initialSpawnProbabilitiesProbe + initialSpawnProbabilitiesSentry + initialSpawnProbabilitiesTank)
                {
                    CompleteSpawnTank(roundWave);
                }
                else if (spawnEnemy > initialSpawnProbabilitiesProbe + initialSpawnProbabilitiesSentry + initialSpawnProbabilitiesTank)
                {
                    CompleteSpawnObserver(roundWave);
                }

                spawnedEnemies++;
            }

            yield return new WaitForSeconds(Mathf.Clamp(1 + 5 * Mathf.Pow(2.71828f, -roundWave / 10), 2.5f, 4));
        }
    }

    #region Complete Spawns
    private void CompleteSpawnProbe(int roundWave)
    {
        GameObject probe = GetProbe();
        probe.transform.position = new Vector3(Random.Range(-xSpawnRange, xSpawnRange), enemiesSpawnPosition.y, enemiesSpawnPosition.z);

        EnemyProbe probeStats = probe.GetComponent<EnemyProbe>();
        probeStats.SetProperties(Mathf.Clamp(initialHealthPointsProbe * (roundWave / 5), initialHealthPointsProbe, 15),
                                 initialResistanceProbe,
                                 Mathf.Clamp(initialSpeedProbe * (roundWave / 5), initialSpeedProbe, 4.5f),
                                 initialDoesFireProbe,
                                 initialFireRateProbe,
                                 initialReloadTimeProbe,
                                 initialDamageProbe,
                                 pointsProbe);
    }

    private void CompleteSpawnSentry(int roundWave)
    {
        GameObject sentry = GetSentry();
        sentry.transform.position = new Vector3(Random.Range(-xSpawnRange, xSpawnRange), enemiesSpawnPosition.y, enemiesSpawnPosition.z);

        EnemySentry sentryStats = sentry.GetComponent<EnemySentry>();
        sentryStats.SetProperties(Mathf.Clamp(initialHealthPointsSentry * ((roundWave - 5) / 5), initialHealthPointsSentry, 25),
                                  Mathf.Clamp(initialResistanceSentry * ((roundWave - 5) / 5), initialResistanceSentry, 0.12f),
                                  Mathf.Clamp(initialSpeedSentry * ((roundWave - 5 ) / 5), initialSpeedSentry, 5f),
                                  initialDoesFireSentry,
                                  initialFireRateSentry,
                                  initialReloadTimeSentry,
                                  initialDamageSentry,
                                  pointsSentry);
    }

    private void CompleteSpawnTank(int roundWave)
    {
        GameObject tank = GetTank();
        tank.transform.position = new Vector3(Random.Range(-xSpawnRange, xSpawnRange), enemiesSpawnPosition.y, enemiesSpawnPosition.z);

        EnemyTank tankStats = tank.GetComponent<EnemyTank>();
        tankStats.SetProperties(Mathf.Clamp(initialHealthPointsTank * ((roundWave - 10) / 5), initialHealthPointsTank, 50),
                                Mathf.Clamp(initialResistanceTank * ((roundWave - 10) / 5), initialResistanceTank, 0.35f),
                                Mathf.Clamp(initialSpeedTank * ((roundWave - 10) / 5), initialSpeedTank, 4.25f),
                                initialDoesFireTank,
                                initialFireRateTank,
                                initialReloadTimeTank,
                                initialDamageTank,
                                pointsTank);
    }

    private void CompleteSpawnObserver(int roundWave)
    {
        GameObject observer = GetObserver();
        observer.transform.position = new Vector3(Random.Range(-xSpawnRange, xSpawnRange), enemiesSpawnPosition.y, enemiesSpawnPosition.z);

        EnemyObserver observerStats = observer.GetComponent<EnemyObserver>();
        observerStats.SetProperties(Mathf.Clamp(initialHealthPointsObserver * ((roundWave - 15) / 5), initialHealthPointsObserver, 35),
                                    Mathf.Clamp(initialResistanceObserver * ((roundWave -15) / 5), initialResistanceObserver, 0.25f),
                                    Mathf.Clamp(initialSpeedObserver * ((roundWave - 15) / 5), initialSpeedObserver, 4f),
                                    initialDoesFireObserver,
                                    initialFireRateObserver,
                                    initialReloadTimeObserver,
                                    initialDamageObserver,
                                    pointsObserver);
    }
    #endregion

    #region Boss
    public void SpawnBoss(int roundWave)
    {
        //Quanto maior a ronda, mais forte o Boss
        GameObject disruptor = GetDisruptor();
        disruptor.transform.position = bossSpawnPosition;

        EnemyDisruptor disruptorStats = disruptor.GetComponent<EnemyDisruptor>();
        disruptorStats.SetProperties(Mathf.Clamp(initialHealthPointsDisruptor * ((roundWave - 5) / 5), initialHealthPointsDisruptor, 120),
                                     Mathf.Clamp(initialResistanceDisruptor * ((roundWave - 5) / 5), initialResistanceDisruptor, 0.40f),
                                     initialSpeedDisruptor,
                                     initialDoesFireObserver,
                                     initialFireRateDisruptor,
                                     initialReloadTimeDisruptor,
                                     initialDamageDisruptor,
                                     pointsDisruptor);

        disruptorAnimator = disruptor.GetComponent<DisruptorAnimator>();
    }

    public void StartBossBattle()
    {
        disruptorAnimator.StartBattle();
    }
    #endregion

    public void EnemyKilled(string enemyTag)
    {
        switch (enemyTag)
        {
            case ("Probe"):
                gameController.AddPoints(pointsProbe);
                gameController.EnemyDown();
                break;
            case ("Sentry"):
                gameController.AddPoints(pointsSentry);
                gameController.EnemyDown();
                break;
            case ("Tank"):
                gameController.AddPoints(pointsTank);
                gameController.EnemyDown();
                break;
            case ("Observer"):
                gameController.AddPoints(pointsObserver);
                gameController.EnemyDown();
                break;
            case ("Disruptor"):
                gameController.AddPoints(pointsDisruptor);
                gameController.BossDown();
                break;
        }
    }

    public void GameEnd()
    {
        gameController.EndGame();
    }
}
