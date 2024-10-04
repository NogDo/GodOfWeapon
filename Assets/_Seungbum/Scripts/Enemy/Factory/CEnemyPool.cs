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
    #endregion

    void Awake()
    {
        meleeEnemyFactory = transform.GetChild(0).GetComponent<CMeleeEnemyFactory>();
        rangeEnemyFactory = transform.GetChild(1).GetComponent<CRangeEnemyFactory>();
        chestFactory = transform.GetChild(2).GetComponent<CChestFactory>();
    }

    /// <summary>
    /// 풀에 적들을 생성한다.
    /// </summary>
    public void InitPool()
    {
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

            case EAttackType.NONE:
                enemyChestPool.Enqueue(enemy);
                break;
        }
    }
}
