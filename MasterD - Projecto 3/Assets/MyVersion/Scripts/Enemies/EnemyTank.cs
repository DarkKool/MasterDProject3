using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : PersonalEnemy
{
    protected override void Destroy()
    {
        enemyController.ReturnTank(gameObject);
    }
}
