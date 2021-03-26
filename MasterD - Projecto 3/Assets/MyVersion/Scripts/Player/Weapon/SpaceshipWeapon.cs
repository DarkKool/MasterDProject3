using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceshipWeapon : MonoBehaviour
{
    //Player Controller Reference
    protected PersonalPlayerController playerController;

    //Player's Fire Rate
    protected float fireRate;

    //Player's Reload Time
    protected float reloadTime;

    //Set Spaceship Weapon Stats
    protected virtual void SetWeaponStats(PersonalPlayerController playerController, float fireRate, float reloadTime)
    {
        this.playerController = playerController;
        this.fireRate = fireRate;
        this.reloadTime = reloadTime;
    }

    public float GetFireRate()
    {
        return fireRate;
    }

    public float GetReloadTime()
    {
        return reloadTime;
    }
}
