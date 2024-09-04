using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyInfoVampireBat : CEnemyInfo
{
    public override void Init()
    {
        if (stats == null)
        {
            stats = new EnemyStats(EAttackType.RANGE, 3.0f, 100.0f, 100.0f, 5.0f);
        }

        else
        {
            stats.fNowHp = stats.fMaxHp;
        }
    }
}