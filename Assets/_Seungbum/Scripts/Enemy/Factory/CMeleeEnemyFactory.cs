using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMeleeEnemyFactory : CEnemyFactory
{
    #region private º¯¼ö
    [SerializeField]
    GameObject[] oMeleeEnemyPrefabs;
    #endregion

    public override void CreateEnemy()
    {
        GameObject enemy = Instantiate(oMeleeEnemyPrefabs[0], transform);
        enemy.SetActive(false);
    }
}