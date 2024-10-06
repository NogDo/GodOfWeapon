using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEliteEnemyFactory : CEnemyFactory
{
    #region private º¯¼ö
    [SerializeField]
    GameObject[] oEliteEnemyPrefabs;
    #endregion

    public override void SetEnemyIndex(int count)
    {

    }

    public override void CreateEnemy()
    {
        GameObject enemy = Instantiate(oEliteEnemyPrefabs[0], transform);
        enemy.SetActive(false);
        enemy.SetActive(true);
    }
}