using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDamageTextPoolManager : MonoBehaviour
{
    #region static 변수
    public static CDamageTextPoolManager Instance { get; private set; }
    #endregion

    #region private 변수
    CDamageTextPool damageTextPool;
    #endregion

    void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);

        damageTextPool = GetComponent<CDamageTextPool>();
    }

    /// <summary>
    /// 적 일반 피격 텍스트를 활성화시킨다.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="damage"></param>
    public void SpawnEnemyNormalText(Transform target, float damage)
    {
        damageTextPool.DisplayText(target, damage, Color.white);
    }

    /// <summary>
    /// 적 크리티컬 피격 텍스트를 활성화시킨다.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="damage"></param>
    public void SpawnEnemyCriticalText(Transform target, float damage)
    {
        damageTextPool.DisplayText(target, damage, Color.yellow);
    }

    /// <summary>
    /// 플레이어 피격 텍스트를 활성화시킨다.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="damage"></param>
    public void SpawnPlayerText(Transform target, float damage)
    {
        damageTextPool.DisplayText(target, damage, Color.red);
    }
}