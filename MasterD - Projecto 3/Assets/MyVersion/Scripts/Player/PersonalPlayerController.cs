using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalPlayerController : MonoBehaviour
{
    //Game Controller Reference
    private PersonalGameController gameController;

    //Bullet Controller Reference
    private BulletController bulletController;

    //Audio Controller Reference
    private AudioController audioController;

    //Rigidbody Reference
    private Rigidbody rb;

    //Stats Arrays
    private Dictionary<string, List<float>> bodyStats = new Dictionary<string, List<float>>{
        //                      HP,RES,VEL
        ["A"] = new List<float> {5, 1, 20},
        ["B"] = new List<float> {12, 3, 25},
        ["C"] = new List<float> {20, 5, 30},
        ["D"] = new List<float> {30, 9, 35},
        ["E"] = new List<float> {50, 15, 50},
    };

    private Dictionary<string, float> boostStats = new Dictionary<string, float>
    {
        //     SPD
        ["A"] = 5,
        ["B"] = 8,
        ["C"] = 12,
        ["D"] = 18,
        ["E"] = 25
    };

    private Dictionary<string, List<float>> weaponStats = new Dictionary<string, List<float>>
    {
        //                     FIRE,DMG
        ["A"] = new List<float> {1.8f,  3},
        ["B"] = new List<float> {1.4f, 6},
        ["C"] = new List<float> {1.1f, 10},
        ["D"] = new List<float> {1f, 16},
        ["E"] = new List<float> {0.85f, 20}
    };

    private Dictionary<string, GameObject> bullets;

    #region Spaceship Stats

    //Spaceship Health
    private float healthPoints;

    //Spaceship Resistance
    private float resistance;

    //Spaceship SlowDownSpeedEffect
    private float slowDownSpeedEffect;

    //Spaceship Speed
    private float speed;

    //Spaceship Fire Rate
    private float fireRate;

    //Spaceship Damage
    private float damage;

    //Spaceship Reload Time
    private float reloadTime;

    //Spaceship Distance Traveled
    private float distanceTraveled;

    #endregion

    //Indicator if Player's able to Move
    private bool canMove;

    //Constraint Player Movement
    private bool movement2D;
    private bool movement3D;

    //Player Input
    private float inputHorizontal;
    private float inputVertical;

    //Player Movement Vector
    private Vector3 movementDirection;

    private void Start()
    {
        //Get References
        gameController = GameObject.Find("GameController").GetComponent<PersonalGameController>();
        bulletController = GameObject.Find("BulletController").GetComponent<BulletController>();
        audioController = GameObject.Find("AudioController").GetComponent<AudioController>();

        rb = GetComponent<Rigidbody>();

        //Initialize Bullet Dictionary
         bullets = new Dictionary<string, GameObject>
         {
             //["A"] = Resources.Load<GameObject>("Bullets/Prefabs/BulletA")
         };

        //Set Player's Ability to Move
        canMove = false;

        //Set Player's Initial Movement
        movement2D = true;
        movement3D = false;
    }

    private void Update()
    {
        //If the Player can move, detect Input
        if (canMove)
        {
            //Player's Input
            inputHorizontal = Input.GetAxisRaw("Horizontal");
            inputVertical = Input.GetAxisRaw("Vertical");

            if (movement2D)
            {
                movementDirection = new Vector3(inputHorizontal, 0 , 0) * 25;
            }
            else
            {
                movementDirection = new Vector3(inputHorizontal, inputVertical, 0).normalized * 25;
            }

            //Decrement ReloadTime
            reloadTime -= Time.deltaTime;

            //Shoot
            if (Input.GetKey(KeyCode.Space) && reloadTime <= 0)
            {
                Shoot();
            }

            //Adding Points if enough Distance Traveled
            distanceTraveled += speed * Time.deltaTime;
            if (distanceTraveled > 1 && Mathf.FloorToInt(distanceTraveled) % 50 == 0)
            {
                gameController.AddPoints(1);
                distanceTraveled = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = movementDirection.normalized * 25f;
    }

    //Allow or Disallow Player's Movement and Removes Speed
    public void SetCanMove(bool ableToMove)
    {
        canMove = ableToMove;
        StopMovement();
    }

    //Change Players Control
    public void ChangePlayerView()
    {
        movement2D = !movement2D;
        movement3D = !movement3D;
    }

    //Centers Player in Game
    public void ResetPosition()
    {
        transform.position = Vector3.zero;
    }

    //Stop Movement
    public void StopMovement()
    {
        movementDirection = Vector3.zero;
    }

    //Set Spaceship Stats
    public void SetStats(string spaceshipBodyName, string spaceshipBoostName, string spaceshipWeaponName)
    {
        //Spaceship Body Stats
        healthPoints = bodyStats[spaceshipBodyName][0];
        resistance = bodyStats[spaceshipBodyName][1];
        slowDownSpeedEffect = bodyStats[spaceshipBodyName][2];

        //Spaceship Boost Stats
        speed = boostStats[spaceshipBoostName];

        //Spaceship Weapon Stats
        fireRate = weaponStats[spaceshipWeaponName][0];
        damage = weaponStats[spaceshipWeaponName][1];
    }

    private void Shoot()
    {
        bulletController.GetBullet();
        gameController.AddShots();
        audioController.PlayFireSFX();

        reloadTime = fireRate;
    }

    public void TakeDamage(float damage)
    {
        healthPoints -= damage;

        CheckHealth();
    }

    private void CheckHealth()
    {
        if(healthPoints <= 0)
        {
            gameController.EndGame();
        }
    }

    #region Get's
    //Return Speed With SpeedSlowDownEffect
    public float GetFinalSpeed()
    {
        return speed - (speed * (slowDownSpeedEffect / 100));
    }

    //Return Bullet Damage
    public float GetDamage()
    {
        return damage;
    }
    #endregion

    //Start Animation Finished
    private void IntroFinished()
    {
        gameController.WaveStart();
        GetComponent<Animator>().enabled = false;
        SetCanMove(true);
    }
}
