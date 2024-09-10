using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemySphereIndicatorSkill : CEnemyIndicatorSkill
{
    #region public 변수
    public CEnemySphereIndicatorControl oIndicatorPrefab;
    #endregion

    #region private 변수
    [SerializeField]
    float fRadius;
    #endregion

    public override void Active(Transform target)
    {
        Vector3 spawnPosition = target.position;
        spawnPosition.y = 0.2f;

        CEnemySphereIndicatorControl indicator = Instantiate(oIndicatorPrefab);
        indicator.InitIndicator(spawnPosition, fAttack + fOwnerAttack, fRadius);

        indicator.ActiveIndicator();
    }
}