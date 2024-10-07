using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBossFactory : CEnemyFactory
{
    #region private º¯¼ö
    [SerializeField]
    GameObject[] oBossPrefabs;
    #endregion

    public override void SetEnemyIndex(int count)
    {

    }

    public override void CreateEnemy()
    {
        GameObject enemy = Instantiate(oBossPrefabs[0], transform);
        enemy.SetActive(false);
        enemy.SetActive(true);
    }
}
