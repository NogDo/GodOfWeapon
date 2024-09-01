using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyPoolManager : MonoBehaviour
{
    #region private 函荐
    CEnemyPool enemyPool;

    IEnumerator spawnEnemyCoroutine;

    float fSpawnTime = 1.0f;
    #endregion

    void Awake()
    {
        enemyPool = GetComponent<CEnemyPool>();

        spawnEnemyCoroutine = SpawnEnemy();
    }

    IEnumerator Start()
    {
        yield return new WaitUntil(() => CCreateMapManager.Instance.IsCreateMap);

        StartSpawn();
    }

    /// <summary>
    /// 利 家券 内风凭阑 角青茄促.
    /// </summary>
    public void StartSpawn()
    {
        StartCoroutine(spawnEnemyCoroutine);
    }

    /// <summary>
    /// 利 家券 内风凭
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            enemyPool.SpawnMeleeEnemy();

            yield return new WaitForSeconds(fSpawnTime);
        }
    }
}