using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMeleeEnemyFactory : CEnemyFactory
{
    #region private º¯¼ö
    [SerializeField]
    GameObject[] oMeleeEnemyPrefabs;
    #endregion

    public override void SetEnemyIndex(int count)
    {
        enemyIndex.Clear();

        while (enemyIndex.Count < count)
        {
            int index = Random.Range(0, oMeleeEnemyPrefabs.Length);
            bool isContain = enemyIndex.Contains(index);

            if (!isContain)
            {
                enemyIndex.Add(index);
            }
        }
    }

    public override void CreateEnemy()
    {
        int index = Random.Range(0, enemyIndex.Count);

        GameObject enemy = Instantiate(oMeleeEnemyPrefabs[enemyIndex[index]], transform);
        enemy.SetActive(false);
    }
}