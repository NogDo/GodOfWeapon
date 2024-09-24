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
    /// <param name="target">텍스트 소환할 위치</param>
    /// <param name="damage">데미지</param>
    public void SpawnEnemyNormalText(Transform target, float damage)
    {
        damageTextPool.DisplayText(target, damage, Color.white, true);
    }

    /// <summary>
    /// 적 크리티컬 피격 텍스트를 활성화시킨다.
    /// </summary>
    /// <param name="target">텍스트 소환할 위치</param>
    /// <param name="damage">데미지</param>
    public void SpawnEnemyCriticalText(Transform target, float damage)
    {
        damageTextPool.DisplayText(target, damage, new Color(1.0f, 0.65f, 0.0f), true);
    }

    /// <summary>
    /// 플레이어 피격 텍스트를 활성화시킨다.
    /// </summary>
    /// <param name="target">텍스트 소환할 위치</param>
    /// <param name="damage">데미지</param>
    public void SpawnPlayerText(Transform target, float damage)
    {
        damageTextPool.DisplayText(target, damage, Color.red, true);
    }

    /// <summary>
    /// 플레이어 힐 텍스트를 활설화시킨다.
    /// </summary>
    /// <param name="target">텍스트 소환할 위치</param>
    /// <param name="heal">힐량</param>
    public void SpawnPlayerHealText(Transform target, float heal)
    {
        damageTextPool.DisplayText(target, heal, Color.green, false);
    }
}