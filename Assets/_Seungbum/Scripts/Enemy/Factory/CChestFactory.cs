using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CChestFactory : CEnemyFactory
{
    #region private ����
    [SerializeField]
    GameObject[] oChestPrefabs;
    #endregion

    public override void CreateEnemy()
    {
        Instantiate(oChestPrefabs[0], transform);
    }
}
