using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyIndicatorManager : MonoBehaviour
{
    #region static 변수
    public static CEnemyIndicatorManager Instance { get; private set; }
    #endregion

    #region private 변수
    CEnemyIndicatorPool indicatorPool;
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

        indicatorPool = GetComponent<CEnemyIndicatorPool>();
    }

    /// <summary>
    /// 구체 피격 범위를 반환한다.
    /// </summary>
    /// <returns></returns>
    public CEnemySphereIndicatorControl SpawnSphereIndicator()
    {
        return indicatorPool.SpawnSphereIndicator();
    }

    /// <summary>
    /// 직선 피격 범위를 반환한다.
    /// </summary>
    /// <returns></returns>
    public CEnemyLineIndicatorControl SpawnLineIndicator()
    {
        return indicatorPool.SpawnLineIndicator();
    }
}
