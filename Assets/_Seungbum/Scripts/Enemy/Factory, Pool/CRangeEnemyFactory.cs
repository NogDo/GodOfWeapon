using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRangeEnemyFactory : CEnemyFactory
{
    #region private ����
    [SerializeField]
    GameObject[] oRangeEnemyPrefabs;
    #endregion

    public override void CreateEnemy()
    {
        GameObject enemy = Instantiate(oRangeEnemyPrefabs[0], transform);
        enemy.SetActive(false);
    }
}
