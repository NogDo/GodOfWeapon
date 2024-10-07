using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemySphereIndicatorSkill : CEnemyIndicatorSkill
{
    #region private º¯¼ö
    [SerializeField]
    float fRadius;
    #endregion

    public override void Active(Transform target)
    {
        Vector3 spawnPosition = target.position;
        spawnPosition.y = 0.2f;

        CEnemySphereIndicatorControl indicator = CEnemyIndicatorManager.Instance.SpawnSphereIndicator();
        indicator.InitIndicator(spawnPosition, fAttack + fOwnerAttack, fRadius, fDuration);

        indicator.gameObject.SetActive(true);

        indicator.ActiveIndicator();
    }
}