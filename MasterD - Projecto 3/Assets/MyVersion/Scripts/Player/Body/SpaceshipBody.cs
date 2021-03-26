using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceshipBody : MonoBehaviour
{
    //Player Controller Reference
    protected PersonalPlayerController playerController;

    //Player's Health
    protected int healthPoints;

    //Player's Resistance
    protected float resistance;

    //Player's Slow Down Speed Effect
    protected float slowDownSpeedEffect;

    //Set Spaceship Body Stats
    protected virtual void SetBodyStats(PersonalPlayerController playerController, int healthPoints, float resistance, float slowDownSpeedEffect)
    {
        this.playerController = playerController;
        this.healthPoints = healthPoints;
        this.resistance = resistance;
        this.slowDownSpeedEffect = slowDownSpeedEffect;
    }

    public int GetHealthPoints()
    {
        return healthPoints;
    }

    public float GetResistance()
    {
        return resistance;
    }

    public float GetSlowDownSpeedEffect()
    {
        return slowDownSpeedEffect;
    }
}
