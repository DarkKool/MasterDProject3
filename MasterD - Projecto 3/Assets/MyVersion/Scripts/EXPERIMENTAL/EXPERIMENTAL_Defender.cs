using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPERIMENTAL_Defender : EXPERIMENTAL2_Enemy
{
    [SerializeField] private float healthDefender;

    protected override void Init()
    {
        PropertiesSetup(healthDefender);
    }
}
