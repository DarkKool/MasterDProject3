using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //Bullet Controller Reference
    private BulletController bulletController;

    //Player Controller Reference
    private PersonalEnemy enemy;

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
        rb = GetComponent<Rigidbody>();

        //Set Speed
        speed = 50f;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(0, 0, -speed);
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void ReturnBullet()
    {
        switch (tag)
        {
            case "BulletSentry":
                bulletController.ReturnBulletSentry(gameObject);
                break;
            case "BulletObserver":
                bulletController.ReturnBulletObserver(gameObject);
                break;
            case "BulletDisruptor":
                bulletController.ReturnBulletDisruptor(gameObject);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            other.GetComponent<PersonalPlayerController>().TakeDamage(damage);
            ReturnBullet();
        }
        else if (other.tag == "EndGame")
        {
            ReturnBullet();
        }
    }
}
