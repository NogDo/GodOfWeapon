using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyInfoDeath : CEnemyInfo
{
    public override void Init()
    {
        attackType = EAttackType.MELEE;

        fSpeed = 5.0f;
        fMaxHp = 100.0f;
        fNowHp = fMaxHp;
    }
}