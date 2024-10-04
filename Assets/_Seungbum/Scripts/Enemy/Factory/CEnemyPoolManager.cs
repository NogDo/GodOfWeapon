using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyPoolManager : MonoBehaviour
{
    #region static 변수
    public static CEnemyPoolManager Instance { get; private set; }
    #endregion

    #region private 변수
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
    /// 풀을 초기화하고 적을 소환한다.
    /// </summary>
    public void InitPooling()
    {
        enemyPool.InitPool();
        StartSpawn();
    }

    /// <summary>
    /// 적 소환 코루틴들을 실행한다.
    /// </summary>
    public void StartSpawn()
    {
        nEliteSpawnCount = nExtraEliteSpawnCount;

        if (CStageManager.Instance.StageCount == 10)
        {
            nEliteSpawnCount++;
        }

        nExtraEliteSpawnCount = 0;

        spawnMeleeEnemyCoroutine = SpawnMeleeEnemy();
        spawnRangeEnemyCoroutine = SpawnRangeEnemy();
        spawnChestCoroutine = SpawnChest();

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
    /// 적 생성량을 늘린다.
    /// </summary>
    /// <param name="rate">늘어날 생성량</param>
    public void IncreaseEnemySpawnRate(float rate)
    {
        fEnemySpawnRate += rate;
    }

    /// <summary>
    /// 적 생성량을 줄인다.
    /// </summary>
    /// <param name="rate">줄어들 생성량</param>
    public void DecreaseEnemySpawnRate(float rate)
    {
        fEnemySpawnRate -= rate;
    }

    /// <summary>
    /// 정예 적 생성 횟수를 늘린다.
    /// </summary>
    /// <param name="count">생성 횟수</param>
    public void IncreaseEliteSpawnCount(int count)
    {
        nExtraEliteSpawnCount = count;
    }

    /// <summary>
    /// 근접 적 소환 코루틴
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
    /// 원거리 적 소환 코루틴
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
    /// 상자 소환 코루틴
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