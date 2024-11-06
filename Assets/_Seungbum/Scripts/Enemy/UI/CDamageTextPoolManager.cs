using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDamageTextPoolManager : MonoBehaviour
{
    #region static ����
    public static CDamageTextPoolManager Instance { get; private set; }
    #endregion

    #region private ����
    CDamageTextPool damageTextPool;

    bool canSpawn = true;
    #endregion

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

        damageTextPool = GetComponent<CDamageTextPool>();
    }

    /// <summary>
    /// �� �Ϲ� �ǰ� �ؽ�Ʈ�� Ȱ��ȭ��Ų��.
    /// </summary>
    /// <param name="target">�ؽ�Ʈ ��ȯ�� ��ġ</param>
    /// <param name="damage">������</param>
    public void SpawnEnemyNormalText(Transform target, float damage)
    {
        if (canSpawn)
        {
            damageTextPool.DisplayText(target, damage, Color.white, true);
        }
    }

    /// <summary>
    /// �� ũ��Ƽ�� �ǰ� �ؽ�Ʈ�� Ȱ��ȭ��Ų��.
    /// </summary>
    /// <param name="target">�ؽ�Ʈ ��ȯ�� ��ġ</param>
    /// <param name="damage">������</param>
    public void SpawnEnemyCriticalText(Transform target, float damage)
    {
        if (canSpawn)
        {
            damageTextPool.DisplayText(target, damage, new Color(1.0f, 0.65f, 0.0f), true);
        }
    }

    /// <summary>
    /// �÷��̾� �ǰ� �ؽ�Ʈ�� Ȱ��ȭ��Ų��.
    /// </summary>
    /// <param name="target">�ؽ�Ʈ ��ȯ�� ��ġ</param>
    /// <param name="damage">������</param>
    public void SpawnPlayerText(Transform target, float damage)
    {
        if (canSpawn)
        {
            damageTextPool.DisplayText(target, damage, Color.red, true);
        }
    }

    /// <summary>
    /// �÷��̾� �� �ؽ�Ʈ�� Ȱ��ȭ��Ų��.
    /// </summary>
    /// <param name="target">�ؽ�Ʈ ��ȯ�� ��ġ</param>
    /// <param name="heal">����</param>
    public void SpawnPlayerHealText(Transform target, float heal)
    {
        if (canSpawn)
        {
            damageTextPool.DisplayText(target, heal, Color.green, false);
        }
    }

    /// <summary>
    /// ������ �ؽ�Ʈ ������ ����Ѵ�.
    /// </summary>
    public void StartSpawn()
    {
        canSpawn = true;
    }

    /// <summary>
    /// ������ �ؽ�Ʈ ������ �����Ѵ�.
    /// </summary>
    public void StopSpawn()
    {
        canSpawn = false;
    }
}