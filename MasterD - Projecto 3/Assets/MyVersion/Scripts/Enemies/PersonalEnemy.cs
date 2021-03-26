using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PersonalEnemy : MonoBehaviour
{
    //Enemy Controller Reference
    protected PersonalEnemyController enemyController;

    //Player Controller Reference
    protected PersonalPlayerController playerController;

    //Bullet Controller Reference
    protected BulletController bulletController;

    //Audio Controller Reference
    protected AudioController audioController;

    //Enemy Rigidbody Reference
    protected Rigidbody rb;

    //Enemy Bullet Point
    protected GameObject bulletPoint;

    //Enemy Health Points
    protected int healthPoints;

    //Enemy Resistance Value
    protected float resistance;

    //Enemy Travel Speed
    protected float speed;

    //Decide if Enemy Fires
    protected bool doesFire;

    //Enemy Fire Rate
    protected float fireRate;

    //Enemy Reload Time
    protected float reloadTime;

    //Enemy Damage
    protected float damage;

    //Enemy Point's Worth
    protected int points;

    //Enemy's Initialization
    protected virtual void Start()
    {
        //Get References
        enemyController = GameObject.Find("EnemyController").GetComponent<PersonalEnemyController>();
        playerController = GameObject.Find("Player").GetComponent<PersonalPlayerController>();
        bulletController = GameObject.Find("BulletController").GetComponent<BulletController>();
        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();

        rb = GetComponent<Rigidbody>();

        bulletPoint = transform.GetChild(0).gameObject;
    }

    //Set Enemy Properties
    public void SetProperties(int healthPoints, float resistance, float speed, bool doesFire, float fireRate, float reloadTime, float damage, int points)
    {
        this.healthPoints = healthPoints;
        this.resistance = resistance;
        this.speed = speed;
        this.doesFire = doesFire;
        this.fireRate = fireRate;
        this.reloadTime = reloadTime;
        this.damage = damage;
        this.points = points;
    }

    protected virtual void Update()
    {
        if(doesFire)
        {
            reloadTime -= Time.deltaTime;

            if(reloadTime <= 0)
            {
                Shoot();
                reloadTime = fireRate;
            }
        }
    }

    protected void FixedUpdate()
    {
        Travel();
    }

    //Travel
    protected virtual void Travel()
    {
        rb.velocity = new Vector3(0, 0, -speed);
    }

    //Shoot
    protected virtual void Shoot() { }

    //Take Damage
    public virtual void TakeDamage(float damage)
    {
        healthPoints -= Mathf.RoundToInt(damage - (damage * resistance));
        CheckHealthPoints();
    }

    private void CheckHealthPoints()
    {
        if(healthPoints <= 0)
        {
            enemyController.EnemyKilled(gameObject.tag);
            Destroy();
        }
    }

    public float GetDamage()
    {
        return damage;
    }

    protected abstract void Destroy();

    protected void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerController.TakeDamage(damage);
        }
        else if(other.tag == "EndGame")
        {
            enemyController.GameEnd();
        }
    }
}
