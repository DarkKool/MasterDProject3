using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProbe : PersonalEnemy
{
    protected override void Destroy()
    {
        enemyController.ReturnProbe(gameObject);
    }
}
