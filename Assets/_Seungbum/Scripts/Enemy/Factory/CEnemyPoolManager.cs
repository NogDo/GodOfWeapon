using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyPoolManager : MonoBehaviour
{
    #region private 변수
    CEnemyPool enemyPool;

    IEnumerator spawnMeleeEnemyCoroutine;
    IEnumerator spawnRangeEnemyCoroutine;
    IEnumerator spawnChestCoroutine;

    float fMeleeEnemySpawnTime = 3.0f;
    float fRangeEnemySpawnTime = 5.0f;
    #endregion

    void Awake()
    {
        enemyPool = GetComponent<CEnemyPool>();

        spawnMeleeEnemyCoroutine = SpawnMeleeEnemy();
        spawnRangeEnemyCoroutine = SpawnRangeEnemy();
        spawnChestCoroutine = SpawnChest();
    }

    IEnumerator Start()
    {
        yield return new WaitUntil(() => CCreateMapManager.Instance.IsCreateMap);

        StartSpawn();
    }

    /// <summary>
    /// 적 소환 코루틴들을 실행한다.
    /// </summary>
    public void StartSpawn()
    {
        StartCoroutine(spawnMeleeEnemyCoroutine);
        StartCoroutine(spawnRangeEnemyCoroutine);
        StartCoroutine(spawnChestCoroutine);
    }


    /// <summary>
    /// 적 소환 코루틴들을 종료한다.
    /// </summary>
    public void StopSpawn()
    {
        StopCoroutine(spawnMeleeEnemyCoroutine);
        StopCoroutine(spawnRangeEnemyCoroutine);
        StopCoroutine(spawnChestCoroutine);
    }

    /// <summary>
    /// 근접 적 소환 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnMeleeEnemy()
    {
        while (true)
        {
            float meleeEnemySpawnTime = Random.Range(1.0f, 7.0f);

            for (int i = 0; i < meleeEnemySpawnTime; i++)
            {
                enemyPool.SpawnMeleeEnemy();
            }

            yield return new WaitForSeconds(fMeleeEnemySpawnTime);
        }
    }

    /// <summary>
    /// 원거리 적 소환 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnRangeEnemy()
    {
        while (true)
        {
            float rangeEnemySpawnTime = Random.Range(0.0f, 3.0f);

            for (int i = 0; i < rangeEnemySpawnTime; i++)
            {
                enemyPool.SpawnRangeEnemy();
            }

            yield return new WaitForSeconds(fRangeEnemySpawnTime);
        }
    }

    /// <summary>
    /// 상자 소환 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnChest()
    {
        while (true)
        {
            float chestSpawnTime = Random.Range(10.0f, 20.0f);

            yield return new WaitForSeconds(1.0f);

            enemyPool.SpawnChest();
        }
    }
}