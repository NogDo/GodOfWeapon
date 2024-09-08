using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyPool : MonoBehaviour
{
    #region private ����
    Queue<GameObject> meleeEnemyPool = new Queue<GameObject>();
    Queue<GameObject> rangeEnemyPool = new Queue<GameObject>();

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

    void Start()
    {
        InitPool();
    }

    /// <summary>
    /// Ǯ�� ������ �����Ѵ�.
    /// </summary>
    void InitPool()
    {
        for (int i = 0; i < 10; i++)
        {
            meleeEnemyFactory.CreateEnemy();
            rangeEnemyFactory.CreateEnemy();
        }
    }

    /// <summary>
    /// �ٰŸ� ���� Ȱ��ȭ ��Ų��.
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
    /// ���Ÿ� ���� Ȱ��ȭ ��Ų��.
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
    /// ���ڸ� �����.
    /// </summary>
    public void SpawnChest()
    {
        chestFactory.CreateEnemy();
    }

    /// <summary>
    /// ���� �����Ѵ�. (Pool�� ��ȯ)
    /// </summary>
    /// <param name="enemy">�� ������Ʈ</param>
    /// <param name="type">�� ���� Ÿ��</param>
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
        }
    }
}
