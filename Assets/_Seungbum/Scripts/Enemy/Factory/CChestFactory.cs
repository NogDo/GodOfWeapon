using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CChestFactory : CEnemyFactory
{
    #region private º¯¼ö
    [SerializeField]
    GameObject[] oChestPrefabs;
    #endregion

    public override void SetEnemyIndex(int count)
    {
        enemyIndex.Clear();

        while (enemyIndex.Count < count)
        {
            int index = Random.Range(0, oChestPrefabs.Length);
            bool isContain = enemyIndex.Contains(index);

            if (!isContain)
            {
                enemyIndex.Add(index);
            }
        }
    }

    public override void CreateEnemy()
    {
        GameObject chest = Instantiate(oChestPrefabs[0], transform);
        chest.SetActive(false);
    }
}
