using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyInfoVampireBat : CEnemyInfo
{
    public override void Init()
    {
        if (stats == null)
        {
            stats = new EnemyStats(EAttackType.RANGE, 3.0f, 50.0f, 50.0f, 5.0f, 3.0f);
        }

        base.Init();
    }
}