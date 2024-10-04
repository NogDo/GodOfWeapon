using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyProjectilePool : MonoBehaviour
{
    #region private 변수
    Dictionary<string, Queue<CEnemyProjectileControl>> enemyProjectilePool = new Dictionary<string, Queue<CEnemyProjectileControl>>();
    #endregion

    /// <summary>
    /// 해당 풀이 존재하는지 확인
    /// </summary>
    /// <param name="key">풀 이름</param>
    /// <returns></returns>
    public bool HasKey(string key)
    {
        return enemyProjectilePool.ContainsKey(key);
    }

    /// <summary>
    /// 풀을 생성한다.
    /// </summary>
    /// <param name="key">풀 이름</param>
    public void CreatePool(string key)
    {
        Queue<CEnemyProjectileControl> projectilePool = new Queue<CEnemyProjectileControl>();

        enemyProjectilePool.Add(key, projectilePool);
    }

    /// <summary>
    /// 생성된 풀을 제거한다.
    /// </summary>
    public void DestroyPool()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        enemyProjectilePool.Clear();
    }

    /// <summary>
    /// 투사체를 활성화 시키기 위해 풀에서 가져온다.
    /// </summary>
    /// <param name="particle">가져올 투사체</param>
    /// <returns></returns>
    public CEnemyProjectileControl SpawnProjectile(CEnemyProjectileControl particle, string key)
    {
        if (enemyProjectilePool[key].Count <= 0)
        {
            CEnemyProjectileControl projectile = Instantiate(particle, transform);
            projectile.InitSkillName(key);
            projectile.gameObject.SetActive(false);
        }

        return enemyProjectilePool[key].Dequeue();
    }

    /// <summary>
    /// 사용한 투사체를 풀로 반환한다.
    /// </summary>
    /// <param name="particle">반환할 투사체</param>
    /// <param name="key">풀 이름</param>
    public void ReturnPool(CEnemyProjectileControl particle, string key)
    {
        enemyProjectilePool[key].Enqueue(particle);
    }
}