using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EXPERIMENTAL2_Enemy : MonoBehaviour
{
    //SpaceShip Health Points
    protected float healthPoints;

    //SpaceShip Reload Time
    protected float reloadTimer;

    //SpaceShip Shoot Timer
    protected float shootTimer;

    //SpaceShip Movement Speed
    protected float movementSpeed;

    //SpaceShip Points
    protected float points; //Not sure if needed HERE.

    //SpaceShip Bullets
    protected GameObject bullet;

    protected void PropertiesSetup(float healthPoints)
    {
        this.healthPoints = healthPoints;
    }

    protected void PropertiesSetup(float healthPoints, float movementSpeed)
    {
        this.healthPoints = healthPoints;
        this.movementSpeed = movementSpeed;
    }

    protected void PropertiesSetup(float healthPoints, float shootTimer, float reloadTimer)
    {
        this.healthPoints = healthPoints;
        this.shootTimer = shootTimer;
        this.reloadTimer = reloadTimer;
    }

    protected void PropertiesSetup(float healthPoints, float movementSpeed, float shootTimer, float reloadTimer)
    {
        this.healthPoints = healthPoints;
        this.movementSpeed = movementSpeed;
        this.shootTimer = shootTimer;
        this.reloadTimer = reloadTimer;
    }

    protected void PropertiesSetup(float healthPoints, float movementSpeed, float shootTimer, float reloadTimer, GameObject bullet)
    {
        this.healthPoints = healthPoints;
        this.movementSpeed = movementSpeed;
        this.shootTimer = shootTimer;
        this.reloadTimer = reloadTimer;
        this.bullet = bullet;
    }

    protected void Start()
    {
        Init();
    }

    protected abstract void Init();

    protected Vector3 StraightDownMovement(float movementSpeed)
    {
        return Vector3.down * movementSpeed;
    }

    protected Vector3 TargetedMovement(float movementSpeed, Transform thisPosition, Transform target)
    {
        return (target.position - thisPosition.position) * movementSpeed * Time.deltaTime;
    }

    protected void DecreasteShootTimer(float shootTimer)
    {
        shootTimer -= Time.deltaTime;
    }

    protected void Shoot(GameObject bullet, Transform spawnPosition)
    {
        Instantiate(bullet, spawnPosition);
    }

    protected float Reload(float shootTimer, float reloadTimer)
    {
        return shootTimer = reloadTimer;
    }
}
