using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyPoolManager : MonoBehaviour
{
    #region private ����
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
    /// �� ��ȯ �ڷ�ƾ�� �����Ѵ�.
    /// </summary>
    public void StartSpawn()
    {
        StartCoroutine(spawnMeleeEnemyCoroutine);
        StartCoroutine(spawnRangeEnemyCoroutine);
    }

    /// <summary>
    /// ���� �� ��ȯ �ڷ�ƾ
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
    /// ���Ÿ� �� ��ȯ �ڷ�ƾ
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