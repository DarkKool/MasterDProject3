using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDisruptor : PersonalEnemy
{
    //Battle Start Flag
    private bool battleStart = false;

    //Shooting Pattern
    private int pattern;

    //Shooting State Flag
    private bool isShooting = false;

    //Pattern Chosen Flag
    private bool hasPattern = false;

    //Shoot Routine
    private bool isShootingRoutine = false;

    //List Holding Active Bullets
    private List<GameObject> bullets;

    protected override void Start()
    {
        base.Start();

        reloadTime = fireRate;
    
        bullets = new List<GameObject>();
    }

    protected override void Update()
    {
        if (battleStart)
        {
            if (!isShooting)
            {
                reloadTime -= Time.deltaTime;
                if (reloadTime <= 0)
                {
                    isShooting = true;
                    reloadTime = fireRate;
                }
            }
            else
            {
                if (!hasPattern)
                {
                    pattern = Random.Range(0, 2);
                    hasPattern = true;
                }
                else
                {
                    if (!isShootingRoutine) {
                        isShootingRoutine = true;
                        StartCoroutine(Fire(pattern));
                    }
                }
            }
        }
    }

    protected IEnumerator Fire(int pattern)
    {
        if (pattern == 0)
        {
            Shoot();
        }
        else
        {
            int numberOfShots = 0;

            while (numberOfShots < 3) { 
                Shoot();
                numberOfShots++;
                yield return new WaitForSeconds(0.5f);
            }

        }

        isShooting = false;
        hasPattern = false;
        isShootingRoutine = false;
    }

    protected override void Shoot()
    {
        GameObject bullet = bulletController.GetBulletDisruptor();
        bullet.transform.position = transform.position;
        bullet.GetComponent<EnemyBullet>().SetDamage(damage);

        bullets.Add(bullet);

        audioController.PlayFireSFX();
    }

    protected override void Destroy()
    {
        foreach(GameObject bullet in bullets)
        {
            if(bullet != null)
            {
                bullet.GetComponent<EnemyBullet>().ReturnBullet();
            }
        }

        enemyController.ReturnDisruptor(gameObject);
    }

    public void StartBattle()
    {
        battleStart = true;
    }
}
