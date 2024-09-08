using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyPoolManager : MonoBehaviour
{
    #region private ����
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
    /// �� ��ȯ �ڷ�ƾ���� �����Ѵ�.
    /// </summary>
    public void StartSpawn()
    {
        StartCoroutine(spawnMeleeEnemyCoroutine);
        StartCoroutine(spawnRangeEnemyCoroutine);
        StartCoroutine(spawnChestCoroutine);
    }


    /// <summary>
    /// �� ��ȯ �ڷ�ƾ���� �����Ѵ�.
    /// </summary>
    public void StopSpawn()
    {
        StopCoroutine(spawnMeleeEnemyCoroutine);
        StopCoroutine(spawnRangeEnemyCoroutine);
        StopCoroutine(spawnChestCoroutine);
    }

    /// <summary>
    /// ���� �� ��ȯ �ڷ�ƾ
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
    /// ���Ÿ� �� ��ȯ �ڷ�ƾ
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
    /// ���� ��ȯ �ڷ�ƾ
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