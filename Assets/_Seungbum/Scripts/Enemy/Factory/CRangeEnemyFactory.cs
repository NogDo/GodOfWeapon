using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRangeEnemyFactory : CEnemyFactory
{
    #region private º¯¼ö
    [SerializeField]
    GameObject[] oRangeEnemyPrefabs;
    #endregion

    public override void SetEnemyIndex(int count)
    {
        enemyIndex.Clear();

        while (enemyIndex.Count < count)
        {
            int index = Random.Range(0, oRangeEnemyPrefabs.Length);
            bool isContain = enemyIndex.Contains(index);

            if (!isContain)
            {
                enemyIndex.Add(index);
            }
        }
    }

    public override void CreateEnemy()
    {
        if (enemyIndex.Count <= 0)
        {
            return;
        }

        int index = Random.Range(0, enemyIndex.Count);

        GameObject enemy = Instantiate(oRangeEnemyPrefabs[enemyIndex[index]], transform);
        enemy.SetActive(false);
    }
}
