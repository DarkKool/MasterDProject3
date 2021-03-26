using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceshipBoost : MonoBehaviour
{
    //Player Controller Reference
    protected PersonalPlayerController playerController;

    //Player's Speed
    protected float speed;

    //Set Spaceship Boost Stats
    protected virtual void SetBoostStats(PersonalPlayerController playerController, float speed)
    {
        this.playerController = playerController;
        this.speed = speed;
    }

    public float GetSpeed()
    {
        return speed;
    }
}
