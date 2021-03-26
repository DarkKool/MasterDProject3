using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    //Bullet Controller Reference
    private BulletController bulletController;

    //Player Controller Reference
    private PersonalPlayerController playerController;

    //Rigidbody Reference
    private Rigidbody rb;

    //Bullet Damage
    private float damage;

    //Bullet Speed
    private float speed;

    private void Start()
    {
        //Get References
        bulletController = GameObject.Find("BulletController").GetComponent<BulletController>();
        playerController = GameObject.Find("Player").GetComponent<PersonalPlayerController>();
        rb = GetComponent<Rigidbody>();

        //Set Damage
        damage = playerController.GetDamage();

        //Set Speed
        speed = 50f;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(0, 0, speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Probe":
                other.GetComponent<EnemyProbe>().TakeDamage(damage);
                bulletController.ReturnBullet(gameObject);
                break;
            case "Sentry":
                other.GetComponent<EnemySentry>().TakeDamage(damage);
                bulletController.ReturnBullet(gameObject);
                break;
            case "Tank":
                other.GetComponent<EnemyTank>().TakeDamage(damage);
                bulletController.ReturnBullet(gameObject);
                break;
            case "Observer":
                other.GetComponent<EnemyObserver>().TakeDamage(damage);
                bulletController.ReturnBullet(gameObject);
                break;
            case "Disruptor":
                other.GetComponent<EnemyDisruptor>().TakeDamage(damage);
                bulletController.ReturnBullet(gameObject);
                break;
            case "BulletEnd":
                bulletController.ReturnBullet(gameObject);
                break;
        }
    }
}
