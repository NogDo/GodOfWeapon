using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyProjectilePoolManager : MonoBehaviour
{
    #region static ����
    public static CEnemyProjectilePoolManager Instance { get; private set; }
    #endregion

    #region private ����
    CEnemyProjectilePool enemyProjectilePool;
    #endregion

    void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);

        enemyProjectilePool = GetComponent<CEnemyProjectilePool>();
    }

    /// <summary>
    /// ����ü�� ����ϱ� ���� Ǯ�� ��û�� ������. Ǯ�� ���ٸ� �����Ѵ�.
    /// </summary>
    /// <param name="particle">����ü</param>
    /// <param name="key">Ǯ �̸�</param>
    public void SpawnProjectile(CEnemyProjectileControl particle, string key, out CEnemyProjectileControl spawnParticle)
    {
        if (!enemyProjectilePool.HasKey(key))
        {
            enemyProjectilePool.CreatePool(key);
        }

        spawnParticle = enemyProjectilePool.SpawnProjectile(particle, key);
    }

    /// <summary>
    /// ������ Ǯ���� �����Ѵ�.
    /// </summary>
    public void DestroyPool()
    {
        enemyProjectilePool.DestroyPool();
    }
}