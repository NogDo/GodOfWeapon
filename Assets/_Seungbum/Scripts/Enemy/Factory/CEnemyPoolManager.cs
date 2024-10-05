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

    int nMeleeEnemySpawnCountMax;
    int nRangeEnemySpawnCountMax;

    float fEnemySpawnRate = 0.0f;
    int nEliteSpawnCount = 0;
    int nExtraEliteSpawnCount = 0;
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
    /// Ǯ�� ������ ������ �����Ѵ�.
    /// </summary>
    public void DestroyPool()
    {
        enemyPool.DestroyEnemys();
    }

    /// <summary>
    /// �� ��ȯ �ڷ�ƾ���� �����Ѵ�.
    /// </summary>
    public void StartSpawn()
    {
        nEliteSpawnCount = nExtraEliteSpawnCount;

        if (CStageManager.Instance.StageCount == 10)
        {
            nEliteSpawnCount++;
        }

        nExtraEliteSpawnCount = 0;

        nMeleeEnemySpawnCountMax = 5 + CStageManager.Instance.StageCount / 2;
        nRangeEnemySpawnCountMax = 3 + CStageManager.Instance.StageCount / 2;

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
    /// �� �������� �ø���.
    /// </summary>
    /// <param name="rate">�þ ������</param>
    public void IncreaseEnemySpawnRate(float rate)
    {
        fEnemySpawnRate += rate;
    }

    /// <summary>
    /// �� �������� ���δ�.
    /// </summary>
    /// <param name="rate">�پ�� ������</param>
    public void DecreaseEnemySpawnRate(float rate)
    {
        fEnemySpawnRate -= rate;
    }

    /// <summary>
    /// ���� �� ���� Ƚ���� �ø���.
    /// </summary>
    /// <param name="count">���� Ƚ��</param>
    public void IncreaseEliteSpawnCount(int count)
    {
        nExtraEliteSpawnCount = count;
    }

    /// <summary>
    /// ���� �� ��ȯ �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnMeleeEnemy()
    {
        float meleeEnemySpawnTime = fMeleeEnemySpawnTime - (fEnemySpawnRate / 0.01f);

        while (true)
        {
            int meleeEnemySpawnCount = Random.Range(nMeleeEnemySpawnCountMax - 3, nMeleeEnemySpawnCountMax);

            StartCoroutine(DelaySpawn(meleeEnemySpawnCount, EAttackType.MELEE));

            yield return new WaitForSeconds(meleeEnemySpawnTime);
        }
    }

    /// <summary>
    /// ���Ÿ� �� ��ȯ �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnRangeEnemy()
    {
        float rangeEnemySpawnTime = fRangeEnemySpawnTime - (fEnemySpawnRate / 0.01f);

        while (true)
        {
            int rangeEnemySpawnCount = Random.Range(nRangeEnemySpawnCountMax - 3, nRangeEnemySpawnCountMax);

            StartCoroutine(DelaySpawn(rangeEnemySpawnCount, EAttackType.RANGE));

            yield return new WaitForSeconds(rangeEnemySpawnTime);
        }
    }

    /// <summary>
    /// ���� ���� �߰��߰��� �����̸� �ְ� �����ϴ� �ڷ�ƾ
    /// </summary>
    /// <param name="spawnCount"></param>
    /// <param name="attackType"></param>
    /// <returns></returns>
    IEnumerator DelaySpawn(int spawnCount, EAttackType attackType)
    {
        while (spawnCount > 0)
        {
            int randSpawnCount = Random.Range(1, 3);
            float spawnDelay = Random.Range(0.0f, 0.2f);

            if (spawnCount - randSpawnCount < 0)
            {
                randSpawnCount = spawnCount;
            }

            spawnCount -= randSpawnCount;

            for (int i = 0; i < randSpawnCount; i++)
            {
                switch (attackType)
                {
                    case EAttackType.MELEE:
                        enemyPool.SpawnMeleeEnemy();
                        break;

                    case EAttackType.RANGE:
                        enemyPool.SpawnRangeEnemy();
                        break;

                    case EAttackType.BOTH:
                        break;

                    case EAttackType.NONE:
                        break;

                    default:
                        break;
                }
            }

            yield return new WaitForSeconds(spawnDelay);
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
            float chestSpawnTime = Random.Range(5.0f, 10.0f);

            yield return new WaitForSeconds(chestSpawnTime);

            enemyPool.SpawnChest();
        }
    }
}