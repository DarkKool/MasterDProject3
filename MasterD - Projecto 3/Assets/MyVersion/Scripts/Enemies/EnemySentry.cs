using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySentry : PersonalEnemy
{
    //List Holding Active Bullets
    private List<GameObject> bullets;

    protected override void Start()
    {
        base.Start();

        bullets = new List<GameObject>();

        reloadTime = fireRate;
    }

    protected override void Shoot()
    {
        GameObject bullet = bulletController.GetBulletSentry();
        bullet.transform.position = transform.position;
        bullet.GetComponent<EnemyBullet>().SetDamage(damage);

        bullets.Add(bullet);

        audioController.PlayFireSFX();
    }

    protected override void Destroy()
    {
        foreach (GameObject bullet in bullets)
        {
            if (bullet != null)
            {
                bullet.GetComponent<EnemyBullet>().ReturnBullet();
            }
        }

        enemyController.ReturnSentry(gameObject);
    }
}
