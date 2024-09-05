using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyInfoDeath : CEnemyInfo
{
    public override void Init()
    {
        if (stats == null)
        {
            stats = new EnemyStats(EAttackType.MELEE, 4.0f, 50.0f, 50.0f, 5.0f, 0.0f);
        }

        base.Init();
    }
}