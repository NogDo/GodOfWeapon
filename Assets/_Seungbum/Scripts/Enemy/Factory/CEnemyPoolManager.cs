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
    IEnumerator spawnEliteEnemyCoroutine;
    IEnumerator spawnBossCoroutine;

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
    /// 풀에 생성된 적들을 제거한다.
    /// </summary>
    public void DestroyPool()
    {
        enemyPool.DestroyEnemys();
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

        nMeleeEnemySpawnCountMax = 5 + CStageManager.Instance.StageCount / 4;
        nRangeEnemySpawnCountMax = 3 + CStageManager.Instance.StageCount / 4;

        spawnMeleeEnemyCoroutine = SpawnMeleeEnemy();
        spawnRangeEnemyCoroutine = SpawnRangeEnemy();
        spawnChestCoroutine = SpawnChest();
        spawnEliteEnemyCoroutine = SpawnEliteEnemy();
        spawnBossCoroutine = SpawnBoss();

        StartCoroutine(spawnMeleeEnemyCoroutine);
        if (enemyPool.RangeEnemyCount > 0)
        {
            StartCoroutine(spawnRangeEnemyCoroutine);
        }
        StartCoroutine(spawnChestCoroutine);
        StartCoroutine(spawnEliteEnemyCoroutine);
        if (CStageManager.Instance.StageCount == 20)
        {
            StartCoroutine(spawnBossCoroutine);
        }
    }

    /// <summary>
    /// 적 소환 코루틴들을 종료한다.
    /// </summary>
    public void StopSpawn()
    {
        StopCoroutine(spawnMeleeEnemyCoroutine);
        StopCoroutine(spawnRangeEnemyCoroutine);
        StopCoroutine(spawnChestCoroutine);
        StopCoroutine(spawnEliteEnemyCoroutine);
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
        float meleeEnemySpawnTime = fMeleeEnemySpawnTime - (fEnemySpawnRate / 100.0f);

        while (true)
        {
            int meleeEnemySpawnCount = Random.Range(nMeleeEnemySpawnCountMax - 3, nMeleeEnemySpawnCountMax);
            meleeEnemySpawnCount = meleeEnemySpawnCount * enemyPool.MeleeEnemyCount;

            StartCoroutine(DelaySpawn(meleeEnemySpawnCount, EAttackType.MELEE));

            yield return new WaitForSeconds(meleeEnemySpawnTime);
        }
    }

    /// <summary>
    /// 원거리 적 소환 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnRangeEnemy()
    {
        float rangeEnemySpawnTime = fRangeEnemySpawnTime - (fEnemySpawnRate / 100.0f);

        while (true)
        {
            int rangeEnemySpawnCount = Random.Range(nRangeEnemySpawnCountMax - 3, nRangeEnemySpawnCountMax);
            rangeEnemySpawnCount = rangeEnemySpawnCount * enemyPool.RangeEnemyCount;

            StartCoroutine(DelaySpawn(rangeEnemySpawnCount, EAttackType.RANGE));

            yield return new WaitForSeconds(rangeEnemySpawnTime);
        }
    }

    /// <summary>
    /// 적들 생성 중간중간에 딜레이를 주고 생성하는 코루틴
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
    /// 상자 소환 코루틴
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

    /// <summary>
    /// 엘리트 적을 소환 코루틴
    /// </summary>
    IEnumerator SpawnEliteEnemy()
    {
        while (nEliteSpawnCount > 0)
        {
            float eliteEnemySpawnTime = Random.Range(3.0f, 5.0f);

            yield return new WaitForSeconds(eliteEnemySpawnTime);

            enemyPool.SpawnEliteEnemy();
            nEliteSpawnCount--;
        }
    }

    /// <summary>
    /// 보스를 소환하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnBoss()
    {
        float bossSpawnTime = Random.Range(3.0f, 5.0f);

        yield return new WaitForSeconds(bossSpawnTime);

        enemyPool.SpawnBoss();
    }
}