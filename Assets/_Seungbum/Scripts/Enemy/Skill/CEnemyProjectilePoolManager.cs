using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyProjectilePoolManager : MonoBehaviour
{
    #region static 변수
    public static CEnemyProjectilePoolManager Instance { get; private set; }
    #endregion

    #region private 변수
    CEnemyProjectilePool enemyProjectilePool;
    #endregion

    void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);

        enemyProjectilePool = GetComponent<CEnemyProjectilePool>();
    }

    /// <summary>
    /// 투사체를 사용하기 위해 풀에 요청을 보낸다. 풀이 없다면 생성한다.
    /// </summary>
    /// <param name="particle">투사체</param>
    /// <param name="key">풀 이름</param>
    public void SpawnProjectile(CEnemyProjectileControl particle, string key, out CEnemyProjectileControl spawnParticle)
    {
        if (!enemyProjectilePool.HasKey(key))
        {
            enemyProjectilePool.CreatePool(key);
        }

        spawnParticle = enemyProjectilePool.SpawnProjectile(particle, key);
    }

    /// <summary>
    /// 생성된 풀들을 제거한다.
    /// </summary>
    public void DestroyPool()
    {
        enemyProjectilePool.DestroyPool();
    }
}