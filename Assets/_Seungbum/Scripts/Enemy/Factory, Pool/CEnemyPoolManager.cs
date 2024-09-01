using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyPoolManager : MonoBehaviour
{
    #region private ����
    CEnemyPool enemyPool;

    IEnumerator spawnEnemyCoroutine;

    float fSpawnTime = 1.0f;
    #endregion

    void Awake()
    {
        enemyPool = GetComponent<CEnemyPool>();

        spawnEnemyCoroutine = SpawnEnemy();
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
        StartCoroutine(spawnEnemyCoroutine);
    }

    /// <summary>
    /// �� ��ȯ �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            enemyPool.SpawnMeleeEnemy();

            yield return new WaitForSeconds(fSpawnTime);
        }
    }
}