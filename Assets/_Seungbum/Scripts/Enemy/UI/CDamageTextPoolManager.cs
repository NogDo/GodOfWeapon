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
    #endregion

    void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);

        damageTextPool = GetComponent<CDamageTextPool>();
    }

    /// <summary>
    /// �� �Ϲ� �ǰ� �ؽ�Ʈ�� Ȱ��ȭ��Ų��.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="damage"></param>
    public void SpawnEnemyNormalText(Transform target, float damage)
    {
        damageTextPool.DisplayText(target, damage, Color.white);
    }

    /// <summary>
    /// �� ũ��Ƽ�� �ǰ� �ؽ�Ʈ�� Ȱ��ȭ��Ų��.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="damage"></param>
    public void SpawnEnemyCriticalText(Transform target, float damage)
    {
        damageTextPool.DisplayText(target, damage, Color.yellow);
    }

    /// <summary>
    /// �÷��̾� �ǰ� �ؽ�Ʈ�� Ȱ��ȭ��Ų��.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="damage"></param>
    public void SpawnPlayerText(Transform target, float damage)
    {
        damageTextPool.DisplayText(target, damage, Color.red);
    }
}