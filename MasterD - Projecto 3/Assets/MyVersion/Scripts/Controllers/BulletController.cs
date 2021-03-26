using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //Data Repository Reference
    private DataRepository repository;

    //Game Controller Reference
    private PersonalGameController gameController;

    //Player Reference
    private GameObject player;

    //Bullets Parent
    private Transform bulletsParent;

    //Bullets Array Pooling
    private Queue<GameObject> bullets;

    ////Maximum Stack Size
    private int maxPoolSize;

    //Bullets Reference
    private GameObject bulletPrefab;

    #region Enemy
    //Bullets Parent
    private Transform bulletsEnemyParent;

    //Bullets Array Pooling
    private Queue<GameObject> bulletsSentry;
    private Queue<GameObject> bulletsObserver;
    private Queue<GameObject> bulletsDisruptor;

    //Maximum Stack Size
    private int maxEnemyPoolSize;

    //Bullets Prefab Reference
    private GameObject bulletPrefabSentry;
    private GameObject bulletPrefabObserver;
    private GameObject bulletPrefabDisruptor;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        //Get References
        repository = GameObject.Find("DataRepository").GetComponent<DataRepository>();
        gameController = GameObject.Find("GameController").GetComponent<PersonalGameController>();
        player = gameController.GetPlayer();
        bulletsParent = GameObject.Find("Bullets").transform;

        //Set Bullet Loaded
        switch (repository.GetSelectedWeaponPart())
        {
            case "A":
                bulletPrefab = Resources.Load<GameObject>("Bullets/Prefabs/BulletA");
                break;
            case "B":
                bulletPrefab = Resources.Load<GameObject>("Bullets/Prefabs/BulletB");
                break;
            case "C":
                bulletPrefab = Resources.Load<GameObject>("Bullets/Prefabs/BulletC");
                break;
            case "D":
                bulletPrefab = Resources.Load<GameObject>("Bullets/Prefabs/BulletD");
                break;
            case "E":
                bulletPrefab = Resources.Load<GameObject>("Bullets/Prefabs/BulletE");
                break;
        }

        //Initialize Queue's
        bullets = new Queue<GameObject>();

        //Set Maximum Stack Size
        maxPoolSize = 100;

        //Enqueue Inactive Bullets Objects
        for (int i = 0; i < maxPoolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, player.transform.GetChild(0));
            bullets.Enqueue(bullet);
            bullet.SetActive(false);
        }

        #region Enemy
        //Get References
        bulletsParent = GameObject.Find("Bullets").transform;

        bulletPrefabSentry = Resources.Load<GameObject>("Bullets/Prefabs/BulletSentry");
        bulletPrefabObserver = Resources.Load<GameObject>("Bullets/Prefabs/BulletObserver");
        bulletPrefabDisruptor = Resources.Load<GameObject>("Bullets/Prefabs/BulletDisruptor");

        //Initialize Queue's
        bulletsSentry = new Queue<GameObject>();
        bulletsObserver = new Queue<GameObject>();
        bulletsDisruptor = new Queue<GameObject>();

        //Set Maximum Stack Size
        maxPoolSize = 60;

        //Enqueue Inactive Sentry Bullet Objects
        for (int i = 0; i < maxPoolSize; i++)
        {
            GameObject sentryBullet = Instantiate(bulletPrefabSentry, bulletsParent);
            bulletsSentry.Enqueue(sentryBullet);
            sentryBullet.SetActive(false);
        }

        //Enqueue Inactive Observer Bullet Objects
        for (int i = 0; i < maxPoolSize; i++)
        {
            GameObject observerBullet = Instantiate(bulletPrefabObserver, bulletsParent);
            bulletsObserver.Enqueue(observerBullet);
            observerBullet.SetActive(false);
        }

        //Enqueue Inactive Sentry Bullet Objects
        for (int i = 0; i < maxPoolSize; i++)
        {
            GameObject disruptorBullet = Instantiate(bulletPrefabDisruptor, bulletsParent);
            bulletsDisruptor.Enqueue(disruptorBullet);
            disruptorBullet.SetActive(false);
        }
        #endregion
    }

    #region Get and Return Bullet Section
    public GameObject GetBullet()
    {
        if (bullets.Count > 0)
        {
            GameObject bullet = bullets.Dequeue();
            bullet.SetActive(true);
            bullet.transform.parent = bulletsParent;
            return bullet;
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab, player.transform.GetChild(0));
            bullet.transform.parent = bulletsParent;
            return bullet;
        }
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullets.Enqueue(bullet);
        bullet.SetActive(false);
    }
    #endregion

    #region Get and Return Sentry Bullets Section
    public GameObject GetBulletSentry()
    {
        if (bulletsSentry.Count > 0)
        {
            GameObject bullet = bulletsSentry.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefabSentry, bulletsParent);
            return bullet;
        }
    }

    public void ReturnBulletSentry(GameObject bullet)
    {
        bulletsSentry.Enqueue(bullet);
        bullet.SetActive(false);
    }
    #endregion

    #region Get and Return Observer Bullets Section
    public GameObject GetBulletObserver()
    {
        if (bulletsObserver.Count > 0)
        {
            GameObject bullet = bulletsObserver.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefabObserver, bulletsParent);
            return bullet;
        }
    }

    public void ReturnBulletObserver(GameObject bullet)
    {
        bulletsObserver.Enqueue(bullet);
        bullet.SetActive(false);
    }
    #endregion

    #region Get and Return Disruptor Bullets Section
    public GameObject GetBulletDisruptor()
    {
        if (bulletsDisruptor.Count > 0)
        {
            GameObject bullet = bulletsDisruptor.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefabDisruptor, bulletsParent);
            return bullet;
        }
    }

    public void ReturnBulletDisruptor(GameObject bullet)
    {
        bulletsDisruptor.Enqueue(bullet);
        bullet.SetActive(false);
    }
    #endregion
}
