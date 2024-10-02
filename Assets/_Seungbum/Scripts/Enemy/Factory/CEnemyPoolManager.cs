using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyPoolManager : MonoBehaviour
{
    #region static ����
    public static CEnemyPoolManager Instance { get; private set; }
    #endregion

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
        Instance = this;
        DontDestroyOnLoad(gameObject);

        enemyPool = GetComponent<CEnemyPool>();
    }

    /// <summary>
    /// Ǯ�� �ʱ�ȭ�ϰ� ���� ��ȯ�Ѵ�.
    /// </summary>
    public void InitPooling()
    {
        enemyPool.InitPool();
        StartSpawn();
    }

    /// <summary>
    /// �� ��ȯ �ڷ�ƾ���� �����Ѵ�.
    /// </summary>
    public void StartSpawn()
    {
        spawnMeleeEnemyCoroutine = SpawnMeleeEnemy();
        spawnRangeEnemyCoroutine = SpawnRangeEnemy();
        spawnChestCoroutine = SpawnChest();

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
            float meleeEnemySpawnCount = Random.Range(1.0f, 7.0f);

            for (int i = 0; i < meleeEnemySpawnCount; i++)
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
            float rangeEnemySpawnCount = Random.Range(0.0f, 3.0f);

            for (int i = 0; i < rangeEnemySpawnCount; i++)
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

            yield return new WaitForSeconds(chestSpawnTime);

            enemyPool.SpawnChest();
        }
    }
}