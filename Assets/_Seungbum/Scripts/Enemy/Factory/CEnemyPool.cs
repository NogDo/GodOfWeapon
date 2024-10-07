using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyPool : MonoBehaviour
{
    #region private 변수
    Queue<GameObject> meleeEnemyPool = new Queue<GameObject>();
    Queue<GameObject> rangeEnemyPool = new Queue<GameObject>();
    Queue<GameObject> enemyChestPool = new Queue<GameObject>();

    CMeleeEnemyFactory meleeEnemyFactory;
    CRangeEnemyFactory rangeEnemyFactory;
    CChestFactory chestFactory;
    CEliteEnemyFactory eliteEnemyFactory;
    CBossFactory bossFactory;

    int nMeleeEnemyCount;
    int nRangeEnemyCount;
    #endregion

    /// <summary>
    /// 근거리 적 종류 개수
    /// </summary>
    public int MeleeEnemyCount
    {
        get
        {
            return nMeleeEnemyCount;
        }
    }

    /// <summary>
    /// 원거리 적 종류 개수
    /// </summary>
    public int RangeEnemyCount
    {
        get
        {
            return nRangeEnemyCount;
        }
    }

    void Awake()
    {
        meleeEnemyFactory = transform.GetChild(0).GetComponent<CMeleeEnemyFactory>();
        rangeEnemyFactory = transform.GetChild(1).GetComponent<CRangeEnemyFactory>();
        chestFactory = transform.GetChild(2).GetComponent<CChestFactory>();
        eliteEnemyFactory = transform.GetChild(3).GetComponent<CEliteEnemyFactory>();
        bossFactory = transform.GetChild(4).GetComponent<CBossFactory>();
    }

    /// <summary>
    /// 풀에 적들을 생성한다.
    /// </summary>
    public void InitPool()
    {
        int nEnemycount = 2;

        if (CStageManager.Instance.StageCount >= 4)
        {
            nEnemycount = 3;

            nMeleeEnemyCount = Random.Range(2, 4);
            nRangeEnemyCount = nEnemycount - nMeleeEnemyCount;
        }

        else
        {
            nMeleeEnemyCount = Random.Range(1, 3);
            nRangeEnemyCount = nEnemycount - nMeleeEnemyCount;
        }

        meleeEnemyFactory.SetEnemyIndex(nMeleeEnemyCount);
        rangeEnemyFactory.SetEnemyIndex(nRangeEnemyCount);

        for (int i = 0; i < 10; i++)
        {
            meleeEnemyFactory.CreateEnemy();
            rangeEnemyFactory.CreateEnemy();
        }
    }

    /// <summary>
    /// 풀에 생성된 적들을 제거한다.
    /// </summary>
    public void DestroyEnemys()
    {
        foreach (Transform child in meleeEnemyFactory.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in rangeEnemyFactory.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in chestFactory.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in eliteEnemyFactory.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in bossFactory.transform)
        {
            Destroy(child.gameObject);
        }

        meleeEnemyPool.Clear();
        rangeEnemyPool.Clear();
        enemyChestPool.Clear();
    }

    /// <summary>
    /// 근거리 적을 활성화 시킨다.
    /// </summary>
    public void SpawnMeleeEnemy()
    {
        if (meleeEnemyPool.Count == 0)
        {
            meleeEnemyFactory.CreateEnemy();
        }

        meleeEnemyPool.Dequeue().SetActive(true);
    }

    /// <summary>
    /// 원거리 적을 활성화 시킨다.
    /// </summary>
    public void SpawnRangeEnemy()
    {
        if (rangeEnemyPool.Count == 0)
        {
            rangeEnemyFactory.CreateEnemy();
        }

        rangeEnemyPool.Dequeue().SetActive(true);
    }

    /// <summary>
    /// 상자를 만든다.
    /// </summary>
    public void SpawnChest()
    {
        if (enemyChestPool.Count == 0)
        {
            chestFactory.CreateEnemy();
        }

        enemyChestPool.Dequeue().SetActive(true);
    }

    /// <summary>
    /// 엘리트 적을 소환한다.
    /// </summary>
    public void SpawnEliteEnemy()
    {
        eliteEnemyFactory.CreateEnemy();
    }

    /// <summary>
    /// 보스를 소환한다.
    /// </summary>
    public void SpawnBoss()
    {
        bossFactory.CreateEnemy();
    }

    /// <summary>
    /// 적을 디스폰한다. (Pool에 반환)
    /// </summary>
    /// <param name="enemy">적 오브젝트</param>
    /// <param name="type">적 공격 타입</param>
    public void ReturnPool(GameObject enemy, EAttackType type)
    {
        switch (type)
        {
            case EAttackType.MELEE:
                meleeEnemyPool.Enqueue(enemy);
                break;

            case EAttackType.RANGE:
                rangeEnemyPool.Enqueue(enemy);
                break;

            case EAttackType.BOTH:
                break;

            case EAttackType.NONE:
                enemyChestPool.Enqueue(enemy);
                break;
        }
    }
}
