using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyPoolManager : MonoBehaviour
{
    #region private 函荐
    CEnemyPool enemyPool;

    IEnumerator spawnMeleeEnemyCoroutine;
    IEnumerator spawnRangeEnemyCoroutine;

    float fMeleeEnemySpawnTime = 3.0f;
    float fRangeEnemySpawnTime = 5.0f;
    #endregion

    void Awake()
    {
        enemyPool = GetComponent<CEnemyPool>();

        spawnMeleeEnemyCoroutine = SpawnMeleeEnemy();
        spawnRangeEnemyCoroutine = SpawnRangeEnemy();
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
        StartCoroutine(spawnMeleeEnemyCoroutine);
        StartCoroutine(spawnRangeEnemyCoroutine);
    }

    /// <summary>
    /// 辟立 利 家券 内风凭
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnMeleeEnemy()
    {
        while (true)
        {
            int meleeEnemySpawnNumber = Random.Range(1, 7);

            for (int i = 0; i < meleeEnemySpawnNumber; i++)
            {
                enemyPool.SpawnMeleeEnemy();
            }

            yield return new WaitForSeconds(fMeleeEnemySpawnTime);
        }
    }

    /// <summary>
    /// 盔芭府 利 家券 内风凭
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnRangeEnemy()
    {
        while (true)
        {
            int rangeEnemySpawnNumber = Random.Range(0, 3);

            for (int i = 0; i < rangeEnemySpawnNumber; i++)
            {
                enemyPool.SpawnRangeEnemy();
            }

            yield return new WaitForSeconds(fRangeEnemySpawnTime);
        }
    }
}