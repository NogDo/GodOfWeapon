using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CChestFactory : CEnemyFactory
{
    #region private º¯¼ö
    [SerializeField]
    GameObject[] oChestPrefabs;
    #endregion

    public override void CreateEnemy()
    {
        GameObject chest = Instantiate(oChestPrefabs[0], transform);
        chest.SetActive(false);
    }
}
