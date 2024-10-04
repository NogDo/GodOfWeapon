using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyProjectilePool : MonoBehaviour
{
    #region private ����
    Dictionary<string, Queue<CEnemyProjectileControl>> enemyProjectilePool = new Dictionary<string, Queue<CEnemyProjectileControl>>();
    #endregion

    /// <summary>
    /// �ش� Ǯ�� �����ϴ��� Ȯ��
    /// </summary>
    /// <param name="key">Ǯ �̸�</param>
    /// <returns></returns>
    public bool HasKey(string key)
    {
        return enemyProjectilePool.ContainsKey(key);
    }

    /// <summary>
    /// Ǯ�� �����Ѵ�.
    /// </summary>
    /// <param name="key">Ǯ �̸�</param>
    public void CreatePool(string key)
    {
        Queue<CEnemyProjectileControl> projectilePool = new Queue<CEnemyProjectileControl>();

        enemyProjectilePool.Add(key, projectilePool);
    }

    /// <summary>
    /// ������ Ǯ�� �����Ѵ�.
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
    /// ����ü�� Ȱ��ȭ ��Ű�� ���� Ǯ���� �����´�.
    /// </summary>
    /// <param name="particle">������ ����ü</param>
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
    /// ����� ����ü�� Ǯ�� ��ȯ�Ѵ�.
    /// </summary>
    /// <param name="particle">��ȯ�� ����ü</param>
    /// <param name="key">Ǯ �̸�</param>
    public void ReturnPool(CEnemyProjectileControl particle, string key)
    {
        enemyProjectilePool[key].Enqueue(particle);
    }
}